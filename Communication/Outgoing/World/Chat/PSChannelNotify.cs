﻿using System;
using System.Text;

using CobolWow.Network;
using CobolWow.Game.Constants.Game.Chat.Channel;

namespace CobolWow.Communication.Outgoing.World.Chat
{
   class PSChannelNotify : ServerPacket
   {
      public PSChannelNotify(ChatChannelNotify type, ulong GUID, String channelName) : base(WorldOpcodes.SMSG_CHANNEL_NOTIFY)
      {
         Write((byte)type);
         Write(Encoding.UTF8.GetBytes(channelName + '\0'));
         Write((ulong)GUID);

         //Write((byte)0x03); // Flags
         //Write((uint)1); // ID
         //Write((uint)0); // ?
      }
   }
}
