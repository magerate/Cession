using System;

namespace Cession.Diagrams
{
	[Flags]
	internal enum ShapeAbility
	{
		None = 0,
		CanSelect = 0x1,
		CanHitTest = 0x2,
		CanOffset = 0x4,
		CanRotate = 0x8,
		CanAssign = 0x10,
		All = CanSelect | CanHitTest | CanOffset | CanRotate | CanAssign,
	}
}

