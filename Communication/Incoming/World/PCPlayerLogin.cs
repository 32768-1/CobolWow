using CobolWow.Network.Packets;
namespace CobolWow.Communication.Incoming.World
{
   public class PCPlayerLogin : PacketReader
    {
        public uint GUID { get; private set; }

        public PCPlayerLogin(byte[] data) : base(data)
        {
            GUID = ReadUInt32();
        }
    }
}
