using System;
using System.IO;
using System.Net.Sockets;

using CobolWow.Tools;
using CobolWow.Network;
using CobolWow.Game.Handlers;
using CobolWow.Communication;

using CobolWow.Tools.Cryptography;

namespace CobolWow.Game.Sessions
{
   public class LoginSession : Session
   {
      public SRP6 Srp6;
      public String accountName { get; set; }
      public byte[] SessionKey;
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
            writer.Write(opcode);
            writer.Write((ushort)data.Length);
            writer.Write(data);

            Logger.Log(LogType.Database, connectionID + "Server -> Client [" + (LoginOpcodes)opcode + "] [0x" + opcode.ToString("X") + "]");

            SendData(ms.ToArray());
         }
      }

      public override void OnPacket(byte[] data)
      {
         short opcode = BitConverter.ToInt16(data, 0);
         Logger.Log(LogType.Network, ConnectionRemoteIP + " Data Recived - OpCode:" + opcode.ToString("X2") + " " + ((LoginOpcodes)opcode));

         LoginOpcodes code = (LoginOpcodes)opcode;

         LoginDataRouter.CallHandler(this, code, data);
      }
   }
}
