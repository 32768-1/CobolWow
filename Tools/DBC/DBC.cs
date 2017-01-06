using CobolWow.Tools.Config;
using CobolWow.Tools.DBC.Tables;
using CobolWow.Tools.DBC.Helper;
using System.Collections.Generic;

using SQLite;
using System.Threading.Tasks;

namespace CobolWow.Tools.DBC
{
   public class DBC
   {
      //public static List<string> QueuedCachedDBC = new List<string>();

      public static SQLiteConnection SQLite { get; private set; }

      // [Helper]
      public static SpellsDBC Spells { get; private set; }
      public static ItemTemplaceDBC ItemTemplates { get; private set; }
      public static ChrStartingOutfitDBC ChracterStartingOutfit { get; private set; }

      // [Non-Helper]
      public static CachedDBC<ChrRacesEntry> CharacterRacesEntry { get; private set; }
      public static CachedDBC<AreaTableEntry> AreaTables { get; private set; }
      public static CachedDBC<AreaTriggerEntry> AreaTriggers { get; private set; }
      public static CachedDBC<CreatureTemplateEntry> CreatureTemplates { get; private set; }
      public static CachedDBC<EmotesEntry> Emotes { get; private set; }
      public static CachedDBC<EmotesTextEntry> EmotesText { get; private set; }

      public async static Task<bool> Boot()
      {
         SQLite = new SQLiteConnection(ConfigManager.DBDBC);

         //TODO Fix Spells DBC and re-enable spellcollections
         //Spells = new SpellsDBC();

         ItemTemplates = new ItemTemplaceDBC();       
         ChracterStartingOutfit = new ChrStartingOutfitDBC();        
         CharacterRacesEntry = new CachedDBC<ChrRacesEntry>();        
         AreaTables = new CachedDBC<AreaTableEntry>();         
         AreaTriggers = new CachedDBC<AreaTriggerEntry>();         
         CreatureTemplates = new CachedDBC<CreatureTemplateEntry>();         
         Emotes = new CachedDBC<EmotesEntry>();         
         EmotesText = new CachedDBC<EmotesTextEntry>();

         //Initialize cache operations.
         await ItemTemplates.InitCache();
         await ChracterStartingOutfit.InitCache();
         await CharacterRacesEntry.InitCache();
         await AreaTables.InitCache();
         await AreaTriggers.InitCache();
         await CreatureTemplates.InitCache();
         await Emotes.InitCache();
         await EmotesText.InitCache();

         // Wait till DBC's are cached
         //while (DBC.QueuedCachedDBC.Count > 0) { }

         return true;

         return false;

      }
   }
}
