
using CobolWow.Game.Constants;
using CobolWow.Network.Packets;
using CobolWow.Tools.Cryptography;
using System;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSAuthLoginProof : ServerPacket
   {
      public PSAuthLoginProof(Authenticator authenticator) : base(LoginOpcodes.AUTH_LOGIN_PROOF)
      {
         Write((byte)base.Opcode);
         Write((byte)0);
         Write(authenticator.SRP6.M2);
         Write((UInt32)0);
      }

      public PSAuthLoginProof(AccountStatus status) : base(LoginOpcodes.AUTH_LOGIN_PROOF)
      {
         Write((byte)base.Opcode);
         Write((byte)status);
      }
   }
}
