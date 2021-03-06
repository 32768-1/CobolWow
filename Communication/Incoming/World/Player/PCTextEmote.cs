﻿using CobolWow.Network.Packets;

namespace CobolWow.Communication.Incoming.World.Player
{
   public class PCTextEmote : PacketReader
   {
      public uint TextID { get; private set; }
      public uint EmoteID { get; private set; }
      public int GUID { get; private set; }

      public PCTextEmote(byte[] data) : base(data)
      {
         TextID = ReadUInt32();
         EmoteID = ReadUInt32();
         GUID = ReadInt32();
      }
   }
}
