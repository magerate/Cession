﻿using System;

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

        public VertexHandle (Shape shape,Point location):base(shape,location)
        {
        }

        public override Handle HitTest (Point point, Matrix transform)
        {
            double dx = Math.Abs (point.X - Location.X);
            double dy = Math.Abs (point.Y - Location.Y);

            double delta = (Size/2) * transform.M11;

            if (dx <= delta && dy <= delta)
                return this;
            return null;
        }

//        public Command CreateCommand(Point point)
//        {
//            var command = Command.Create (Segment,point, Segment.Point1, (s,p) => s.Point1 = p);
//            return command;
//        }

    }
}

