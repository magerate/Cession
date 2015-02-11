//using System;
//using System.Linq;
//using System.Collections.Generic;
//
//using CoreGraphics;
//using Foundation;
//using UIKit;
//
//using Cession.Modeling;
//using Cession.Geometries;
//
//namespace Cession.Drawing
//{
//    public class LayerDrawer:ShapeDrawer
//    {
//        public static readonly int LogicalUnitPerDp = 25;
//
//        protected override void DoDraw (CGContext context, Diagram diagram)
//        {
//            var layer = diagram as Layer;
//            if (null == layer)
//                throw new ArgumentException ("diagram");
//
//            DrawHelper.Transform = layer.Transform;
//
//            foreach (var item in layer.Diagrams)
//            {
//                item.Draw (context);
//            }
//
//            foreach (var roomGroup in layer.RoomGroups)
//            {
//                DrawRoomGroup (context, roomGroup);
//            }
//        }
//
//        public static Matrix GetLayerDefaultTransform ()
//        {
//            return Layer.GetDefaultTransform (Layer.DefaultSize, 
//                (int)UIScreen.MainScreen.Bounds.Width,
//                (int)UIScreen.MainScreen.Bounds.Height,
//                LogicalUnitPerDp);
//        }
//
//        private void DrawRoomGroup (CGContext context, RoomGroup roomGroup)
//        {
//            var polygons = roomGroup.Select (r => r.OuterContour as IEnumerable<Point2>);
//            var outer = PolygonHelper.Union (polygons).ToList ();
//            context.BuildPolygonPath (outer);
//
//            foreach (var room in roomGroup)
//            {
//                context.BuildFigurePath (room.Contour);
//            }
//
//            context.SaveState ();
//            UIColor.Gray.SetFill ();
//            context.DrawPath (CGPathDrawingMode.EOFillStroke);
//            context.RestoreState ();
//        }
//    }
//}
//
