using CobolWow.Network.Packets;

namespace CobolWow.Communication.Incoming.World
{
   public class PCNameQuery : PacketReader
   {
      public uint GUID { get; private set; }

      public PCNameQuery(byte[] data) : base(data)
      {
         GUID = ReadUInt32();
      }
   }
}
