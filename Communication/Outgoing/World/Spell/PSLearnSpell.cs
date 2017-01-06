using System;
using CobolWow.Network;

namespace CobolWow.Communication.Outgoing.World.Spell
{
   public class PSLearnSpell : ServerPacket
   {
      public PSLearnSpell(uint spellID) : base(WorldOpcodes.SMSG_LEARNED_SPELL)
      {
         Write((uint)spellID);
         Write((UInt16)0);
      }
   }
}
