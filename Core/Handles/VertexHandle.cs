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

        public override bool Contains (Point point, Matrix transform)
        {
            double dx = Math.Abs (point.X - Location.X);
            double dy = Math.Abs (point.Y - Location.Y);

            double delta = (Size/2) / transform.M11;

            return dx <= delta && dy <= delta;
        }

//        public Command CreateCommand(Point point)
//        {
//            var command = Command.Create (Segment,point, Segment.Point1, (s,p) => s.Point1 = p);
//            return command;
//        }

    }
}

