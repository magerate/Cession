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
        All = CanSelect | CanOffset | CanRotate,
    }
}

