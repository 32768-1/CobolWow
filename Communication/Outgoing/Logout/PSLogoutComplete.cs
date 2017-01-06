using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSLogoutComplete : ServerPacket
   {
      public PSLogoutComplete() : base(WorldOpcodes.SMSG_LOGOUT_COMPLETE)
      {
         Write((byte)0);
      }
   }
}
