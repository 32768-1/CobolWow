using System.Linq;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Tools;
using CobolWow.Game.Handlers;
using CobolWow.Communication;
using CobolWow.Game.Constants;
using CobolWow.Game.Constants.Login;
using CobolWow.Communication.Outgoing.Char;
using CobolWow.Communication.Incoming.Character;
using CobolWow.Database20.Tables;
using System;
using CobolWow.Tools.Database20.Tables;
using CobolWow.DBC.Structs;

namespace CobolWow.Game.Managers
{
   public class CharacterManager
   {
      public static void Boot()
      {
         WorldDataRouter.AddHandler(WorldOpcodes.CMSG_CHAR_ENUM, OnCharEnum);
         WorldDataRouter.AddHandler<PCCharCreate>(WorldOpcodes.CMSG_CHAR_CREATE, OnCharCreate);
         WorldDataRouter.AddHandler<PCCharDelete>(WorldOpcodes.CMSG_CHAR_DELETE, OnCharDelete);

         Logger.Log(LogType.Information, "CharacterManager Initialized.");
      }

      private static void OnCharDelete(WorldSession session, PCCharDelete packet)
      {
         //DBCharacters.DeleteCharacter(packet.Character);
         session.SendPacket(new PSCharDelete(LoginErrorCode.CHAR_DELETE_SUCCESS));
      }

      private static void OnCharCreate(WorldSession session, PCCharCreate packet)
      {
         CharacterCreationInfo newCharacterInfo = Database20.Characters.CharacterManager.GetCreationInfo((RaceID)packet.Race, (ClassID)packet.Class);


         Database20.Characters.CharacterManager.CreateCharacter(session.Account, new Character()
         {
            name = Helper.NormalizeText(packet.Name),
            race = (RaceID)packet.Race,
            @class = (ClassID)packet.Class,
            gender = packet.Gender,
            playerints = BitConverter.ToUInt32(new[] { packet.Skin, packet.Face, packet.HairStyle, packet.HairColor }, 0),
            playerints2 = (byte)packet.Accessory,
            money = 100000,
            level = 1,
            health = 1000,
            drunk = 100,
            online = 0,
            map = newCharacterInfo.Map,
            zone = newCharacterInfo.Zone,
            position_x = newCharacterInfo.X, //1235.54f, //- 2917.580078125f,
            position_y = newCharacterInfo.Y, //1427.1f, //- 257.980010986328f,
            position_z = newCharacterInfo.Z, //309.715f, //52.9967994689941f,
            orientation = newCharacterInfo.R,
            equipmentCache = GetStartingEquipment(packet.Race, packet.Class, packet.Gender),
         });

         session.SendPacket(new PSCharCreate(LoginErrorCode.CHAR_CREATE_SUCCESS));
      }

      private static unsafe string GetStartingEquipment(byte Race, byte Class, byte Gender)
      {
         CharStartOutfit entry = CobolWow.DBC.GetDBC<CharStartOutfit>().SingleOrDefault(item => (byte)item.Class == Class && (byte)item.Race == Race && (byte)item.Gender == Gender);
         var result = "";
         for (int i = 0; i < 12; i++)
         {
            result += entry.ItemId[i];
            if (i != 11) result += ",";
         }
         return result;
      }

      private static void OnCharEnum(WorldSession session, byte[] packet)
      {
         List<Character> characters = Database20.Characters.CharacterManager.GetCharacters(session.Account.username);
         Logger.Log(LogType.Debug, "Characters retrieved: " + characters.Count);
         session.SendPacket(WorldOpcodes.SMSG_CHAR_ENUM, new PSCharEnum(characters).PacketData);
      }
   }
}
