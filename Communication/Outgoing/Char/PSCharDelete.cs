using CobolWow.Network;
using CobolWow.Game.Constants.Login;

namespace CobolWow.Communication.Outgoing.Char
{
   class PSCharDelete : ServerPacket
   {
      public PSCharDelete(LoginErrorCode code) : base(WorldOpcodes.SMSG_CHAR_DELETE)
      {
         Write((byte)code);
      }
   }
}
