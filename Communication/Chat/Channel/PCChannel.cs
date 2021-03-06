﻿
using CobolWow.Network.Packets;

namespace CobolWow.Communication.Incoming.World.Chat.Channel
{
   class PCChannel : PacketReader
   {
      public string ChannelName { get; private set; }
      public string Password { get; private set; }

      public PCChannel(byte[] data) : base(data)
      {
         ChannelName = ReadCString();
         Password = ReadCString();
      }
   }
}
