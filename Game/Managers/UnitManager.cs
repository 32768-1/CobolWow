using System.Linq;

using CobolWow.Net;
using CobolWow.Tools;
using CobolWow.Game.Entitys;
using CobolWow.Game.Handlers;
using CobolWow.Communication;
using CobolWow.Network.Packets;
using CobolWow.Communication.Outgoing.World.Entity;

namespace CobolWow.Game.Managers
{
   public class UnitManager
   {
      public static void Boot()
      {
         WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_ATTACKSWING, OnAttackSwing);
         WorldDataRouter.AddHandler<PacketReader>(WorldOpcodes.CMSG_CREATURE_QUERY, OnCreatureQuery);

         Logger.Log(LogType.Information, "UnitManager Initialized.");
      }

      private static void OnCreatureQuery(WorldSession session, PacketReader handler)
      {
         uint entry = handler.ReadUInt32();
         ulong GUID = handler.ReadUInt64();

         UnitEntity entity = CobolWow.UnitComponent.Entitys.FindAll(u => u.ObjectGUID.RawGUID == GUID).First();

         session.SendPacket(new PSCreatureQueryResponse(entry, entity));
      }

      private static void OnAttackSwing(WorldSession session, PacketReader handler)
      {
         ulong GUID = handler.ReadUInt64();

         UnitEntity target = CobolWow.UnitComponent.Entitys.FindAll(u => u.ObjectGUID.RawGUID == GUID).First();

         if (target != null)
         {
            session.SendMessage("Attacking:" + target.Name);

            ServerPacket packet = new ServerPacket(WorldOpcodes.SMSG_ATTACKSTART);
            packet.Write(session.Entity.ObjectGUID.RawGUID);
            packet.Write(target.ObjectGUID.RawGUID);
            session.SendPacket(packet);
         }
         else
         {
            session.SendMessage("Cant find target");
         }
      }
   }
}
