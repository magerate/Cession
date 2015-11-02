using System;
using System.Collections.Generic;

using UIKit;
using CoreGraphics;

using Cession.Diagrams;
using Cession.Geometries;
using D = Cession.Diagrams;

namespace Cession.Drawing
{
    public class RoomDrawer:ShapeDrawer
    {
        protected override void DoDraw (DrawingContext drawingContext, Shape shape)
        {
            var room = shape as Room;
            if (null == room)
                throw new ArgumentException ("shape");

            DrawWallSection (drawingContext, room);
            DrawLabe (drawingContext, room);
        }

        private void DrawWallSection(DrawingContext drawingContext, Room room)
        {
            drawingContext.SaveState ();
            UIColor.Gray.SetFill ();
            drawingContext.BuildClosedShapePath (room.OuterContour);
            drawingContext.BuildClosedShapePath (room.Floor.Contour);
            drawingContext.CGContext.DrawPath (CGPathDrawingMode.EOFillStroke);
            drawingContext.RestoreState ();
        }

        private void DrawLabe(DrawingContext drawingContext,Room room)
        {
            room.Label.Draw (drawingContext);
        }
    }
}

