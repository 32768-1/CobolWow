using System;
using System.Threading;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Tools;
using CobolWow.Network;
using CobolWow.Tools.Database.Tables;
using CobolWow.Tools.Database.Helpers;
using CobolWow.Communication.Outgoing.World.Update;

namespace CobolWow.Game.Managers
{
   public class EntityManager
   {
      private static Log Logger = new Log();
      public static void Boot()
      {
         Thread thread = new Thread(Update);
         thread.Start();
      }

      public static void Update()
      {
         /*while (true)
         {
             /*
             foreach (ObjectEntity entity in ObjectEntity.Entitys.ToArray())
             {
                 if (entity.UpdateCount > 0)
                 {
                     ServerPacket packet = PSUpdateObject.UpdateValues(entity);

                     WorldServer.Sessions.FindAll(s => entity.ObjectGUID != null).ForEach(s => 
                         {
                             s.sendMessage("Update Packet From: " + (entity as PlayerEntity).Character.Name);
                             s.sendPacket(packet);
                         });
                 }



                 if (entity is PlayerEntity)
                 {
                     PlayerEntity player = entity as PlayerEntity;

                     float distance = (float)Math.Sqrt(Math.Pow(player.X - player.lastUpdateX, 2) + Math.Pow(player.Y - player.lastUpdateY, 2));

                     if (player.Session != null && distance > 50)
                     {
                         SpawnGameObjects(player.Session);                           
                     }
                 }
             }

             */
         Thread.Sleep(100);

      }

      public static void SpawnPlayer(Character character)
      {
         WorldServer.Sessions.FindAll(s => s.Character != character).ForEach(s =>
         {
            s.SendMessage("Spawning: " + character.Name);
            s.SendPacket(PSUpdateObject.CreateCharacterUpdate(character));
         });
      }

      public static void SendPlayers(WorldSession session)
      {
         WorldServer.Sessions.FindAll(s => s.Character != null).FindAll(s => s.Character != session.Character).ForEach(s =>
         {
            session.SendMessage("Spawning: " + s.Character);
            session.SendPacket(PSUpdateObject.CreateCharacterUpdate(s.Character));
         });
      }

      public static void SpawnGameObjects(WorldSession worldSession)
      {
         worldSession.Entity.lastUpdateX = worldSession.Entity.X;
         worldSession.Entity.lastUpdateY = worldSession.Entity.Y;

         worldSession.Entity.X = worldSession.Character.X;
         worldSession.Entity.Y = worldSession.Character.Y;
         worldSession.Entity.Z = worldSession.Character.Z;

         DateTime before = DateTime.Now;
         List<GameObject> gameObjects = DBGameObject.GetGameObjects(worldSession.Entity, 100);
         var ms = DateTime.Now.Subtract(before).Milliseconds;

         worldSession.SendMessage("Sending " + gameObjects.Count + " in " + ms);

         foreach (GameObject gameObject in gameObjects)
         {
            GameObjectTemplate template = DBGameObject.GetGameObjectTemplate((uint)gameObject.ID);
            if (template != null)
               worldSession.SendPacket(PSUpdateObject.CreateGameObject(gameObject.X, gameObject.Y, gameObject.Z, gameObject, template));
         }
      }
   }
}
