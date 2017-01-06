using CobolWow.Network;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSLogoutCancelAcknowledgement : ServerPacket
   {
      public PSLogoutCancelAcknowledgement() : base(WorldOpcodes.SMSG_LOGOUT_CANCEL_ACK)
      {
         Write((byte)0);
      }
   }
}
