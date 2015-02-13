using System;

using CoreGraphics;

using Cession.Diagrams;
using Cession.Drawing;
using Cession.Geometries;

namespace Cession.Tools
{
    public class AddRectTool:DragDropTool
    {
        public AddRectTool (ToolManager toolManager) : base (toolManager)
        {
        }

        protected override void DoDrawDragDrop (DrawingContext drawingContext)
        {
            var rect = Rect.FromPoints (StartPoint.Value, EndPoint.Value);
            drawingContext.StrokeRect (rect);
//            drawingContext.DrawDimension (rect.LeftTop, rect.RightTop);
//            drawingContext.DrawDimension (rect.RightTop, rect.RightBottom);
        }

        protected override void Commit ()
        {
            var rect = Rect.FromPoints (StartPoint.Value, EndPoint.Value);
            Rectangle rs = new Rectangle (rect);
            CommandManager.ExecuteListAdd (CurrentLayer.Shapes, rs);
        }
    }
}

