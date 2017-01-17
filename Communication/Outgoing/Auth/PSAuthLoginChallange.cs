using CobolWow.Network;
using CobolWow.Game.Constants;
using CobolWow.Tools.Extensions;
using CobolWow.Tools.Cryptography;
using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSAuthLoginChallange : ServerPacket
   {
      public PSAuthLoginChallange(Authenticator authenticator) : base(LoginOpcodes.AUTH_LOGIN_CHALLENGE)
      {
         Write((byte)base.Opcode);
         Write((byte)0);
         Write((byte)0);
         Write(authenticator.SRP6.B);
         Write((byte)1);
         Write(authenticator.SRP6.g[0]);
         Write((byte)authenticator.SRP6.N.Length);
         Write(authenticator.SRP6.N);
         Write(authenticator.SRP6.salt);
         Write(new byte[16]);
         Write((byte)0);
      }

      public PSAuthLoginChallange(AccountStatus accountStatus) : base(LoginOpcodes.AUTH_LOGIN_CHALLENGE)
      {
         Write((byte)base.Opcode);
         this.WriteNull(1);
         Write((byte)accountStatus);
      }
   }
}
