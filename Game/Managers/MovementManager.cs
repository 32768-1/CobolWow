using CobolWow.Net;
using CobolWow.Tools;
using CobolWow.Network;
using CobolWow.Game.Entitys;
using CobolWow.Game.Handlers;
using CobolWow.Communication;
using System.Collections.Generic;
using CobolWow.Tools.Database.Helpers;
using CobolWow.Communication.Outgoing.World.Update;
using CobolWow.Communication.Incoming.World.Movement;
using CobolWow.Communication.Outgoing.World.Movement;

namespace CobolWow.Game.Managers
{
   public class MovementManager
   {
      private static readonly List<WorldOpcodes> MovementOpcodes = new List<WorldOpcodes>()
        {
            WorldOpcodes.MSG_MOVE_HEARTBEAT,
            WorldOpcodes.MSG_MOVE_JUMP,
            WorldOpcodes.MSG_MOVE_START_FORWARD,
            WorldOpcodes.MSG_MOVE_START_BACKWARD,
            WorldOpcodes.MSG_MOVE_SET_FACING,
            WorldOpcodes.MSG_MOVE_STOP,
            WorldOpcodes.MSG_MOVE_START_STRAFE_LEFT,
            WorldOpcodes.MSG_MOVE_START_STRAFE_RIGHT,
            WorldOpcodes.MSG_MOVE_STOP_STRAFE,
            WorldOpcodes.MSG_MOVE_START_TURN_LEFT,
            WorldOpcodes.MSG_MOVE_START_TURN_RIGHT,
            WorldOpcodes.MSG_MOVE_STOP_TURN,
            WorldOpcodes.MSG_MOVE_START_PITCH_UP,
            WorldOpcodes.MSG_MOVE_START_PITCH_DOWN,
            WorldOpcodes.MSG_MOVE_STOP_PITCH,
            WorldOpcodes.MSG_MOVE_SET_RUN_MODE,
            WorldOpcodes.MSG_MOVE_SET_WALK_MODE,
            WorldOpcodes.MSG_MOVE_SET_PITCH,
            WorldOpcodes.MSG_MOVE_START_SWIM,
            WorldOpcodes.MSG_MOVE_STOP_SWIM,
            WorldOpcodes.MSG_MOVE_FALL_LAND,
            WorldOpcodes.MSG_MOVE_HOVER,
            WorldOpcodes.MSG_MOVE_KNOCK_BACK
        };

      public static void Boot()
      {
         foreach (WorldOpcodes movementOpcode in MovementOpcodes)
            WorldDataRouter.AddHandler<PCMoveInfo>(movementOpcode, GenerateResponse(movementOpcode));

         WorldDataRouter.AddHandler(WorldOpcodes.CMSG_MOVE_TIME_SKIPPED, OnMoveTimeSkipped);
         WorldDataRouter.AddHandler(WorldOpcodes.MSG_MOVE_WORLDPORT_ACK, OnWorldPort);

         Logger.Log(LogType.Information, "MovementManager Initialized.");
      }

      private static void OnWorldPort(WorldSession session, byte[] data)
      {
         PlayerEntity player;
         PSUpdateObject updateObj = PSUpdateObject.CreateOwnCharacterUpdate(session.Character, out player);
         session.SendPacket(updateObj);
         player.Session = session;         
      }

      private static void OnMoveTimeSkipped(WorldSession session, byte[] packet)
      {
         Logger.Log(LogType.Warning, "MovementManager MoveTimeSkipped.");
         using (PacketReader reader = new PacketReader(packet))
         {
            PSUpdateObject.ReadPackedGuid(reader);
            session.OutOfSyncDelay = reader.ReadUInt32();
         }
      }

      private static void SavePosition(WorldSession session, PCMoveInfo handler)
      {
         session.Character.X = handler.X;
         session.Character.Y = handler.Y;
         session.Character.Z = handler.Z;
         session.Character.Rotation = handler.R;

         DBCharacters.UpdateCharacter(session.Character);
      }

      private static void UpdateEntity(WorldSession session, PCMoveInfo handler)
      {
         session.Entity.X = handler.X;
         session.Entity.Y = handler.Y;
         session.Entity.Z = handler.Z;
      }

      private static ProcessWorldPacketCallbackTypes<PCMoveInfo> GenerateResponse(WorldOpcodes worldOpcode)
      {
         return delegate (WorldSession session, PCMoveInfo handler) { TransmitMovement(session, handler, worldOpcode); };
      }

      private static void TransmitMovement(WorldSession session, PCMoveInfo handler, WorldOpcodes code)
      {
         if (code == WorldOpcodes.MSG_MOVE_HEARTBEAT)
            SavePosition(session, handler);

         session.Entity.X = handler.X;
         session.Entity.Y = handler.Y;
         session.Entity.Z = handler.Z;

         UpdateEntity(session, handler);

         foreach (WorldSession entity in World.SessionsWhoKnow(session.Entity))
            entity.SendPacket(new PSMovement(code, session, handler)); //Each packet gets disposed after sent, cannot re use.
      }
   }
}
