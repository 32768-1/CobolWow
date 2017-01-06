﻿using CobolWow.Communication;
using CobolWow.Game.Constants;

namespace CobolWow.Network
{
   public class WorldPacket : Packet
    {
        public WorldPacket(byte[] data)
            : base(data)
        { }

        public WorldPacket(LoginOpcodes opCode)
            : base(opCode.Parse(), (byte)opCode)
        { }
    }
}
