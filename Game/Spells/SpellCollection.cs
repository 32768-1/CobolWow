﻿using System.Collections;
using System.Collections.Generic;

using CobolWow.Tools.DBC;
using CobolWow.Game.Entitys;
using CobolWow.Tools.DBC.Tables;
using CobolWow.Tools.Database.Tables;
using CobolWow.Tools.Database.Helpers;
using CobolWow.Game.Constants.Game.World.Spell;
using CobolWow.Communication.Outgoing.World.Spell;

namespace CobolWow.Game.Spells
{
   public class SpellCollection : IEnumerable<Spell>
   {
      protected Dictionary<SpellID, Spell> Collection;

      public PlayerEntity Owner { get; private set; }

      public SpellCollection(PlayerEntity PlayerEntity)
      {
         Owner = PlayerEntity;
         Collection = new Dictionary<SpellID, Spell>();
         GetDBSpells(Owner.Character).ForEach(s => Collection.Add(s.SpellID, s));
      }

      public int Count { get { return Collection.Count; } }

      public void AddSpell(int SpellID)
      {
         AddSpell(CreateSpell(SpellID));
      }

      public void AddSpell(SpellEntry spellEntry)
      {
         AddSpell(CreateSpell(spellEntry.ID));
      }

      public void AddSpell(Spell Spell)
      {
         if (Collection.ContainsKey(Spell.SpellID)) return;
         Collection.Add(Spell.SpellID, Spell);
         DBSpells.AddSpell(Owner.Character, (int)Spell.SpellID);

         Owner.Session.SendPacket(new PSLearnSpell((uint)Spell.SpellID));
      }

      public void RemoveSpell(Spell Spell)
      {
         if (!Collection.ContainsKey(Spell.SpellID)) return;
         Collection.Remove(Spell.SpellID);
         DBSpells.RemoveSpell(Owner.Character, (int)Spell.SpellID);

         Owner.Session.SendPacket(new PSRemoveSpell((uint)Spell.SpellID));
      }

      private Spell CreateSpell(int spellID)
      {
         SpellEntry spellEntry = DBC.Spells.GetSpellByID(spellID);
         return new Spell((SpellID)spellID, spellEntry);
      }

      private List<Spell> GetDBSpells(Character character)
      {
         List<Spell> Spells = new List<Spell>();
         DBSpells.GetCharacterSpells(character).ForEach(characterSpell => Spells.Add(CreateSpell(characterSpell.SpellID)));
         return Spells;
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public IEnumerator<Spell> GetEnumerator()
      {
         foreach (var spell in Collection.Values)
         {
            yield return spell;
         }
      }
   }
}