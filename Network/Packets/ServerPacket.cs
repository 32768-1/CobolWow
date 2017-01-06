using System.IO;
using CobolWow.Communication;

namespace CobolWow.Network.Packets
{
   public class ServerPacket : BinaryWriter
   {
      public int Opcode;

      public ServerPacket(int opcode) : base(new MemoryStream())
      {
         Opcode = opcode;
      }

      public ServerPacket(WorldOpcodes worldOpcode) : this((int)worldOpcode) { }

      public ServerPacket(LoginOpcodes opcode) : this((byte)opcode) { }

      public byte[] Packet
      {
         get { return (BaseStream as MemoryStream).ToArray(); }
      }
   }
}
