using System;

using Cession.Geometries;
using Cession.Diagrams;
using D = Cession.Diagrams;
using G = Cession.Geometries;

namespace Cession.Handles
{
    public enum CircleHandleTypes
    {
        Left,
        Top,
        Right,
        Bottom,
    }

    public class CircleHandle:Handle
    {
        private CircleHandleTypes _type;

        public override Point Location
        {
            get
            {
                switch (_type)
                {
                case CircleHandleTypes.Left:
                    return new Point (Circle.Center.X - Circle.Radius,Circle.Center.Y);
                case CircleHandleTypes.Top:
                    return new Point (Circle.Center.X ,Circle.Center.Y - Circle.Radius);
                case CircleHandleTypes.Right:
                    return new Point (Circle.Center.X + Circle.Radius, Circle.Center.Y);
                case CircleHandleTypes.Bottom:
                    return new Point (Circle.Center.X, Circle.Center.Y + Circle.Radius );
                default:
                    throw new InvalidOperationException ();
                }
            }
        }

        public D.Circle Circle => Shape as D.Circle;
        public override Type ToolType => null;

        public CircleHandle (D.Circle circle,CircleHandleTypes type) : base (circle)
        {
            _type = type;
        }
    }
}

