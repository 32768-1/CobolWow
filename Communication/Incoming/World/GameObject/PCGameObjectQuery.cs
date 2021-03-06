﻿using CobolWow.Network.Packets;

namespace CobolWow.Communication.Incoming.World.GameObject
{
   public class PCGameObjectQuery : PacketReader
   {
      public uint EntryID { get; private set; }
      public uint GUID { get; private set; }

      public PCGameObjectQuery(byte[] data) : base(data)
      {
         EntryID = ReadUInt32();
         GUID = ReadUInt32();
      }
   }
}
