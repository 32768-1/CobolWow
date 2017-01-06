using System.Linq;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Game.Entitys;
using CobolWow.Game.Managers;

namespace CobolWow.Game
{
   public delegate void PlayerEvent(PlayerEntity player);

   // Temp name...
   public class World
   {
      public static event PlayerEvent OnPlayerSpawn;
      public static event PlayerEvent OnPlayerDespawn;

      public static void DispatchOnPlayerSpawn(PlayerEntity player)
      {
         OnPlayerSpawn?.Invoke(player);
      }

      public static void DispatchOnPlayerDespawn(PlayerEntity player)
      {
         OnPlayerDespawn?.Invoke(player);
      }

      // [Helpers]
      public static List<PlayerEntity> PlayersWhoKnow(PlayerEntity player)
      {
         return PlayerManager.Players.Where(p => p.KnownPlayers.Contains(player)).ToList();
      }

      public static List<WorldSession> SessionsWhoKnow(PlayerEntity player, bool includeSelf = false)
      {
         List<WorldSession> sessions = PlayersWhoKnow(player).ConvertAll<WorldSession>(p => p.Session);

         if (includeSelf == true) sessions.Add(player.Session);

         return sessions;
      }


      public static List<PlayerEntity> PlayersWhoKnowUnit(UnitEntity unit)
      {
         return PlayerManager.Players.Where(p => p.KnownUnits.Contains(unit)).ToList();
      }
   }
}
