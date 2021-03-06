﻿using CobolWow.Network.Packets;

namespace CobolWow.Communication.Incoming.World.Player
{
   public class PCAreaTrigger : PacketReader
   {
      public uint TriggerID { get; private set; }

      public PCAreaTrigger(byte[] data) : base(data)
      {
         TriggerID = ReadUInt32();
      }
   }
}
