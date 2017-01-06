using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

using CobolWow.Tools;
using CobolWow.Game.Sessions;

namespace CobolWow.Network
{
   public class CobolTCPListener
   {
      private Socket socketHandler;
      private int socketPort;
      private int maxConnections;
      private int acceptedConnections;

      private Dictionary<int, Session> ActiveConnections = new Dictionary<int, Session>();

      public int Port { get { return socketPort; } }
      public int AcceptedConnections { get { return acceptedConnections; } }
      public int ConnectionCount { get { return ActiveConnections.Count; } }
      public int MaxConnections { get { return maxConnections; } }

      public bool Start(int _portNumber, int _maxConnections)
      {
         maxConnections = _maxConnections;
         socketPort = _portNumber;
         socketHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

         Logger.Log(LogType.Network, "Starting up game server...");

         try
         {
            socketHandler.Bind(new IPEndPoint(IPAddress.Any, socketPort));
            socketHandler.Listen(25);
            socketHandler.BeginAccept(ConnectionRequest, socketHandler);

            Logger.Log(LogType.Network, "Server Port: " + socketPort);
            Logger.Log(LogType.Network, "Max connections: " + maxConnections);

            return true;
         }
         catch (Exception e)
         {
            Logger.Log(LogType.Error, e.ToString());

            return false;
         }
      }

      private void ConnectionRequest(IAsyncResult _asyncResult)
      {
         Socket connectionSocket = ((Socket)_asyncResult.AsyncState).EndAccept(_asyncResult);

         if (ActiveConnections.Count == maxConnections)
         {
            Logger.Log(LogType.Network, "User Disconnected - Server Full");

            connectionSocket.Close();
            socketHandler.BeginAccept(ConnectionRequest, socketHandler);
            return;
         }

         int connectionID = GetFreeID();

         ActiveConnections.Add(connectionID, GenerateSession(connectionID, connectionSocket));
         acceptedConnections++;

         socketHandler.BeginAccept(ConnectionRequest, socketHandler);
      }

      public virtual Session GenerateSession(int connectionID, Socket connectionSocket)
      {
         throw new Exception("Using base.");
      }

      private int GetFreeID()
      {
         for (int i = 0; i < maxConnections; i++)
            if (!ActiveConnections.ContainsKey(i))
               return i;

         return -1;
      }

      public void FreeConnectionID(int _connectionID)
      {
         if (ActiveConnections.ContainsKey(_connectionID)) ActiveConnections.Remove(_connectionID);
      }
   }
}
