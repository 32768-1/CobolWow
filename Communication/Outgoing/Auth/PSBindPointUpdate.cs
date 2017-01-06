﻿using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSBindPointUpdate : ServerPacket
   {
      public PSBindPointUpdate() : base(WorldOpcodes.SMSG_BINDPOINTUPDATE)
      {
         //TODO Pull hearthstone info from DB.
         Write((float)1); //X
         Write((float)1); //Y
         Write((float)1); //Z
         Write((uint)1); //MAPID
         Write((short)1); //AREAID
      }
   }
}
