﻿using System;

namespace CobolWow.Game.Constants.Game.Update
{
   [Flags]
   public enum MovementFlags
   {
       MOVEFLAG_NONE               = 0x00000000,
       MOVEFLAG_FORWARD            = 0x00000001,
       MOVEFLAG_BACKWARD           = 0x00000002,
       MOVEFLAG_STRAFE_LEFT        = 0x00000004,
       MOVEFLAG_STRAFE_RIGHT       = 0x00000008,
       MOVEFLAG_TURN_LEFT          = 0x00000010,
       MOVEFLAG_TURN_RIGHT         = 0x00000020,
       MOVEFLAG_PITCH_UP           = 0x00000040,
       MOVEFLAG_PITCH_DOWN         = 0x00000080,
       MOVEFLAG_WALK_MODE          = 0x00000100,               // Walking

       MOVEFLAG_LEVITATING         = 0x00000400,
       MOVEFLAG_FLYING             = 0x00000800,               // [-ZERO] is it really need and correct value
       MOVEFLAG_FALLING            = 0x00002000,
       MOVEFLAG_FALLINGFAR         = 0x00004000,
       MOVEFLAG_SWIMMING           = 0x00200000,               // appears with fly flag also
       MOVEFLAG_SPLINE_ENABLED     = 0x00400000,               // [-ZERO] is it really need and correct value
       MOVEFLAG_CAN_FLY            = 0x00800000,               // [-ZERO] is it really need and correct value
       MOVEFLAG_FLYING_OLD         = 0x01000000,               // [-ZERO] is it really need and correct value

       MOVEFLAG_ONTRANSPORT        = 0x02000000,               // Used for flying on some creatures
       MOVEFLAG_SPLINE_ELEVATION   = 0x04000000,               // used for flight paths
       MOVEFLAG_ROOT               = 0x08000000,               // used for flight paths
       MOVEFLAG_WATERWALKING       = 0x10000000,               // prevent unit from falling through water
       MOVEFLAG_SAFE_FALL          = 0x20000000,               // active rogue safe fall spell (passive)
       MOVEFLAG_HOVER              = 0x40000000
   }
}