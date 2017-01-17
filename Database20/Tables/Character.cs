using CobolWow.Game.Constants;
using Dapper.Contrib.Extensions;
using System;

namespace CobolWow.Database20.Tables
{
   [Table("character")]
   public class Character
   {
      public int guid { get; set; }
      public int account { get; set; }
      public string name { get; set; }
      public RaceID race { get; set; }
      public ClassID @class { get; set; }
      public int gender { get; set; }
      public int level { get; set; }
      public long xp { get; set; }
      public int money { get; set; }
      public long playerints { get; set; }
      public long playerints2 { get; set; }
      public long playerFlags { get; set; }
      public float position_x { get; set; }
      public float position_y { get; set; }
      public float position_z { get; set; }
      public int map { get; set; }
      public float orientation { get; set; }
      public string taximask { get; set; }
      public int online { get; set; }
      public int cinematic { get; set; }
      public long totaltime { get; set; }
      public long leveltime { get; set; }
      public decimal logout_time { get; set; }
      public int is_logout_resting { get; set; }
      public float rest_bonus { get; set; }
      public long resettalents_cost { get; set; }
      public decimal resettalents_time { get; set; }
      public float trans_x { get; set; }
      public float trans_y { get; set; }
      public float trans_z { get; set; }
      public float trans_o { get; set; }
      public decimal transguid { get; set; }
      public long extra_flags { get; set; }
      public bool stable_slots { get; set; }
      public long at_login { get; set; }
      public long zone { get; set; }
      public decimal death_expire_time { get; set; }
      public string taxi_path { get; set; }
      public long honor_highest_rank { get; set; }
      public long honor_standing { get; set; }
      public float stored_honor_rating { get; set; }
      public int stored_dishonorable_kills { get; set; }
      public int stored_honorable_kills { get; set; }
      public long watchedFaction { get; set; }
      public int drunk { get; set; }
      public long health { get; set; }
      public long power1 { get; set; }
      public long power2 { get; set; }
      public long power3 { get; set; }
      public long power4 { get; set; }
      public long power5 { get; set; }
      public string exploredZones { get; set; }
      public string equipmentCache { get; set; }
      public long ammoId { get; set; }
      public int actionBars { get; set; }
      public Nullable<long> deleteInfos_Account { get; set; }
      public string deleteInfos_Name { get; set; }
      public Nullable<decimal> deleteDate { get; set; }
   }
}
