using System;

using Cession.Geometries;
using Cession.Diagrams;
using D = Cession.Diagrams;
using G = Cession.Geometries;

namespace Cession.Handles
{
    public enum RectangleHandleTypes
    {
        Left,
        Top,
        Right,
        Bottom,
    }

    public class RectangleHandle:Handle
    {
        public static Type TargetToolType{ get; set;}

        private RectangleHandleTypes _type;

        public RectangleHandleTypes Type
        {
            get{ return _type; }
        }

        public override Point Location
        {
            get
            {
                switch (_type)
                {
                case RectangleHandleTypes.Left:
                    return new Point (Rectangle.Rect.X, Rectangle.Rect.Y + Rectangle.Rect.Height / 2);
                case RectangleHandleTypes.Top:
                    return new Point (Rectangle.Rect.X + Rectangle.Rect.Width / 2, Rectangle.Rect.Y);
                case RectangleHandleTypes.Right:
                    return new Point (Rectangle.Rect.Right, Rectangle.Rect.Y + Rectangle.Rect.Height / 2);
                case RectangleHandleTypes.Bottom:
                    return new Point (Rectangle.Rect.X + Rectangle.Rect.Width / 2, Rectangle.Rect.Bottom );
                default:
                    throw new InvalidOperationException ();
                }
            }
        }

        public Rectangle Rectangle
        {
            get{ return Shape as Rectangle; }
        }

        public override Type ToolType
        {
            get{ return TargetToolType; }
        }


        public RectangleHandle (Rectangle rectangle,RectangleHandleTypes type) : base (rectangle)
        {
            _type = type;
        }
    }
}

