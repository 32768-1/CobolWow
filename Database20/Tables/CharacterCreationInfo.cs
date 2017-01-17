using CobolWow.Game.Constants;

namespace CobolWow.Tools.Database20.Tables
{
   public class CharacterCreationInfo
   {
      public RaceID Race { get; set; }
      public ClassID Class { get; set; }
      public int Map { get; set; }
      public int Zone { get; set; }
      public float X { get; set; }
      public float Y { get; set; }
      public float Z { get; set; }
      public float R { get; set; }
   }
}
