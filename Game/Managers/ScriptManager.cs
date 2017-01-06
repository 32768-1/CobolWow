using System;
using System.IO;
using System.Linq;
using CobolWow.Tools;
using CobolWow.Tools.Config;
using System.Collections.Generic;

namespace CobolWow.Game.Managers
{
   public class ScriptManager
   {
      private static List<ScriptCompiler> scripts = new List<ScriptCompiler>();
      private static FileSystemWatcher watcher;

      public static void Boot()
      {
         LoadScripts();

         watcher = new FileSystemWatcher(ConfigManager.SCRIPT_LOCATION);

         watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
         watcher.Filter = "*.cs";

         watcher.Changed += ReloadScript;
         watcher.Created += LoadScript;
         watcher.Renamed += LoadScript;
         watcher.Deleted += UnloadScript;

         watcher.EnableRaisingEvents = true;
      }

      private static void LoadScripts()
      {
         if (!Directory.Exists(ScriptLocation))
         {
            Logger.Log(LogType.Script, "Script location (" + ScriptLocation + ") dosn't exist. Creating...");

            Directory.CreateDirectory(ScriptLocation);
         }

         String[] scripts = Directory.GetFiles(ScriptLocation);

         foreach (String script in scripts)
         {
            LoadScript(script);
         }
      }

      private static void ReloadScript(object source, FileSystemEventArgs e)
      {
         try
         {
            watcher.EnableRaisingEvents = false;

            string scriptName = Path.GetFileNameWithoutExtension(e.FullPath);
            UnloadScript(scriptName);
            LoadScript(e.FullPath);
         }

         finally
         {
            watcher.EnableRaisingEvents = true;
         }
      }

      private static void LoadScript(object sender, FileSystemEventArgs e)
      {
         LoadScript(e.FullPath);
      }

      private static void LoadScript(string scriptPath)
      {
         ScriptCompiler script = new ScriptCompiler();
         if (script.Compile(scriptPath)) scripts.Add(script);
      }

      private static void UnloadScript(object sender, FileSystemEventArgs e)
      {
         UnloadScript(Path.GetFileNameWithoutExtension(e.FullPath));
      }

      private static void UnloadScript(string scriptName)
      {
         ScriptCompiler script = scripts.FirstOrDefault(s => s.Name == scriptName);

         if (script != null)
         {
            scripts.Remove(script);

            script.Plugin.Unload();

            Logger.Log(LogType.Script, "Script Unloaded: " + scriptName);
         }
      }

      public static string ScriptLocation
      {
         get { return ConfigManager.SCRIPT_LOCATION; }
      }
   }
}
