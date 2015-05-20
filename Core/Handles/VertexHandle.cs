using System;

using Cession.Diagrams;
using Cession.Geometries;
using Cession.Commands;
using D = Cession.Diagrams;

namespace Cession.Handles
{
    public class VertexHandle:Handle
    {
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
    }
}

