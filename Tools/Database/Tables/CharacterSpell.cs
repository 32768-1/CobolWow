using SQLite;

namespace CobolWow.Tools.Database.Tables
{
   class CharacterSpell
   {
      [PrimaryKey, AutoIncrement]
      public int ID { get; set; }

      public int GUID { get; set; }
      public int SpellID { get; set; }
   }
}
