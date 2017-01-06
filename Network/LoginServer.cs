using System.Net.Sockets;
using CobolWow.Game.Sessions;

namespace CobolWow.Network
{
   public class LoginServer : CobolTCPListener
   {
      public override Session GenerateSession(int connectionID, Socket connectionSocket)
      {
         return new LoginSession(connectionID, connectionSocket);
      }
   }
}
