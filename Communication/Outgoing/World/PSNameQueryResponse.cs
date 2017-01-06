﻿using System.Text;
using CobolWow.Network.Packets;
using CobolWow.Tools.Database.Tables;

namespace CobolWow.Communication.Outgoing.World
{
   public class PSNameQueryResponse : ServerPacket
   {
      public PSNameQueryResponse(Character character) : base(WorldOpcodes.SMSG_NAME_QUERY_RESPONSE)
      {
         Write((ulong)character.GUID);
         Write(Encoding.UTF8.GetBytes(character.Name + '\0'));
         Write((byte)0); // realm name for cross realm BG usage
         Write((uint)character.Race);
         Write((uint)character.Gender);
         Write((uint)character.Class);
      }
   }
}
