using System;

using CobolWow.Game.Entitys;
using CobolWow.Network.Packets;
using CobolWow.Tools.Extensions;

namespace CobolWow.Communication.Outgoing.World.Entity
{
   public class PSCreatureQueryResponse : ServerPacket
   {
      public PSCreatureQueryResponse(uint entry, UnitEntity entity) : base(WorldOpcodes.SMSG_CREATURE_QUERY_RESPONSE)
      {
this.Write(entry);
            this.WriteCString(entity.Name);
            this.WriteNullByte(3); // Name2,3,4

            if (entity.Template.SubName == "\\N")
            {
                this.WriteNullByte(1);
            }
            else
            {
                this.WriteCString(entity.Template.SubName);
            }

            this.Write((UInt32)entity.Template.CreatureTypeFlags);
            this.Write((UInt32)entity.Template.CreatureType);
            this.Write((UInt32)entity.Template.Family);
            this.Write((UInt32)entity.Template.Rank);
            this.WriteNullUInt(1);

            this.Write((UInt32)entity.Template.PetSpellDataId);
            this.Write((UInt32)entity.Template.ModelId1);
            this.Write((UInt16)entity.Template.Civilian);
      }
   }
}
