using System;
using CobolWow.Network.Packets;

namespace CobolWow.Communication.Incoming.World.Player
{
   public class PCSetSelection : PacketReader
   {
      public UInt64 GUID { get; private set; }

      public PCSetSelection(byte[] data) : base(data)
      {
         GUID = ReadUInt64();
      }
   }
}
