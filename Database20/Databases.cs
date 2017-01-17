using System.Data;
using System.Configuration;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using CobolWow.Tools;
using CobolWow.Database20.Items;
using CobolWow.Database20.Characters;

namespace CobolWow.Database20
{
   public static class DataBase20
   {
      public static IDbConnection WorldDatabase;
      public static IDbConnection CharacterDatabase;
      public static IDbConnection RealmDatabase;
      public async static Task<bool> BootWorldDatabase()
      {
         return await Task.Factory.StartNew<bool>(() =>
         {
            try
            {
               Logger.Log(LogType.Database, "Opening Database connection...");
               WorldDatabase = new MySqlConnection(ConfigurationManager.ConnectionStrings["WorldDatabase"].ToString());
               WorldDatabase.Open();

               //Load tables.
               ItemManager.Initialize();
               return true;
            }
            catch
            {
               return false;
            }
         });
      }

      public async static Task<bool> BootCharacterDatabase()
      {
         return await Task.Factory.StartNew<bool>(() =>
         {
            try
            {
               Logger.Log(LogType.Database, "Opening Character Database connection...");
               CharacterDatabase = new MySqlConnection(ConfigurationManager.ConnectionStrings["CharactersDatabase"].ToString());
               CharacterDatabase.Open();

               //Load tables.
               ActionButtonsManager.Initialize();
               CharacterManager.Initialize();
               return true;
            }
            catch
            {
               return false;
            }
         });
      }

      public async static Task<bool> BootRealmDatabase()
      {
         return await Task.Factory.StartNew<bool>(() =>
         {
            try
            {
               Logger.Log(LogType.Database, "Opening Realm Database connection...");
               RealmDatabase = new MySqlConnection(ConfigurationManager.ConnectionStrings["RealmDatabase"].ToString());
               RealmDatabase.Open();

               //Load tables.
               return true;
            }
            catch
            {
               return false;
            }
         });
      }
   }
}
