using System;
using System.Threading;
using System.Collections.Generic;

using CobolWow.Net;
using CobolWow.Tools;
using CobolWow.Network;
using CobolWow.Communication;
using CobolWow.Game.Handlers;
using CobolWow.Communication.Outgoing.Auth;
using CobolWow.Communication.Outgoing.World.Logout;

namespace CobolWow.Game.Managers
{
   public class LogoutManager
   {
      public const int LOGOUT_TIME = 1; //Todo
      private static Log Logger = new Log();
      private static Dictionary<WorldSession, DateTime> LogoutQueue = new Dictionary<WorldSession, DateTime>();

      public static void Boot()
      {
         Thread thread = new Thread(Run);
         thread.Start();

         Logger.Print(LogType.Information, "LogoutManager Initialized.");

         WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_LOGOUT_REQUEST, OnLogout);
         WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_LOGOUT_CANCEL, OnCancel);
      }

      public static void OnLogout(WorldSession session, PacketReader reader)
      {
         if (LogoutQueue.ContainsKey(session))
            return; //Already queued for logout.
        
         LogoutQueue.Add(session, DateTime.Now);
         session.SendPacket(new SCLogoutResponse());
      }

      public static void OnCancel(WorldSession session, PacketReader reader)
      {
         LogoutQueue.Remove(session);
         session.SendPacket(new PSLogoutCancelAcknowledgement());
      }

      public static void Run()
      {
         while (true)
         {
            foreach (var queuedLogout in LogoutQueue)
            {
               if (DateTime.Now.Subtract(queuedLogout.Value).Seconds >= LOGOUT_TIME)
               {                 
                  LogoutQueue.Remove(queuedLogout.Key);
                  queuedLogout.Key.SendPacket(new PSLogoutComplete());
                  World.DispatchOnPlayerDespawn(queuedLogout.Key.Entity);
               }
            }

            Thread.Sleep(1000);
         }
      }
   }
}
