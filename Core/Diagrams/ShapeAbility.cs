using System;

namespace Cession.Diagrams
{
    [Flags]
    internal enum ShapeAbility
    {
        None = 0,
        CanSelect = 0x1,
        CanOffset = 0x2,
        CanRotate = 0x4,
        CanAssign = 0x8,
        All = CanSelect | CanOffset | CanRotate | CanAssign,
    }
}

