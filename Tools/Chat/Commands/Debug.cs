﻿using System;
using CobolWow.Net;
using CobolWow.Game.Entitys;
using CobolWow.Communication;
using CobolWow.Network.Packets;
using CobolWow.Tools.Extensions;

namespace CobolWow.Tools.Chat.Commands
{
   [ChatCommandNode("debug", "Debug Commands")]
   public class Debug
   {
      [ChatCommand("come", "Makes NPC come to you!!")]
      public static void Send(WorldSession session, string[] args)
      {
         if (session.Entity.Target != null)
         {
            //session.sendMessage("Target: " + session.Entity.Name);

            UnitEntity target = session.Entity.Target;

            ServerPacket packet = new ServerPacket(WorldOpcodes.SMSG_MONSTER_MOVE);
            packet.WritePackedUInt64(target.ObjectGUID.RawGUID);
            packet.Write(target.X);
            packet.Write(target.Y);
            packet.Write(target.Z);
            packet.Write((UInt32)Environment.TickCount);
            packet.Write((byte)0); // SPLINETYPE_NORMAL
            packet.Write(0); // sPLINE FLAG
            packet.Write(100); // TIME
            packet.Write(1);
            packet.Write(session.Character.position_x);
            packet.Write(session.Character.position_y);
            packet.Write(session.Character.position_z);

            session.SendPacket(packet);
         }
      }
   }
}
