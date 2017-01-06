using CobolWow.Net;
using System.Net.Sockets;
using CobolWow.Game.Sessions;
using CobolWow.Network.Packets;
using System.Collections.Generic;


namespace CobolWow.Network
{
   public class WorldServer : CobolTCPListener
   {
      public static List<WorldSession> Sessions = new List<WorldSession>();
      public int connectionID = 0;

      public override Session GenerateSession(int connectionID, Socket connectionSocket)
      {
         connectionID++;
         WorldSession session = new WorldSession(connectionID, connectionSocket);
         Sessions.Add(session);

         return session;
      }

      public static void TransmitToAll(ServerPacket packet)
      {
         using (packet)
            WorldServer.Sessions.FindAll(s => s.Character != null).ForEach(s => s.SendPacket(packet));
      }

      public static WorldSession GetSessionByPlayerName(string playerName)
      {
         return WorldServer.Sessions.Find(user => user.Character.Name.ToLower() == playerName.ToLower());
      }

   }
}
