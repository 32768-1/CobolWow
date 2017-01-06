using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CobolWow.Tools.DBC
{
   public class CachedDBC<T> where T : new()
   {
      private List<T> _cachedList;

      public async Task<bool> InitCache()
      {
         return await LoadCache();
      }

      public T Find(Predicate<T> match)
      {
         return List.Find(match);
      }

      public List<T> List
      {
         get
         {
            return _cachedList;
         }
      }

      private async Task<bool> LoadCache()
      {
         await Task.Run<bool>(() =>
         {
            Stopwatch StopWatch = new Stopwatch();
            _cachedList = DBC.SQLite.Table<T>().ToList();
            Logger.Log(LogType.Database, "[Cached] Table: " + typeof(T).Name + " Loaded: " + List.Count + " items. Took: " + StopWatch.ElapsedMilliseconds + "ms ");
            return true;
         });
         return false;
      }
   }
}
