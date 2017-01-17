using System;
using System.Linq;
using CobolWow.Net;
using CobolWow.Tools;
using CobolWow.Network;
using CobolWow.Game.Entitys;
using CobolWow.Game.Handlers;
using CobolWow.Communication;
using CobolWow.Tools.Database.Tables;
using CobolWow.Communication.Outgoing.World;
using CobolWow.Communication.Incoming.World;
using CobolWow.Communication.Outgoing.Players;
using CobolWow.Game.Constants.Game.World.Entity;
using CobolWow.Communication.Incoming.World.Player;
using CobolWow.Communication.Outgoing.World.Player;
using CobolWow.Communication.Incoming.World.GameObject;
using CobolWow.Database20.Tables;
using CobolWow.DBC.Structs;

namespace CobolWow.Game.Managers
{
   public class MiscManager
   {
      public static void Boot()
      {
         WorldDataRouter.AddHandler<PCNameQuery>(WorldOpcodes.CMSG_NAME_QUERY, OnNameQueryPacket);
         WorldDataRouter.AddHandler<PCTextEmote>(WorldOpcodes.CMSG_TEXT_EMOTE, OnTextEmotePacket);
         WorldDataRouter.AddHandler<PCEmote>(WorldOpcodes.CMSG_EMOTE, OnEmotePacket);
         WorldDataRouter.AddHandler<PCZoneUpdate>(WorldOpcodes.CMSG_ZONEUPDATE, OnZoneUpdatePacket);
         WorldDataRouter.AddHandler<PCAreaTrigger>(WorldOpcodes.CMSG_AREATRIGGER, OnAreaTriggerPacket);
         WorldDataRouter.AddHandler<PCPing>(WorldOpcodes.CMSG_PING, OnPingPacket);
         WorldDataRouter.AddHandler<PCSetSelection>(WorldOpcodes.CMSG_SET_SELECTION, OnSetSelectionPacket);
         WorldDataRouter.AddHandler<PCGameObjectQuery>(WorldOpcodes.CMSG_GAMEOBJECT_QUERY, OnGameObjectQuery);

         Logger.Log(LogType.Information, "MiscManager Initialized.");
      }

      public static void OnNameQueryPacket(WorldSession session, PCNameQuery packet)
      {
         Logger.Log(LogType.Debug, "MiscManager OnNameQueryPacket.");
         Character target = Database20.Characters.CharacterManager.GetCharacter((int)packet.GUID);

         if (target != null)
            session.SendPacket(new PSNameQueryResponse(target));
      }

      public static void OnGameObjectQuery(WorldSession session, PCGameObjectQuery packet)
      {
         Logger.Log(LogType.Debug, "MiscManager OnGameObjectQuery.");
         //GameObject_Template template = DBGameObject.GameObjectTemplates.Find(g => g.Entry == packet.EntryID);
         //session.sendPacket(new PSGameObjectQueryResponse(template));
         //session.sendMessage("Requested Info: " + template.Name + " " + (GameobjectTypes)template.Type);
      }

      private static void OnEmotePacket(WorldSession session, PCEmote packet)
      {
         Logger.Log(LogType.Debug, "MiscManager OnEmotePacket.");
         session.SendPacket(new PSEmote(packet.EmoteID, session.Entity.ObjectGUID.RawGUID));
      }

      public static void OnTextEmotePacket(WorldSession session, PCTextEmote packet)
      {
         Logger.Log(LogType.Debug, "MiscManager OnTextEmotePacket.");

         String targetName = session.Entity.Target != null ? session.Entity.Target.Name : null;
         WorldServer.TransmitToAll(new PSTextEmote((int)session.Character.guid, (int)packet.EmoteID, (int)packet.TextID, targetName));
         EmotesText textEmote = CobolWow.DBC.GetDBC<EmotesText>().SingleOrDefault(e => e.textid == packet.TextID);

         switch ((Emote)textEmote.textid)
         {
            case Emote.EMOTE_STATE_SLEEP:
            case Emote.EMOTE_STATE_SIT:
            case Emote.EMOTE_STATE_KNEEL:
            case Emote.EMOTE_ONESHOT_NONE:
               break;
            default:
               World.SessionsWhoKnow(session.Entity).ForEach(s => s.SendPacket(new PSEmote(textEmote.textid, session.Entity.ObjectGUID.RawGUID)));
               session.SendPacket(new PSEmote(textEmote.textid, session.Entity.ObjectGUID.RawGUID));
               break;
         }
      }

      public static void OnZoneUpdatePacket(WorldSession session, PCZoneUpdate packet)
      {
         Logger.Log(LogType.Debug, "MiscManager OnZoneUpdatePacket.");
         //session.SendMessage("[ZoneUpdate] ID:" + packet.ZoneID + " " + DBC.AreaTables.Find(a => a.ID == packet.ZoneID).Name);
      }

      public static void OnAreaTriggerPacket(WorldSession session, PCAreaTrigger packet)
      {
         Logger.Log(LogType.Debug, "MiscManager OnAreaTriggerPacket.");

         //AreaTriggerTeleport areaTrigger = DBAreaTriggers.AreaTriggerTeleport.Find(at => at.id == packet.TriggerID);

         //if (areaTrigger != null)
         //{
         //   session.SendMessage("[AreaTrigger] ID:" + packet.TriggerID + " " + areaTrigger.name);
         //   session.Character.map = areaTrigger.target_map;
         //   session.Character.position_x = areaTrigger.target_position_x;
         //   session.Character.position_y = areaTrigger.target_position_y;
         //   session.Character.position_z = areaTrigger.target_position_z;
         //   session.Character.orientation = areaTrigger.target_orientation;
         //   //DBCharacters.UpdateCharacter(session.Character);

         //   session.SendPacket(new PSTransferPending(areaTrigger.target_map));
         //   session.SendPacket(new PSNewWorld(areaTrigger.target_map, areaTrigger.target_position_x, areaTrigger.target_position_y, areaTrigger.target_position_z, areaTrigger.target_orientation));

         //}
         //else
         //{
         //   session.SendMessage("[AreaTrigger] ID:" + packet.TriggerID);
         //}
      }

      public static void OnPingPacket(WorldSession session, PCPing packet)
      {
         session.SendMessage("Ping: " + packet.Ping + " Latancy: " + packet.Latency);
         session.SendPacket(new PSPong(packet.Ping));
      }

      public static void OnSetSelectionPacket(WorldSession session, PCSetSelection packet)
      {
         Logger.Log(LogType.Debug, "MiscManager OnSetSelectionPacket.");

         UnitEntity target = null;

         WorldSession op = WorldServer.Sessions.Find(s => s.Entity?.ObjectGUID.RawGUID == packet.GUID);

         if (op != null)
            target = op.Entity;

         if (target == null)
            target = CobolWow.UnitComponent.Entitys.Find(e => e.ObjectGUID?.RawGUID == packet.GUID);

         if (target != null)
         {
            session.Entity.Target = target;
            session.SendMessage("Target: " + target.Name);
         }
         else
         {
            session.SendMessage("Couldnt find target!");
            session.Entity.Target = null;
         }
      }
   }
}
