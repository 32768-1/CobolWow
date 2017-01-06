using CobolWow.Game.Constants.Login;
using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSAuthResponse : ServerPacket
   {
      public PSAuthResponse() : base(WorldOpcodes.SMSG_AUTH_RESPONSE)
      {
         Write((byte)ResponseCodes.AUTH_OK);
      }
   }
}
