using System;
using System.Linq;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Game.Entitys;
using CobolWow.Network;
using CobolWow.DBC.Structs;

namespace CobolWow.Tools.Chat.Commands
{
   [ChatCommandNode("spell", "Spell commands")]
   public class Spell
   {
      [ChatCommand("lookup", "Takes a spellName string and returns a spellID int.")]
      public static void Lookup(WorldSession session, string[] args)
      {
         throw new System.Exception("Not Implemented");
         //string spellName = args[0];

         //List<SpellEntry> matchingSpells = DBC.DBC.Spells.GetSpellsNameContain(spellName);

         //matchingSpells.ForEach(s => session.SendMessage("[" + s.ID + "] " + s.Name));
      }

      public static List<SpellEntry> LookUp(string spellName)
      {
         //return DBC.DBC.Spells.GetSpellsNameContain(spellName);
         throw new System.Exception("Not Implemented");
      }

      [ChatCommand("learn", "Takes a spellID or spellName and learns it")]
      public static void Learn(WorldSession session, string[] args)
      {
         string spellNameOrID = args[0];
         int spellID;
         if (Int32.TryParse(spellNameOrID, out spellID))
         {
            session.Entity.SpellCollection.AddSpell(spellID);
         }
         else
         {
            throw new System.Exception("Not Implemented");
            //List<SpellEntry> matchingSpells = DBC.DBC.Spells.GetSpellsNameContain(spellNameOrID);
            //if (matchingSpells.Count > 0) session.Entity.SpellCollection.AddSpell(matchingSpells.First());
            //else
            //{
            //   session.SendMessage("A spell was not found with the ID or Name '" + spellNameOrID + "'");
            //}
         }
      }

      [ChatCommand("tp", "Teleport to player")]
      public static void Teleport(WorldSession session, string[] args)
      {
         string playerName = args[0].ToLower();

         PlayerEntity player = WorldServer.Sessions.Find(s => s.Character.name.ToLower() == playerName).Entity;

         if (player != null)
         {
            session.Teleport(player.Character.map, player.X, player.Y, player.Z);
         }
         else
         {
            session.SendMessage("Cannot find player");
         }
      }
   }
}
