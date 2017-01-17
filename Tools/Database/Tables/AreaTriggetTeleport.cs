namespace CobolWow.Tools.Database.Tables
{
   public class AreaTriggerTeleport
   {
      public int id { get; set; }
      public string name { get; set; }
      public byte required_level { get; set; }
      public int required_item { get; set; }
      public int required_item2 { get; set; }
      public long required_quest_done { get; set; }
      public int target_map { get; set; }
      public float target_position_x { get; set; }
      public float target_position_y { get; set; }
      public float target_position_z { get; set; }
      public float target_orientation { get; set; }
   }
}
