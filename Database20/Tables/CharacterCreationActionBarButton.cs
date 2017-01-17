using CobolWow.Game.Constants;

namespace CobolWow.Database20.Tables
{
   class CharacterCreationActionBarButton
   {
      public RaceID race { get; set; }
      public ClassID @class { get; set; }
      public int button { get; set; }
      public int action { get; set; }
      public int type { get; set; }
   }
}
