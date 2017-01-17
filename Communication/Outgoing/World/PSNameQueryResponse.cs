using System.Text;
using CobolWow.Network.Packets;
using CobolWow.Database20.Tables;

namespace CobolWow.Communication.Outgoing.World
{
   public class PSNameQueryResponse : ServerPacket
   {
      public PSNameQueryResponse(Character character) : base(WorldOpcodes.SMSG_NAME_QUERY_RESPONSE)
      {
         Write((ulong)character.guid);
         Write(Encoding.UTF8.GetBytes(character.name + '\0'));
         Write((byte)0); // realm name for cross realm BG usage
         Write((uint)character.race);
         Write((uint)character.gender);
         Write((uint)character.@class);
      }
   }
}
