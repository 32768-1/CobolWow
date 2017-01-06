using System;
using System.Net.Sockets;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Tools;
using CobolWow.Network;
using CobolWow.Tools.DBC;
using CobolWow.Tools.Chat;
using CobolWow.Tools.Config;
using CobolWow.Game.Managers;
using CobolWow.Game.Sessions;
using CobolWow.Tools.Database;

namespace CobolWow
{
   public class CobolWow
   {
      private static Log Logger = new Log();
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
         Logger.Print(LogType.General, "CobolWoW is warming up...");
         Logger.Print(LogType.General, "Loading Database/DBC...");
         ConfigManager.Boot();
         await DB.Boot();
         await DBC.Boot();
         Logger.Print(LogType.General, "Initializing Managers...");

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

         Logger.Print(LogType.General, "Launching Servers...");
         LoginServer = new LoginServer();
         LoginServer.Start(ConfigManager.LOGINPORT, ConfigManager.LOGINMAX_CONNECTIONS);
         WorldServer = new WorldServer();
         WorldServer.Start(ConfigManager.WORLDPORT, ConfigManager.WORLDMAX_CONNECTIONS);         
      }
   }
}
