using System.Text;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Security.Cryptography;

using CobolWow.Net;
using CobolWow.Tools;

using CobolWow.Game.Handlers;
using CobolWow.Game.Sessions;
using CobolWow.Tools.Cryptography;

using CobolWow.Communication;
using CobolWow.Communication.Incoming.Auth;
using CobolWow.Communication.Incoming.World;
using CobolWow.Communication.Incoming.World.Auth;

using CobolWow.Communication.Outgoing.Auth;
using CobolWow.Communication.Outgoing.World;
using CobolWow.Communication.Outgoing.World.ActionBarButton;
using CobolWow.Communication.Outgoing.World.Player;
using CobolWow.Communication.Outgoing.World.Update;
using CobolWow.Database20.Tables;
using CobolWow.Game.Constants;
using System.Text.RegularExpressions;

namespace CobolWow.Game.Managers
{
   public class AuthManager
   {
      public static void Boot()
      {
         WorldDataRouter.AddHandler<PCAuthSession>(WorldOpcodes.CMSG_AUTH_SESSION, OnAuthSession);
         WorldDataRouter.AddHandler<PCPlayerLogin>(WorldOpcodes.CMSG_PLAYER_LOGIN, OnPlayerLogin);
         LoginDataRouter.AddHandler<PCAuthLoginChallenge>(LoginOpcodes.AUTH_LOGIN_CHALLENGE, OnAuthLoginChallenge);
         LoginDataRouter.AddHandler<PCAuthLoginProof>(LoginOpcodes.AUTH_LOGIN_PROOF, OnAuthLoginProof);
         LoginDataRouter.AddHandler(LoginOpcodes.REALM_LIST, OnRealmList);
         WorldDataRouter.AddHandler(WorldOpcodes.CMSG_UPDATE_ACCOUNT_DATA, OnUpdateAccount);

         Logger.Log(LogType.Information, "AuthManager Initialized.");
      }

      private static void OnRealmList(LoginSession session, byte[] packet)
      {
         session.SendPacket(new PSRealmList());
      }


      private static void OnAuthLoginChallenge(LoginSession session, PCAuthLoginChallenge packet)
      {       
         Account account = Database20.Accounts.RealmAccountManager.GetAccount(packet.Name);
         Logger.Log(LogType.Debug, "OnAuthLoginChallenge " + account.username);

         if (account != null)
         {
            session.accountName = packet.Name;

            byte[] userBytes = Encoding.UTF8.GetBytes(account.username.ToUpper());
            byte[] passBytes = Encoding.UTF8.GetBytes(account.sha_pass_hash.ToUpper());

            Logger.Log(LogType.Warning, account.sha_pass_hash.ToUpper());

            session.Authenticator = new Authenticator();
            session.Authenticator.CalculateX(userBytes, passBytes);
            session.SendData(new PSAuthLoginChallange(session.Authenticator));
         }
         else
         {
            Logger.Log(LogType.Error, "Unknown account!");
            session.SendData(new PSAuthLoginChallange(AccountStatus.UnknownAccount));
         }
      }

      private static void OnAuthLoginProof(LoginSession session, PCAuthLoginProof packet)
      {
         session.Authenticator.CalculateU(packet.A);
         session.Authenticator.CalculateM2(packet.M1);
         session.Authenticator.CalculateAccountHash();

         byte[] sessionKey = session.Authenticator.SRP6.K;

         Account account = Database20.Accounts.RealmAccountManager.GetAccount(session.accountName);
        
         if (account != null)
         {        
            account.sessionkey = Regex.Replace(Helper.ByteArrayToHex(sessionKey), @"\s+", "");
            Database20.Accounts.RealmAccountManager.UpdateAccount(account);
            session.SendData(new PSAuthLoginProof(session.Authenticator));
         }
         else
         {
            Logger.Log(LogType.Error, "Unknown account!");
            session.SendData(new PSAuthLoginProof(AccountStatus.UnknownAccount));
         }
      }

      private static void OnUpdateAccount(WorldSession session, byte[] data)
      {
         Logger.Log(LogType.Debug, "AuthManager OnUpdateAccount.");
         //Log.Print(LogType.Map, "Length: " + length + " Real Length: " + _dataBuffer.Length);
         //crypt.decrypt(new byte[(int)2 * 6]);
      }

      private static void OnPlayerLogin(WorldSession session, PCPlayerLogin packet)
      {
         Logger.Log(LogType.Debug, "AuthManager OnPlayerLogin. - Todo Spells");

         session.Character = Database20.Characters.CharacterManager.GetCharacter((int)packet.GUID);
         session.SendPacket(new LoginVerifyWorld(session.Character.map, session.Character.position_x, session.Character.position_y, session.Character.position_z, 0));
         session.SendPacket(new PSAccountDataTimes());
         session.SendPacket(new PSSetRestStart());
         session.SendPacket(new PSBindPointUpdate());
         session.SendPacket(new PSTutorialFlags());
         //SpellManager.SendInitialSpells(session); //TODO FIX
         session.SendPacket(new PSActionButtons(session.Character));
         session.SendPacket(new PSInitializeFactions());
         session.SendPacket(new PSLoginSetTimeSpeed());
         session.SendPacket(new PSInitWorldStates());
         session.SendPacket(PSUpdateObject.CreateOwnCharacterUpdate(session.Character, out session.Entity));
         session.Entity.Session = session;
         World.DispatchOnPlayerSpawn(session.Entity);
      }

      private static void OnAuthSession(WorldSession session, PCAuthSession packet)
      {
         session.Account = Database20.Accounts.RealmAccountManager.GetAccount(packet.AccountName);
         session.crypt = new VanillaCrypt();
         session.crypt.init(Helper.HexToByteArray(session.Account.sessionkey));
         Logger.Log(LogType.Debug, "AuthManager Started Encryption");
         session.SendPacket(new PSAuthResponse());
      }

      //private static void CalculateAccountHash(LoginSession session)
      //{
      //   SHA1 shaM1 = new SHA1CryptoServiceProvider();
      //   byte[] S = session.Srp6.S;
      //   var S1 = new byte[16];
      //   var S2 = new byte[16];

      //   for (int t = 0; t < 16; t++)
      //   {
      //      S1[t] = S[t * 2];
      //      S2[t] = S[(t * 2) + 1];
      //   }

      //   byte[] hashS1 = shaM1.ComputeHash(S1);
      //   byte[] hashS2 = shaM1.ComputeHash(S2);
      //   session.SessionKey = new byte[hashS1.Length + hashS2.Length];
      //   for (int t = 0; t < 20; t++)
      //   {
      //      session.SessionKey[t * 2] = hashS1[t];
      //      session.SessionKey[(t * 2) + 1] = hashS2[t];
      //   }

      //   var opad = new byte[64];
      //   var ipad = new byte[64];

      //   //Static 16 byte Key located at 0x0088FB3C
      //   var key = new byte[] { 56, 167, 131, 21, 248, 146, 37, 48, 113, 152, 103, 177, 140, 4, 226, 170 };

      //   //Fill 64 bytes of same value
      //   for (int i = 0; i <= 64 - 1; i++)
      //   {
      //      opad[i] = 0x05C;
      //      ipad[i] = 0x036;
      //   }

      //   //XOR Values
      //   for (int i = 0; i <= 16 - 1; i++)
      //   {
      //      opad[i] = (byte)(opad[i] ^ key[i]);
      //      ipad[i] = (byte)(ipad[i] ^ key[i]);
      //   }

      //   byte[] buffer1 = ipad.Concat(session.SessionKey).ToArray();
      //   byte[] buffer2 = shaM1.ComputeHash(buffer1);

      //   buffer1 = opad.Concat(buffer2).ToArray();
      //   session.SessionKey = shaM1.ComputeHash(buffer1);
      //}
   }
}
