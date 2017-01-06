using CobolWow.Network;
using CobolWow.Game.Constants.Login;

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
