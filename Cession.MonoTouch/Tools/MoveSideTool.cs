//using System;
//using System.Collections.Generic;
//
//using CoreGraphics;
//
//using Cession.Diagrams;
//using Cession.Drawing;
//using Cession.Geometries;
//
////using Cession.Handles;
//using Cession.Commands;
//
//namespace Cession.Tools
//{
//    public class MoveSideTool:DragDropTool
//    {
//        private SideHandle handle;
//
//        public MoveSideTool (ToolManager toolManager) : base (toolManager)
//        {
//        }
//
//        public override void Enter (Tool parentTool, params object[] args)
//        {
//            base.Enter (parentTool, args);
//
//            if (null == args)
//                throw new ArgumentNullException ();
//
//            if (args.Length != 1)
//                throw new ArgumentException ();
//
//            handle = args [0] as SideHandle;
//
//            if (null == handle)
//                throw new ArgumentException ();
//        }
//
//        protected override void DoDraw (CGContext context)
//        {
//            var segment = handle.MoveSide (endPoint.Value);
//            var prevSegment = handle.PreviousSide;
//            var nextSegment = handle.NextSide;
//
//            if (prevSegment.HasValue)
//                context.StrokeLine (prevSegment.Value.P1, segment.P1);
//            context.StrokeLine (segment.P1, segment.P2);
//            if (nextSegment.HasValue)
//                context.StrokeLine (segment.P2, nextSegment.Value.P2);
//        }
//
//        protected override void Commit ()
//        {
//            var diagram = handle.Diagram;
//            if (diagram is RectangleDiagram)
//            {
//                var rectDiagram = diagram as RectangleDiagram;
//                var rect = rectDiagram.MoveSide (handle.Index,
//                    endPoint.Value.X - startPoint.Value.X,
//                    endPoint.Value.Y - startPoint.Value.Y);
//
//                CommandManager.Execute (rect, rectDiagram.Rect, r => rectDiagram.Rect = r);
//            } else if (diagram is PathDiagram)
//            {
//                var targetSegment = handle.MoveSide (endPoint.Value);
//                var currentSegment = handle.Side;
//                var pathDiagram = diagram as PathDiagram;
//
//                var command = Command.Create (handle.Index, targetSegment, currentSegment, pathDiagram.MoveSide);
//                CommandManager.Execute (command);
//            }
//        }
//    }
//}
//
