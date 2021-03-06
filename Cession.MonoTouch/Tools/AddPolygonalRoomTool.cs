//using System;
//using System.Linq;
//using System.Collections.Generic;
//
//
//using UIKit;
//using CoreGraphics;
//using Foundation;
//
//using Cession.UIKit;
//using Cession.Geometries;
//using Cession.Diagrams;
//using Cession.Drawing;
//using Cession.Alignments;
//using Cession.Commands;
//
//namespace Cession.Tools
//{
//    public class AddPolygonalRoomTool:Tool
//    {
//        private List<Point> points = new List<Point> ();
//        private Point? currentPoint;
//
//        private DevicePointToPointRule p2pRule = new DevicePointToPointRule ();
//
//        public AddPolygonalRoomTool (ToolManager toolManager) : base (toolManager)
//        {
//            p2pRule.Scale = (float)(1 / CurrentLayer.Scale);
//        }
//
//        public override void TouchBegin (CGPoint point)
//        {
//            if (points.Count == 0)
//            {
//                var lp = ConvertToLogicalPoint (point);
//                points.Add (lp);
//                p2pRule.ReferencePoint = lp;
//            }
//        }
//
//        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
//        {
//            if (gestureRecognizer.IsDone ())
//            {
//                if (p2pRule.IsAligned)
//                {
//                    AddRoom ();
//                    Clear ();
//                    RefreshToolView ();
//                } else
//                {
//                    points.Add (currentPoint.Value);
//                    currentPoint = null;
//                }
//                return;
//            }
//
//            currentPoint = GetLogicPoint (gestureRecognizer);
//            if (points.Count > 2)
//                currentPoint = p2pRule.Align (currentPoint.Value);
//
//            RefreshToolView ();
//        }
//
//        private void Clear ()
//        {
//            points.Clear ();
//            currentPoint = null;
//            p2pRule.Reset ();
//        }
//
//        private void AddRoom ()
//        {
////            var polygon = new PathDiagram (points);
////            var room = new Room (polygon, CurrentLayer);
////            room.Name = CurrentLayer.CreateRoomName ();
////            CommandManager.ExecuteListAdd (CurrentLayer.Diagrams, room);
//        }
//
//        protected override void DoDraw (CGContext context)
//        {
////            if (points.Count > 1)
////            {
////                for (int i = 0; i < points.Count - 1; i++)
////                {
////                    context.StrokeLine (points [i], points [i + 1]);
////                }
////            }
////
////            if (currentPoint.HasValue)
////            {
////                context.StrokeLine (points.Last (), currentPoint.Value);
////            }
////
////            p2pRule.Draw (context, CurrentLayer.Transform);
//        }
//    }
//}
//
