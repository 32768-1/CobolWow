using CobolWow.Communication;
using CobolWow.Network;

namespace CobolWow.Game.Constants
{
   public enum PacketHeaderType : byte
   {
      AuthClientMessage = 1,
      AuthServerMessage = 3,
      WorldServerMessage = 4,
      WorldClientMessage = 6
   }

   public static class PacketHeaderExtension
   {
      public static PacketHeaderType Parse(this RealmServerOpCode opCode)
      {
         return opCode.ToString().StartsWith("CMSG") ? PacketHeaderType.WorldClientMessage : PacketHeaderType.WorldServerMessage;
      }

      public static PacketHeaderType Parse(this LoginOpcodes opCode)
      {
         return PacketHeaderType.AuthServerMessage;
      }
   }
}
