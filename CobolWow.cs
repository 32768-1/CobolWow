using System;
using CobolWow.Tools;
using CobolWow.Network;
using CobolWow.Tools.DBC;
using CobolWow.Tools.Chat;
using CobolWow.Tools.Config;
using CobolWow.Game.Managers;
using CobolWow.Tools.Database;

namespace CobolWow
{
   public class CobolWow
   {
      public static UnitComponent UnitComponent { get; private set; }
      public static GameObjectComponent GameObjectComponent { get; private set; }
      public static LoginServer LoginServer { get; private set; }
      public static WorldServer WorldServer { get; private set; }

      static void Main(string[] args)
      {
         Start();
         while (true) Console.ReadLine();
      }

      private static async void Start()
      {
         Logger.Log("CobolWoW is warming up...");
         Logger.Log("Loading Database/DBC...");

         if(!ConfigManager.Boot())
            Logger.Log(LogType.Warning, "Configuration file not found, using default values...");

         await DB.Boot();
         await DBC.Boot();
         Logger.Log("Initializing Managers...");

         AuthManager.Boot();
         LogoutManager.Boot();
         ChatManager.Boot();
         ChatChannelManager.Boot();
         ChatCommandParser.Boot();
         MovementManager.Boot();
         MiscManager.Boot();
         SpellManager.Boot();         
         EntityManager.Boot();       
         CharacterManager.Boot();        
         PlayerManager.Boot();
         UnitManager.Boot();
         MailManager.Boot();

         //ZoneHandler.Boot();
         //ScriptManager.Boot();
         //AIBrainManager.Boot();

         new PlayerManager();
         UnitComponent = new UnitComponent();
         GameObjectComponent = new GameObjectComponent();
         new WorldManager();

         Logger.Log("Launching Servers...");
         LoginServer = new LoginServer();
         LoginServer.Start(ConfigManager.LOGINPORT, ConfigManager.LOGINMAX_CONNECTIONS);
         WorldServer = new WorldServer();
         WorldServer.Start(ConfigManager.WORLDPORT, ConfigManager.WORLDMAX_CONNECTIONS);         
      }
   }
}
