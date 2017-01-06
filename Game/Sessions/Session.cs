using System;
using System.Net.Sockets;

using CobolWow.Tools;
using CobolWow.Network.Packets;

namespace CobolWow.Game.Sessions
{
   public abstract class Session
   {
      public const int BUFFER_SIZE = 2048 * 2;
      public byte[] BUFFER = new byte[BUFFER_SIZE];
      public byte[] RECIEVE_BUFFER = new byte[0]; //Dynamic
      public byte[] SEND_BUFFER = new byte[0]; //Dynamic

      public const int TIMEOUT = 1000;

      public int connectionID { get; private set; }
      public Socket connectionSocket { get; private set; }

      public string ConnectionRemoteIP { get { return connectionSocket.RemoteEndPoint.ToString(); } }
      public int ConnectionID { get { return connectionID; } }

      public Session(int _connectionID, Socket _connectionSocket)
      {
         connectionID = _connectionID;
         connectionSocket = _connectionSocket;

         connectionSocket.BeginReceive(BUFFER, 0, BUFFER.Length, SocketFlags.None, new AsyncCallback(RecievedData), null);
      }

      public void SendData(byte[] send)
      {
         Array.Resize(ref SEND_BUFFER, send.Length);
         Array.Copy(send, SEND_BUFFER, send.Length);

         try
         {
            connectionSocket.BeginSend(SEND_BUFFER, 0, SEND_BUFFER.Length, SocketFlags.None, delegate (IAsyncResult result) { }, null);
         }
         catch (Exception)
         {
            Disconnect();
         }
      }

      public void SendData(ServerPacket packet)
      {
         using(packet)
            SendData(packet.Packet);
      }

      public virtual void Disconnect(object _obj = null)
      {
         try
         {
            Logger.Log(LogType.Network, "User Disconnected");

            connectionSocket.Close();
            CobolWow.LoginServer.FreeConnectionID(connectionID);
         }
         catch (Exception socketException)
         {
            Logger.Log(LogType.Error, socketException.ToString());
         }
      }

      internal virtual void RecievedData(IAsyncResult _asyncResult)
      {
         int bytesRecived = 0;

         try { bytesRecived = connectionSocket.EndReceive(_asyncResult); }
         catch (Exception) { Disconnect(); }

         if (bytesRecived != 0)
         {
            Array.Resize(ref RECIEVE_BUFFER, bytesRecived);
            Array.Copy(BUFFER, RECIEVE_BUFFER, bytesRecived);

            OnPacket(RECIEVE_BUFFER);

            try
            {
               connectionSocket.BeginReceive(BUFFER, 0, BUFFER.Length, SocketFlags.None, new AsyncCallback(RecievedData), null);
            }
            catch (Exception e)
            {
               Console.WriteLine("Error occured recieving packet!");
            }
         }
         else
         {
            Disconnect();
         }
      }

      public abstract void OnPacket(byte[] data);
   }
}
