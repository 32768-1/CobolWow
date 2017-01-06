using CobolWow.Net;
using CobolWow.Communication.Outgoing.World.Player;

namespace CobolWow.Tools.Chat.Commands
{
   [ChatCommandNode("emote", "Emote Commands")]
   public class Emote
   {
      [ChatCommand("chicken", "Sends chicken emote")]
      public static void Send(WorldSession session, string[] args)
      {
         session.SendPacket(new PSEmote(19, session.Entity.ObjectGUID.RawGUID));
      }
   }
}
