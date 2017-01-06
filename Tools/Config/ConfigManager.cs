using System;
using System.IO;
using Nini.Config;

namespace CobolWow.Tools.Config
{
   static class ConfigManager
   {
      private static IConfigSource ConfigSource = new IniConfigSource("CobolWoWConf.ini");
      private const String FILEPATH = "./CobolWoWConf.ini";
      private static Log Logger = new Log();

      public static bool Boot()
      {
         if (!File.Exists(FILEPATH))
         {
            Logger.Print(LogType.Warning, "INI file not found!");
            return false;
         }

         return true;
      }

      //LOGON SERVER
      public static string LOGINIP { get { return ConfigSource.Configs["LOGIN"].Get("IP"); } }
      public static int LOGINPORT { get { return ConfigSource.Configs["LOGIN"].GetInt("PORT"); } }
      public static int LOGINMAX_CONNECTIONS { get { return ConfigSource.Configs["LOGIN"].GetInt("MAX_CONNECTIONS"); } }

      //WORLD
      public static string WORLDIP { get { return ConfigSource.Configs["WORLD"].Get("IP"); } }
      public static int WORLDPORT { get { return ConfigSource.Configs["WORLD"].GetInt("PORT"); } }
      public static int WORLDMAX_CONNECTIONS { get { return ConfigSource.Configs["WORLD"].GetInt("MAX_CONNECTIONS"); } }
      public static string WORLDNAME { get { return ConfigSource.Configs["WORLD"].Get("NAME"); } }
      public static float WORLDPOPULATION { get { return ConfigSource.Configs["WORLD"].GetFloat("POPULATION"); } }

      //DB
      public static string DBCHARACTERS { get { return ConfigSource.Configs["DB"].Get("CHARACTER"); } }
      public static string DBWORLD { get { return ConfigSource.Configs["DB"].Get("WORLD"); } }
      public static string DBDBC { get { return ConfigSource.Configs["DB"].Get("DBC"); } }

      //DEV
      public static string DBC_LOCATION { get { return ConfigSource.Configs["DEV"].Get("DBC_LOCATION"); } }
      public static string SCRIPT_LOCATION { get { return ConfigSource.Configs["DEV"].Get("SCRIPT_LOCATION"); } }
      public static string COMMAND_KEY { get { return ConfigSource.Configs["DEV"].Get("COMMAND_KEY"); } }
      public static string DBC { get { return ConfigSource.Configs["DEV"].Get("DBC"); } }
   }
}
