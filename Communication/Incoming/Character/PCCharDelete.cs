using CobolWow.Network.Packets;

namespace CobolWow.Communication.Incoming.Character
{
   public class PCCharDelete : PacketReader
   {
      public Database20.Tables.Character Character { get; private set; }

      public PCCharDelete(byte[] data) : base(data)
      {
         int GUID = (int)ReadUInt64();

         Character = Database20.Characters.CharacterManager.GetCharacter(GUID);
      }
   }
}
