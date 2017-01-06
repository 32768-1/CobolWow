using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.World.Player
{
   public class PSPong : ServerPacket
   {
      public PSPong(uint ping) : base(WorldOpcodes.SMSG_PONG)
      {
         Write((ulong)ping);
      }
   }
}
