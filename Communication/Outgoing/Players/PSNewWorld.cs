using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Players
{
   public class PSNewWorld : ServerPacket
   {
      public PSNewWorld(int map, float X, float Y, float Z, float R) : base(WorldOpcodes.SMSG_NEW_WORLD)
      {
         Write(map);
         Write(X);
         Write(Y);
         Write(Z);
         Write(R);
      }
   }
}
