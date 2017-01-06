using CobolWow.Network.Packets;
using CobolWow.Tools.Extensions;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSTutorialFlags : ServerPacket
   {
      //TODO Write the uint ids of 8 tutorial values
      public PSTutorialFlags() : base(WorldOpcodes.SMSG_TUTORIAL_FLAGS)
      {
         this.WriteNullUInt(8);
      }
   }
}
