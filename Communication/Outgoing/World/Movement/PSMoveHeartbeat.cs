using System;
using CobolWow.Network.Packets;
using CobolWow.Database20.Tables;
using CobolWow.Game.Constants.Game.Update;
using CobolWow.Communication.Outgoing.World.Update;

namespace CobolWow.Communication.Outgoing.World.Movement
{
   public class PSMoveHeartbeat : ServerPacket
   {
      public PSMoveHeartbeat(Character character) : base(WorldOpcodes.MSG_MOVE_HEARTBEAT)
      {
         byte[] packedGUID = PSUpdateObject.GenerateGuidBytes((ulong)character.guid);
         PSUpdateObject.WriteBytes(this, packedGUID);
         Write((UInt32)MovementFlags.MOVEFLAG_NONE);
         Write((UInt32)1); // Time
         Write(character.position_x);
         Write(character.position_y);
         Write(character.position_z);
         Write(character.orientation);
         Write((UInt32)0); // ?
      }
   }
}
