using CobolWow.Tools.Config;
using CobolWow.Tools.Database.Helpers;
using CobolWow.Tools.Database.Tables;

using SQLite;
using System.Threading.Tasks;

namespace CobolWow.Tools.Database
{
   public class DB
   {
      public static SQLiteConnection Character;
      public static SQLiteConnection World;

      public async static Task<bool> Boot()
      {
         return await Task.Factory.StartNew<bool>(() =>
         {
            Character = new SQLiteConnection(ConfigManager.DBCHARACTERS);
            //TODO these values should be removed when we have finished with the databases
            Character.CreateTable(typeof(Account));
            Character.CreateTable(typeof(Character));
            Character.CreateTable(typeof(Channel));
            Character.CreateTable(typeof(ChannelCharacter));
            Character.CreateTable(typeof(CharacterSpell));
            Character.CreateTable(typeof(CharacterActionBarButton));
            Character.CreateTable(typeof(CharacterInventory));
            Character.CreateTable(typeof(CharacterMail));

            DBAccounts.CreateAccount("Wesko", "ira31793");

            World = new SQLiteConnection(ConfigManager.DBWORLD);
            World.CreateTable(typeof(CharacterCreationSpell));
            World.CreateTable(typeof(CharacterCreationActionBarButton));
            World.CreateTable(typeof(CharacterCreationInfo));
            World.CreateTable(typeof(AreaTriggerTeleport));

            return true;
         });
      }
   }
}
