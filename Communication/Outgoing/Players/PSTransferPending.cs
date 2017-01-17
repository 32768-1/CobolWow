using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Players
{
   public class PSTransferPending : ServerPacket
   {
      public PSTransferPending(int map) : base(WorldOpcodes.SMSG_TRANSFER_PENDING)
      {
         Write(map);
      }
   }
}
