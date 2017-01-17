﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobolWow.Database20.Tables
{
   [Table("creature_template")]
   public partial class CreatureTemplate
   {
      public int Entry { get; set; }
      public string Name { get; set; }
      public string SubName { get; set; }
      public byte MinLevel { get; set; }
      public byte MaxLevel { get; set; }
      public int ModelId1 { get; set; }
      public int ModelId2 { get; set; }
      public int FactionAlliance { get; set; }
      public int FactionHorde { get; set; }
      public float Scale { get; set; }
      public sbyte Family { get; set; }
      public byte CreatureType { get; set; }
      public byte InhabitType { get; set; }
      public byte RegenerateHealth { get; set; }
      public byte RacialLeader { get; set; }
      public long NpcFlags { get; set; }
      public long UnitFlags { get; set; }
      public long DynamicFlags { get; set; }
      public long ExtraFlags { get; set; }
      public long CreatureTypeFlags { get; set; }
      public float SpeedWalk { get; set; }
      public float SpeedRun { get; set; }
      public byte UnitClass { get; set; }
      public byte Rank { get; set; }
      public float HealthMultiplier { get; set; }
      public float ManaMultiplier { get; set; }
      public float DamageMultiplier { get; set; }
      public float DamageVariance { get; set; }
      public float ArmorMultiplier { get; set; }
      public float ExperienceMultiplier { get; set; }
      public long MinLevelHealth { get; set; }
      public long MaxLevelHealth { get; set; }
      public long MinLevelMana { get; set; }
      public long MaxLevelMana { get; set; }
      public float MinMeleeDmg { get; set; }
      public float MaxMeleeDmg { get; set; }
      public float MinRangedDmg { get; set; }
      public float MaxRangedDmg { get; set; }
      public int Armor { get; set; }
      public long MeleeAttackPower { get; set; }
      public int RangedAttackPower { get; set; }
      public long MeleeBaseAttackTime { get; set; }
      public long RangedBaseAttackTime { get; set; }
      public sbyte DamageSchool { get; set; }
      public int MinLootGold { get; set; }
      public int MaxLootGold { get; set; }
      public int LootId { get; set; }
      public int PickpocketLootId { get; set; }
      public int SkinningLootId { get; set; }
      public long KillCredit1 { get; set; }
      public long KillCredit2 { get; set; }
      public long MechanicImmuneMask { get; set; }
      public short ResistanceHoly { get; set; }
      public short ResistanceFire { get; set; }
      public short ResistanceNature { get; set; }
      public short ResistanceFrost { get; set; }
      public short ResistanceShadow { get; set; }
      public short ResistanceArcane { get; set; }
      public int PetSpellDataId { get; set; }
      public byte MovementType { get; set; }
      public sbyte TrainerType { get; set; }
      public int TrainerSpell { get; set; }
      public byte TrainerClass { get; set; }
      public byte TrainerRace { get; set; }
      public int TrainerTemplateId { get; set; }
      public int VendorTemplateId { get; set; }
      public int GossipMenuId { get; set; }
      public int EquipmentTemplateId { get; set; }
      public byte Civilian { get; set; }
      public string AIName { get; set; }
      public string ScriptName { get; set; }
   }
}