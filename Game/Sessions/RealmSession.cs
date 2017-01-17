using System;
using System.IO;
using System.Net.Sockets;

using CobolWow.Tools;
using CobolWow.Game.Handlers;
using CobolWow.Communication;
using CobolWow.Network.Packets;
using CobolWow.Tools.Cryptography;

namespace CobolWow.Game.Sessions
{
   public class LoginSession : Session
   {
      public String accountName { get; set; }
      public byte[] SessionKey;
      public Authenticator Authenticator;
      public LoginSession(int _connectionID, Socket _connectionSocket) : base(_connectionID, _connectionSocket) { }

      public override void Disconnect(object _obj = null)
      {
         base.Disconnect();
         CobolWow.LoginServer.FreeConnectionID(connectionID);
      }

      public void SendPacket(LoginOpcodes opcode, byte[] data)
      {
         SendPacket((byte)opcode, data);
      }

      public void SendPacket(ServerPacket packet)
      {
         using (packet)
            SendPacket((byte)packet.Opcode, packet.Packet);
      }

      public void SendPacket(byte opcode, byte[] data)
      {
         using (MemoryStream ms = new MemoryStream())
         using (BinaryWriter writer = new BinaryWriter(ms))
         {
            LoginOpcodes _opcode = (LoginOpcodes)opcode;
            if (_opcode == LoginOpcodes.AUTH_LOGIN_CHALLENGE || _opcode == LoginOpcodes.AUTH_LOGIN_PROOF) //err.... was working without this in the past... -¿-
            {
               Logger.Log(LogType.Warning, "Sending Logon Opcode: " + _opcode);
               writer.Write(opcode);
               writer.Write(data);
            }
            else
            {
               writer.Write(opcode);
               writer.Write((ushort)data.Length);
               writer.Write(data);
            }

            Logger.Log(LogType.Network, "Server -> Client [" + (LoginOpcodes)opcode + "] [0x" + opcode.ToString("X") + "]");

            SendData(ms.ToArray());
         }
      }

      public override void OnPacket(byte[] data)
      {
         short opcode = BitConverter.ToInt16(data, 0);
         Logger.Log(LogType.Network, " Data Recived - OpCode:" + opcode.ToString("X2") + " " + ((LoginOpcodes)opcode));

         LoginOpcodes code = (LoginOpcodes)opcode;

         LoginDataRouter.CallHandler(this, code, data);
      }
   }
}
