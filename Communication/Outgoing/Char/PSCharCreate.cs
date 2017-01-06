using CobolWow.Network;
using CobolWow.Game.Constants.Login;

namespace CobolWow.Communication.Outgoing.Char
{
   class PSCharCreate : ServerPacket
   {
      public PSCharCreate(LoginErrorCode code) : base(WorldOpcodes.SMSG_CHAR_CREATE)
      {
         Write((byte)code);
      }
   }
}
