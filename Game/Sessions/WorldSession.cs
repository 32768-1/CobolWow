using System;
using System.IO;
using System.Net.Sockets;

using CobolWow.Game;
using CobolWow.Tools;
using CobolWow.Network;
using CobolWow.Game.Entitys;
using CobolWow.Game.Managers;
using CobolWow.Game.Sessions;
using CobolWow.Game.Handlers;
using CobolWow.Communication;
using CobolWow.Tools.Cryptography;
using CobolWow.Tools.Database.Tables;
using CobolWow.Tools.Database.Helpers;
using CobolWow.Communication.Outgoing.Players;

namespace CobolWow.Net
{
   public class WorldSession : Session
   {
      public UInt32 seed;
      public VanillaCrypt crypt;
      public Account Account;
      public Character Character;
      public PlayerEntity Entity;
      public uint OutOfSyncDelay;
      public uint Latancy;

      public WorldSession(int _connectionID, Socket _connectionSocket) : base(_connectionID, _connectionSocket)
      {
         SendPacket(WorldOpcodes.SMSG_AUTH_CHALLENGE, new byte[] { 0x33, 0x18, 0x34, 0xC8 });
      }

      public override void Disconnect(object _obj = null)
      {
         base.Disconnect();
         CobolWow.WorldServer.FreeConnectionID(connectionID);
         World.DispatchOnPlayerDespawn(Entity);
         WorldServer.Sessions.Remove(this);
      }

      private byte[] Encode(int size, int opcode)
      {
         int index = 0;
         int newSize = size + 2;
         byte[] header = new byte[4];
         if (newSize > 0x7FFF)
            header[index++] = (byte)(0x80 | (0xFF & (newSize >> 16)));

         header[index++] = (byte)(0xFF & (newSize >> 8));
         header[index++] = (byte)(0xFF & newSize);
         header[index++] = (byte)(0xFF & opcode);
         header[index] = (byte)(0xFF & (opcode >> 8));

         if (crypt != null) header = crypt.encrypt(header);

         return header;
      }

      public void SendPacket(int opcode, byte[] data)
      {
         using (MemoryStream ms = new MemoryStream())
         using (BinaryWriter writer = new BinaryWriter(ms))
         {
            byte[] header = Encode(data.Length, (int)opcode);

            writer.Write(header);
            writer.Write(data);

            Logger.Log(LogType.Network, "Server -> Client [" + (WorldOpcodes)opcode + "] [0x" + opcode.ToString("X") + "]");

            //TODO CRASH
            //if (opcode == (int)WorldOpcodes.SMSG_UPDATE_OBJECT)
            //   return;

            SendData(ms.ToArray());
         }
      }

      public void SendPacket(WorldOpcodes opcode, byte[] data)
      {
         SendPacket((int)opcode, data);
      }

      public void SendPacket(ServerPacket packet)
      {
         using (packet)
            SendPacket((int)packet.Opcode, packet.Packet);
      }

      public void SendMessage(String message)
      {
         ChatManager.SendSytemMessage(this, message);
      }

      public void Teleport(int mapID, float X, float Y, float Z)
      {
         Character.MapID = mapID;
         Character.X = X;
         Character.Y = Y;
         Character.Z = Z;
         Character.Rotation = 0;
         DBCharacters.UpdateCharacter(Character);

         using (PSTransferPending transPending = new PSTransferPending(mapID))
         using (PSNewWorld newWorld = new PSNewWorld(mapID, X, Y, Z, 0))
         {
            SendPacket(transPending);
            SendPacket(newWorld);
         }
      }

      private void Decode(byte[] header, out ushort length, out short opcode)
      {
         if (crypt != null)
         {
            crypt.decrypt(header, 6);
         }

         using (PacketReader reader = new PacketReader(header))
         { 
            if (crypt == null)
            {
               length = BitConverter.ToUInt16(new byte[] { header[1], header[0] }, 0);
               opcode = BitConverter.ToInt16(header, 2);
            }
            else
            {
               length = BitConverter.ToUInt16(new byte[] { header[1], header[0] }, 0);
               opcode = BitConverter.ToInt16(new byte[] { header[2], header[3] }, 0);
            }
         }
      }

      public override void OnPacket(byte[] data)
      {
         for (int index = 0; index < data.Length; index++)
         {
            byte[] headerData = new byte[6];
            Array.Copy(data, index, headerData, 0, 6);

            ushort length = 0;
            short opcode = 0;

            Decode(headerData, out length, out opcode);

            WorldOpcodes worldOpcode = (WorldOpcodes)opcode;

            byte[] packetDate = new byte[length];
            Array.Copy(data, index + 6, packetDate, 0, length - 4);
            Logger.Log(LogType.Client, "Client -> Server [" + worldOpcode + "] Packet Length: " + length);

            WorldDataRouter.CallHandler(this, worldOpcode, packetDate);

            index += 2 + (length - 1);
         }
      }

      public void SendHexPacket(WorldOpcodes opcde, string hex)
      {
         string end = hex.Replace(" ", "").Replace("\n", "");
         byte[] data = Helper.HexToByteArray(end);

         SendPacket(opcde, data);
      }
   }
}