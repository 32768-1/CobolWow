using System;
using CobolWow.Net;
using CobolWow.Tools;
using System.Threading;
using CobolWow.Game.Entitys;
using System.Collections.Generic;
using CobolWow.Communication.Outgoing.World.Update;

namespace CobolWow.Game.Managers
{
   public class PlayerManager
   {
      public static HashSet<PlayerEntity> Players = new HashSet<PlayerEntity>();

      public static void Boot()
      {
         World.OnPlayerSpawn += new PlayerEvent(OnPlayerSpawn);
         World.OnPlayerDespawn += new PlayerEvent(OnPlayerDespawn);

         Thread _thread = new Thread(Update);
         _thread.Start();

         Logger.Log(LogType.Information, "PlayerManager Initialized.");
      }

      private static void OnPlayerDespawn(PlayerEntity player)
      {
         foreach (PlayerEntity remotePlayer in Players)
         {         
            if (player == remotePlayer) //Skip self
               continue;

            if (remotePlayer.KnownPlayers.Contains(player))
               DespawnPlayer(remotePlayer, player);
         }

         Players.Remove(player);
      }

      private static void OnPlayerSpawn(PlayerEntity player)
      {        
         Players.Add(player); //Enqueu
      }

      private static void Update()
      {
         while (true)
         {
            // Spawning && Despawning
            foreach (PlayerEntity player in Players)
            {
               foreach (PlayerEntity otherPlayer in Players)
               {
                  if (player == otherPlayer)//Skip self
                     continue;

                  if (InRangeCheck(player, otherPlayer))
                     if (!player.KnownPlayers.Contains(otherPlayer))
                        SpawnPlayer(player, otherPlayer);
                  else
                     if (player.KnownPlayers.Contains(otherPlayer))
                        DespawnPlayer(player, otherPlayer);
               }
            }

            // Update (Maybe have one for all entitys (GO, Unit & Players)
            foreach (PlayerEntity player in Players)
            {
               if (player.UpdateCount > 0)
               {
                  // Generate update packet
                  PSUpdateObject updateMsg = PSUpdateObject.UpdateValues(player);
                  player.Session.SendPacket(updateMsg);

                  foreach (WorldSession session in World.SessionsWhoKnow(player))
                     session.SendPacket(updateMsg);
               }
            }

            Thread.Sleep(100);
         }
      }

      private static void SpawnPlayer(PlayerEntity remote, PlayerEntity player)
      {
         // Should be sending player entity
         remote.Session.SendPacket(PSUpdateObject.CreateCharacterUpdate(player.Character));

         // Add it to known players
         remote.KnownPlayers.Add(player);

         remote.Session.SendMessage("SpawningPlayer: " + player.Character.name);
      }

      private static void DespawnPlayer(PlayerEntity remote, PlayerEntity player)
      {
         // Should be sending player entity
         remote.Session.SendPacket(PSUpdateObject.CreateOutOfRangeUpdate(new List<ObjectEntity>() { player }));

         // Add it to known players
         remote.KnownPlayers.Remove(player);

         remote.Session.SendMessage("DespawningPlayer: " + player.Character.name);
      }

      private static bool InRangeCheck(PlayerEntity playerA, PlayerEntity playerB)
      {
         // Check map
         // Check distance

         double distance = GetDistance(playerA.X, playerA.Y, playerB.X, playerB.Y);
         return distance < 50;
      }

      private static double GetDistance(float aX, float aY, float bX, float bY)
      {
         double a = (double)(aX - bX);
         double b = (double)(bY - aY);

         return Math.Sqrt(a * a + b * b);
      }
   }
}
