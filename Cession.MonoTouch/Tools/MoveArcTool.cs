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
    public class MoveArcTool:DragDropTool
    {
        private ArcHandle _handle;

        public MoveArcTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void Enter (Tool parentTool, params object[] args)
        {
            base.Enter (parentTool, args);

            if (null == args)
                throw new ArgumentNullException ();

            if (args.Length != 1)
                throw new ArgumentException ();

            _handle = args [0] as ArcHandle;

            if (null == _handle)
                throw new ArgumentException ();
        }

        protected override void DoDrawDragDrop (DrawingContext drawingContext)
        {
            ArcSegment segment = _handle.ArcSegment;
            drawingContext.StrokeArc (segment.Point1, EndPoint.Value, segment.Point2);
        }

        protected override void Commit ()
        {
            ArcSegment segment = _handle.ArcSegment;
            var command = Command.CreateSetProperty(segment, EndPoint.Value, "PointOnArc");
            CommandManager.Execute (command);
        }
    }
}

