using CobolWow.Network;

namespace CobolWow.Communication.Outgoing.Players
{
   public class ForceRunSpeedChange : ServerPacket
   {
      //Todo
      public ForceRunSpeedChange(uint GUID, float speed) : base(WorldOpcodes.SMSG_FORCE_RUN_SPEED_CHANGE)
      {
         Write((ulong)GUID);
         Write((uint)0);
         Write(speed);
      }
   }
}
