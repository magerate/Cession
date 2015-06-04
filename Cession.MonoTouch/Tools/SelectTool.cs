using System;
using System.Linq;
using System.Collections.Generic;

using Cession.UIKit;
using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.Handles;
using Cession.Projects;

using CoreGraphics;
using UIKit;

namespace Cession.Tools
{
    public class SelectTool:Tool
    {
        private CGPoint _touchPoint;

        protected IReadOnlyList<Handle> Handles
        {
            get{ return Host.HandleManager.Handles; }
        }

        public SelectTool (ToolManager toolManager) : base (toolManager)
        {
            VertexHandle.TargetToolType = typeof(MoveVertexTool);
            LineHandle.TargetToolType = typeof(MoveLineTool);
            ArcHandle.TargetToolType = typeof(MoveArcTool);
            CircleHandle.TargetToolType = typeof(AdjustCircleTool);
            RectangleHandle.TargetToolType = typeof(AdjustRectTool);
        }

        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.IsBegan ())
            {
                if (!TryOperateDiagram ())
                {
                    ToolManager.PushTool (typeof(PanTool));
                }
                ToolManager.CurrentTool.TouchBegin (_touchPoint);
                ToolManager.CurrentTool.Pan (gestureRecognizer);
            }
        }

        public override void TouchBegin (CGPoint point)
        {
            _touchPoint = point;
        }

        private bool TryOperateDiagram ()
        {
            if (TryPanHandle ())
                return true;

            if (TryMove ())
                return true;

            return false;
        }

        private bool TryPanHandle ()
        {
            if (Handles == null)
                return false;

            Point tp = ConvertToLogicalPoint (_touchPoint);
            foreach (var h in Handles)
            {
                if (h.Contains (tp))
                {
                    ToolManager.PushTool (h.ToolType, h);
                    return true;
                }
            }
            return false;
        }

        private bool TryMove ()
        {
            HitTest (ConvertToLogicalPoint (_touchPoint));
            if (CurrentLayer.SelectedShapes.Count > 0 && CurrentLayer.SelectedShapes.CanOffset())
            {
                ToolManager.PushTool (typeof(MoveTool), CurrentLayer.SelectedShapes.ToArray());
                return true;
            }
            return false;
        }

        public override void Tap (UITapGestureRecognizer gestureRecognizer)
        {
            var point = GetLogicPoint (gestureRecognizer);
            HitTest (point);
            RefreshToolView ();
        }

        private void HitTest (Point point)
        {
            var shapes = GetShapesToHitTest (CurrentLayer);
            var shape = shapes.HitTestAny (point,s => !(s is Label));

            CurrentLayer.ClearSelection ();
            if (null != shape)
            {
                shape = shape.GetSelectableAncestor ();
                if (null != shape)
                    CurrentLayer.Select (shape);
            }
            RefreshToolView ();
        }

        private IEnumerable<Shape> GetShapesToHitTest(Layer layer)
        {
            var foldShapes = layer.SelectedShapes.
                Select(s => s.GetAncestor<IFoldableHost>()).
                Where(s => s != null).
                Cast<IFoldableHost>().
                SelectMany(f => f.GetFoldableShapes ());

            if (foldShapes.Count () <= 0)
                return layer.Shapes;
            return layer.Shapes.Concat (foldShapes);
        }
    }
}

