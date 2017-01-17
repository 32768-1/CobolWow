using System.Runtime.InteropServices;
using CobolWow.Game.Constants.Game.World.Spell;

namespace CobolWow.DBC.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    [DBCFileAttribute("Spell")]
    public unsafe struct SpellEntry
    {
        [FieldOffset(0)]
        public int ID;

        [FieldOffset(2 * 4)]
        public int Category;

        [FieldOffset(6 * 4)]
        public SpellAttributes Attributes;

        [FieldOffset(19 * 4)]
        public int RecoveryTime;

        [FieldOffset(20 * 4)]
        public int CategoryRecoveryTime;

        [FieldOffset(36 * 4)]
        public int rangeIndex;

        [FieldOffset(37 * 4)]
        public float Speed;
    };
}
