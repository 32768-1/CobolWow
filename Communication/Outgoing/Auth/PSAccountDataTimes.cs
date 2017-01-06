using CobolWow.Network;
using CobolWow.Tools.Extensions;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSAccountDataTimes : ServerPacket
   {
      public PSAccountDataTimes() : base(WorldOpcodes.SMSG_ACCOUNT_DATA_TIMES)
      {
         this.WriteNullByte(128);
      }
   }
}
