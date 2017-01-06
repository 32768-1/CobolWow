using System.Timers;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Tools;
using CobolWow.Tools.DBC;
using CobolWow.Game.Spells;
using CobolWow.Game.Entitys;
using CobolWow.Game.Handlers;
using CobolWow.Communication;
using CobolWow.Tools.DBC.Tables;
using CobolWow.Tools.Database.Tables;
using CobolWow.Communication.Incoming.World.Spell;
using CobolWow.Communication.Outgoing.World.Spell;

namespace CobolWow.Game.Managers
{
   class SpellManager
   {
      public Dictionary<Character, SpellCollection> SpellCollections = new Dictionary<Character, SpellCollection>();
      public static void Boot()
      {
         WorldDataRouter.AddHandler<PCCastSpell>(WorldOpcodes.CMSG_CAST_SPELL, OnCastSpell);
         WorldDataRouter.AddHandler<PCCancelSpell>(WorldOpcodes.CMSG_CANCEL_CAST, OnCancelSpell);

         Logger.Log(LogType.Information, "SpellManager Initialized.");
      }

      public static void SendInitialSpells(WorldSession session)
      {
         Logger.Log(LogType.Debug, "SpellManager SendInitialSpells.");
         //TODO Fix spellCollection DBC
         session.SendPacket(new PSInitialSpells(session.Entity.SpellCollection));
      }

      private static void OnCastSpell(WorldSession session, PCCastSpell packet)
      {
         Logger.Log(LogType.Debug, "SpellManager OnCastSpell.");

         PrepareSpell(session, packet);
         ObjectEntity target = (session.Entity.Target != null) ? session.Entity.Target : session.Entity;

         //WorldServer.TransmitToAll(new PSSpellGo(session.Entity, target, packet.spellID));
         session.SendPacket(new PSCastFailed(packet.spellID));

         SpellEntry spell = DBC.Spells.GetSpellByID((int)packet.spellID);
         float spellSpeed = spell.speed;

         /*
         float distance =  (float)Math.Sqrt((session.Entity.X - session.Entity.Target.X) * (session.Entity.X - session.Entity.Target.X) +
                                            (session.Entity.Y - session.Entity.Target.Y) * (session.Entity.Y - session.Entity.Target.Y) +
                                            (session.Entity.Z - session.Entity.Target.Z) * (session.Entity.Z - session.Entity.Target.Z));

         if (distance < 5) distance = 5;

         float dx = session.Entity.X - target.X;
         float dy = session.Entity.Y - target.Y;
         float dz = session.Entity.Z - target.Target.Z;
         float radius = 5;
         float distance = (float)Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz)) - radius;

         //if (distance < 5) distance = 5;
         float timeToHit = (spellSpeed > 0) ? (float)Math.Floor(distance / spellSpeed * 1000f) : 0;

         session.sendMessage("Cast [" + spell.Name + "] Distance: " + distance + " Speed: " + spellSpeed + " Time: " + timeToHit);
         float radians = (float)(Math.Atan2(session.Entity.Y - session.Entity.Target.Y, session.Entity.X - session.Entity.Target.X));

         if(spellSpeed > 0)
         {
             DoTimer(timeToHit, (s, e) =>
             {
                 WorldServer.TransmitToAll(new PSMoveKnockBack(target, (float)Math.Cos(radians), (float)Math.Sin(radians), -10, -10));
             });
         }


        */
      }

      private static void PrepareSpell(WorldSession session, PCCastSpell packet)
      {
         UnitEntity target = session.Entity.Target ?? session.Entity;
      }

      public static void DoTimer(double interval, ElapsedEventHandler elapseEvent)
      {
         Timer aTimer = new Timer(interval);
         aTimer.Elapsed += new ElapsedEventHandler(elapseEvent);
         aTimer.Elapsed += new ElapsedEventHandler((s, e) => ((Timer)s).Stop());
         aTimer.Start();
      }

      private static void OnCancelSpell(WorldSession session, PCCancelSpell packet)
      {
         Logger.Log(LogType.Debug, "SpellManager OnCancelSpell - Todo.");
      }

      public static void OnLearnSpell(WorldSession session, int spellID)
      {
         Logger.Log(LogType.Debug, "SpellManager OnLearnSpell");
         session.Entity.SpellCollection.AddSpell(spellID);
      }
   }
}
