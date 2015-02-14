using System;
using System.Linq;

using Cession.Handles;
using Cession.Drawing;
using Cession.Diagrams;

using UIKit;

namespace Cession.Tools
{
    public class MoveVertexTool:DragDropTool
    {
        private VertexHandle _handle;

        public MoveVertexTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void Enter (Tool parentTool, params object[] args)
        {
            base.Enter (parentTool, args);
            if (args.Length != 1)
                throw new ArgumentException ("args");
            _handle = args [0] as VertexHandle;
            if(null == _handle)
                throw new ArgumentException ("args");
        }

        protected override void DoDrawDragDrop (DrawingContext drawingContext)
        {
            drawingContext.SaveState ();
            drawingContext.CGContext.SetAlpha (.5f);
            if (_handle.Shape is Polyline)
            {
                var polyline = _handle.Shape as Polyline;
                var segment = polyline.Segments.Last ();
                drawingContext.StrokeLine (segment.Point1, EndPoint.Value);
            }
            else if (_handle.Shape is LineSegment)
            {
                var line = _handle.Shape as LineSegment;
                var prevLine = line.Previous;
                if (prevLine != null)
                {
                    drawingContext.StrokeLine (prevLine.Point1, EndPoint.Value);
                }
                drawingContext.StrokeLine (EndPoint.Value, line.Point2);
            }
            drawingContext.RestoreState ();
        }

        protected override void Commit ()
        {
            if (_handle.Shape is Polyline)
            {
                var polyline = _handle.Shape as Polyline;
                CommandManager.Execute (polyline, EndPoint.Value, polyline.LastPoint, (pl, p) => pl.LastPoint = p);
            }
            else if(_handle.Shape is Segment)
            {
                var line = _handle.Shape as Segment;
                CommandManager.Execute (line, EndPoint.Value, line.Point1, (l, p) => l.Point1 = p);
            }
        }
    }
}

