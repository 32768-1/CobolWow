using System;
using System.Collections.Generic;

namespace CobolWow.Tools
{
   public enum LogType
   {
      None,
      Debug,
      Client,
      Network,
      Packet,
      Error,
      Warning,
      Database,
      Information,
      General,
      Script
   }

   public static class Logger
   {
      private static readonly Dictionary<LogType, ConsoleColor> TypeColour = new Dictionary<LogType, ConsoleColor>()
        {
            { LogType.Debug,    ConsoleColor.DarkMagenta },
            { LogType.Network,   ConsoleColor.Green },
            { LogType.Client,   ConsoleColor.Cyan },
            { LogType.Error,    ConsoleColor.Red },
            { LogType.Information,   ConsoleColor.Cyan },
            { LogType.Database, ConsoleColor.Magenta },
            { LogType.General,      ConsoleColor.White },
            { LogType.Packet,   ConsoleColor.Cyan },
            { LogType.Warning,  ConsoleColor.Yellow },
            { LogType.Script,  ConsoleColor.Cyan }
        };

      public static void Log(LogType _type, object _obj)
      {
         Console.ForegroundColor = TypeColour[_type];
         Console.Write("[" + _type.ToString() + "] ");
         Console.ForegroundColor = ConsoleColor.White;
         Console.WriteLine(_obj.ToString());
      }

      public static void Log(object obj)
      {
         Console.ResetColor();
         Console.WriteLine("[CobolWoW] " + obj.ToString());
      }

   }
}
