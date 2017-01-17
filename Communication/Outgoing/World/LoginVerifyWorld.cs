using CobolWow.Network;
using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.World
{
   public class LoginVerifyWorld : ServerPacket
   {
      public LoginVerifyWorld(int map, float X, float Y, float Z, float orientation) : base(WorldOpcodes.SMSG_LOGIN_VERIFY_WORLD)
      {
         Write(map);
         Write(X);
         Write(Y);
         Write(Z);
         Write(orientation); // orientation
      }
   }
}
