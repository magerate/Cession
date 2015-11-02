using System;
using System.Collections.Generic;

using CoreGraphics;

using Cession.Diagrams;
using Cession.Drawing;
using Cession.Geometries;
using Cession.Handles;
using Cession.Commands;
using D = Cession.Diagrams;

namespace Cession.Tools
{
    public class MoveLineTool:DragDropTool
    {
        private LineHandle _handle;

        public MoveLineTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void Enter (Tool parentTool, params object[] args)
        {
            base.Enter (parentTool, args);

            if (null == args)
                throw new ArgumentNullException ();

            if (args.Length != 1)
                throw new ArgumentException ();

            _handle = args [0] as LineHandle;

            if (null == _handle)
                throw new ArgumentException ();
        }

        protected override void DoDrawDragDrop (DrawingContext drawingContext)
        {
            D.Segment segment = _handle.Line;
            D.Segment prevSegment = segment.Previous;
            D.Segment nextSegment = segment.Next;

            var pair = _handle.MoveLine (EndPoint.Value);

            Point p1 = pair.Item1;
            Point p2 = pair.Item2;

            CGContext context = drawingContext.CGContext;
            context.SaveState ();
            context.SetAlpha (.5f);
            if (prevSegment != null)
                drawingContext.DrawSegment (prevSegment, p1,true);
            drawingContext.StrokeLine (p1, p2);
            if (nextSegment != null)
                drawingContext.DrawSegment (nextSegment,p2,false);
            context.RestoreState ();
        }

        protected override void Commit ()
        {
            LineSegment line = _handle.Line;
            var pair = _handle.MoveLine (EndPoint.Value);
            var oldPair = new Tuple<Point,Point> (line.Point1, line.Point2);
            var command = Command.Create (line, pair, oldPair, (l, p) => l.Move (p));
            CommandManager.Execute (command);
        }
    }
}

