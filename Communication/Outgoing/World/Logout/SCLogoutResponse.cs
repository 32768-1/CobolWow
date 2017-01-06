using System;
using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.World.Logout
{
   public class SCLogoutResponse : ServerPacket
   {
      public SCLogoutResponse() : base(WorldOpcodes.SMSG_LOGOUT_RESPONSE)
      {
         Write((UInt32)0);
         Write((byte)0);
      }
   }
}
