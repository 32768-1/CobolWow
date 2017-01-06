using CobolWow.Network.Packets;

namespace CobolWow.Communication.Incoming.World.Mail
{
   public class PCGetMailList : PacketReader
   {
      public uint GUID { get; private set; }

      public PCGetMailList(byte[] data) : base(data)
      {
         GUID = ReadUInt32();
      }
   }
}
