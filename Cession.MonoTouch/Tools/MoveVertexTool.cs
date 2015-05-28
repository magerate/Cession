using System;
using System.Linq;

using Cession.Handles;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.Geometries;
using D = Cession.Diagrams;
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
            var segment = _handle.Shape as D.Segment;

            if (!_handle.IsFirstVertex)
            {
                drawingContext.DrawSegment (segment, EndPoint.Value, true);
            }
            else
            {
                var prevLine = segment.Previous;
                if (prevLine != null)
                {
                    drawingContext.DrawSegment (prevLine, EndPoint.Value, true);
                }
                drawingContext.DrawSegment (segment, EndPoint.Value, false);
            }
            drawingContext.RestoreState ();
        }

        protected override void Commit ()
        {
            D.Segment segment = _handle.Shape as D.Segment;
            if (!_handle.IsFirstVertex)
                segment = segment.Next;

            if (segment != null)
                CommandManager.Execute (EndPoint.Value, segment.Point1, segment.MoveVertex);
            else
            {
                Polyline polyline = _handle.Shape.Parent as Polyline;
                CommandManager.Execute (EndPoint.Value, polyline.LastPoint, polyline.MoveLastPoint);
            }
        }
    }
}

