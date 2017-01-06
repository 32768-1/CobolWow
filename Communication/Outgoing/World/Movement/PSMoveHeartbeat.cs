using System;
using CobolWow.Network;
using CobolWow.Tools.Database.Tables;
using CobolWow.Game.Constants.Game.Update;
using CobolWow.Communication.Outgoing.World.Update;

namespace CobolWow.Communication.Outgoing.World.Movement
{
   public class PSMoveHeartbeat : ServerPacket
   {
      public PSMoveHeartbeat(Character character) : base(WorldOpcodes.MSG_MOVE_HEARTBEAT)
      {
         byte[] packedGUID = PSUpdateObject.GenerateGuidBytes((ulong)character.GUID);
         PSUpdateObject.WriteBytes(this, packedGUID);
         Write((UInt32)MovementFlags.MOVEFLAG_NONE);
         Write((UInt32)1); // Time
         Write(character.X);
         Write(character.Y);
         Write(character.Z);
         Write(character.Rotation);
         Write((UInt32)0); // ?
      }
   }
}
