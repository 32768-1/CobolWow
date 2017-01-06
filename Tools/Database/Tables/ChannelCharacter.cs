using SQLite;

namespace CobolWow.Tools.Database.Tables
{
   public class ChannelCharacter
   {
      [PrimaryKey, AutoIncrement]
      public int ID { get; set; }

      public int GUID { get; set; }
      public int ChannelID { get; set; }
   }
}
