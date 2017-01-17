using System.IO;
using System.Collections.Generic;

using CobolWow.Tools;
using CobolWow.Network.Packets;
using CobolWow.Database20.Items;
using CobolWow.Database20.Tables;
using System;

namespace CobolWow.Communication.Outgoing.Char
{
   public class PSCharEnum : PacketWriter
   {
      public PSCharEnum(List<Character> characters) : base(Game.Constants.PacketHeaderType.WorldServerMessage)
      {
         Write((byte)characters.Count);

         foreach (Character character in characters)
         {
            Write((ulong)character.guid);
            WriteCString(character.name);
            Write((byte)character.race);
            Write((byte)character.@class);

            Write((byte)character.gender);

            Byte[] playerBytes = BitConverter.GetBytes(character.playerints);
            Byte[] playerBytes2 = BitConverter.GetBytes(character.playerints2);

            Write((byte)playerBytes[0]);
            Write((byte)playerBytes[1]);
            Write((byte)playerBytes[2]);
            Write((byte)playerBytes[3]);
            Write((byte)playerBytes2[0]);
            Write((byte)character.level);

            Write((int)0); // Zone ID
            Write(character.map);
            Write(character.position_x);
            Write(character.position_y);
            Write(character.position_z);

            Write((int)0); // Guild ID
            Write((int)0); // Character Flags

            Write((byte)0); // // Login Flags?

            Write(0); // Pet DisplayID
            Write(0); // Pet Level
            Write(0); // Pet FamilyID

            ItemTemplateEntry[] Equipment =  ItemManager.GenerateInventoryByIDs(Helper.CSVStringToIntArray(character.equipmentCache));

            for (int itemSlot = 0; itemSlot < 19; itemSlot++)
            {
               if (Equipment[itemSlot] != null)
               {
                  Write((int)Equipment[itemSlot].displayid); // Item DisplayID
                  Write((byte)Equipment[itemSlot].InventoryType); // Item Inventory Type
               }
               else
               {
                  Write((int)0);
                  Write((byte)0);
               }
            }

            Write((int)0); // first bag display id
            Write((byte)0); // first bag inventory type
         }
      }

      public byte[] Packet { get { return (BaseStream as MemoryStream).ToArray(); } }
   }
}
