using System;

using CobolWow.Tools.Config;
using CobolWow.Network.Packets;
using CobolWow.Tools.Extensions;
using CobolWow.Game.Constants.Login;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSRealmList : ServerPacket
   {
      public PSRealmList() : base(LoginOpcodes.REALM_LIST)
      {
         Write((uint)0x0000);
         Write((byte)1);
         Write((uint)RealmType.PVP);
         Write((byte)0);
         this.WriteCString(ConfigManager.WORLDNAME);
         this.WriteCString(ConfigManager.WORLDIP + ":" + ConfigManager.WORLDPORT);
         Write((float)ConfigManager.WORLDPOPULATION); // Pop
         Write((byte)3); // Chars
         Write((byte)1); // time
         Write((byte)0); // time
         Write((UInt16)0x0002);
      }
   }
}
