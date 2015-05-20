using System;

using Cession.Diagrams;
using Cession.Geometries;
using Cession.Commands;
using D = Cession.Diagrams;

namespace Cession.Handles
{
    public class VertexHandle:Handle
    {
        public static double Size{ get; set;}

        static VertexHandle()
        {
            Size = 24;
        }

        public static Type TargetToolType{ get; set;}
        public bool IsFirstVertex{ get; private set;}

        public override Type ToolType
        {
            get{return TargetToolType;}
        }

        public D.Segment Segment
        {
            get{ return Shape as D.Segment; }
        }

        public override Point Location
        {
            get
            {
                if (IsFirstVertex)
                    return Segment.Point1;
                return Segment.Point2;
            }
        }

        public VertexHandle (D.Segment segment,bool isFirstVertex):base(segment)
        {
            IsFirstVertex = isFirstVertex;
        }

        public override bool Contains (Point point)
        {
            double dx = Math.Abs (point.X - Location.X);
            double dy = Math.Abs (point.Y - Location.Y);

            var transform = Transform;
            double delta = (Size/2) / transform.M11;

            return dx <= delta && dy <= delta;
        }

        //bounds of handles is in device coordiante space
        //then handle won't scale when diagram scaled
        public Rect GetBounds()
        {
            var point = Transform.Transform (Location);
            return new Rect (point.X - Size / 2, point.Y - Size / 2, Size, Size);
        }
    }
}

