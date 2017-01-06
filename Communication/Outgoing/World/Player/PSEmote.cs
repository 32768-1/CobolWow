using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.World.Player
{
   class PSEmote : ServerPacket
   {
      public PSEmote(uint emoteID, ulong GUID) : base(WorldOpcodes.SMSG_EMOTE)
      {
         Write(emoteID);
         Write(GUID);
      }
   }
}
