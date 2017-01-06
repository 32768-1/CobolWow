
using CobolWow.Network;
using CobolWow.Game.Constants;
using CobolWow.Tools.Extensions;
using CobolWow.Tools.Cryptography;
using CobolWow.Network.Packets;

namespace CobolWow.Communication.Outgoing.Auth
{
   class PSAuthLoginProof : ServerPacket
   {
      public PSAuthLoginProof(SRP6 Srp6) : base(LoginOpcodes.AUTH_LOGIN_PROOF)
      {
         Write((byte)1);
         Write((byte)AccountStatus.Ok);
         Write(Srp6.M2);
         this.WriteNullByte(4);
      }
   }
}
