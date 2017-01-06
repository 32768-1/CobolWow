using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Players
{
   public class PSTransferPending : ServerPacket
   {
      public PSTransferPending(int mapID) : base(WorldOpcodes.SMSG_TRANSFER_PENDING)
      {
         Write(mapID);
      }
   }
}
