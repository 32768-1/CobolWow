﻿using CobolWow.DBC.Structs;
using CobolWow.Game.Constants.Game.World.Spell;

namespace CobolWow.Game.Spells
{
   public class Spell
   {
      public SpellID SpellID { get; private set; }

      public int Catagory { get; private set; }

      public int CooldownCatagory { get; private set; }

      public int Cooldown { get; private set; }

      public int Range { get; private set; }

      public bool IsPassive { get; private set; }

      public string Name { get; private set; }

      public Spell(SpellID spellID, SpellEntry spellEntry)
      {
         SpellID = spellID;
         Name = ""; //Todo?
         Cooldown = spellEntry.RecoveryTime;
         CooldownCatagory = spellEntry.CategoryRecoveryTime;
         Catagory = spellEntry.Category;
         Range = spellEntry.rangeIndex > 0 ? spellEntry.rangeIndex : 30;
         IsPassive = spellEntry.Attributes.HasFlag(SpellAttributes.SPELL_ATTR_PASSIVE);
      }
   }
}
