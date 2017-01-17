using System;
using System.Text;
using CobolWow.Network.Packets;
using CobolWow.Tools.Database.Tables;
using CobolWow.Database20.Tables;

namespace CobolWow.Communication.Outgoing.World.Entity
{
   public class PSGameObjectQueryResponse : ServerPacket
   {
      public PSGameObjectQueryResponse(GameObject_Template gameObjectTemplate) : base(WorldOpcodes.SMSG_GAMEOBJECT_QUERY_RESPONSE)
      {
         Write((UInt32)gameObjectTemplate.entry);
         Write((UInt32)gameObjectTemplate.type);
         Write((UInt32)gameObjectTemplate.displayId);
         Write(Encoding.UTF8.GetBytes(gameObjectTemplate.name));
         Write((UInt16)0);
         Write((byte)0);
         Write((byte)0);

         // 24 byte clear
         Write((short)0);
         Write((byte)0); //
      }
   }
}
