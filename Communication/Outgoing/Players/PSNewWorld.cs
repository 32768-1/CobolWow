using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Players
{
   public class PSNewWorld : ServerPacket
   {
      public PSNewWorld(int mapID, float X, float Y, float Z, float R) : base(WorldOpcodes.SMSG_NEW_WORLD)
      {
         Write(mapID);
         Write(X);
         Write(Y);
         Write(Z);
         Write(R);
      }
   }
}
