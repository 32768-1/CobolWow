using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSSetRestStart : ServerPacket
   {
      //TODO Implement
      public PSSetRestStart() : base(WorldOpcodes.SMSG_SET_REST_START)
      {
         Write((byte)0);
      }
   }
}
