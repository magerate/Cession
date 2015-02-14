using System;
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
        private HandleManager _handlerManager;

        public IEnumerable<Handle> Handles
        {
            get
            { 
                if (null == _handlerManager)
                    return null;
                return _handlerManager.Handles;
            }
        }

        public SelectTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public void AttachProject(Project project)
        {
            if (null == _handlerManager)
                _handlerManager = new HandleManager ();
            _handlerManager.AttachProject (project);
        }

        public void DetachProject(Project project)
        {
            if (null != _handlerManager)
                _handlerManager.DetachProject (project);
        }

        public override void Pan (UIPanGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.IsBegan ())
            {
                if (!TryOperateDiagram ())
                {
                    _toolManager.PushTool (typeof(PanTool));
                }
                _toolManager.CurrentTool.TouchBegin (_touchPoint);
                _toolManager.CurrentTool.Pan (gestureRecognizer);
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
//            if (CurrentLayer.SelectedDiagrams.Count == 0)
//                return false;
//
//            var room = CurrentLayer.SelectedDiagrams [0] as Room;
//            if (null == room)
//                return false;
//
//            var handles = room.Contour.GetHandles (CurrentLayer.Transform);
//            for (int i = 0; i < handles.Length; i++)
//            {
//                if (handles [i].Contains (_touchPoint.ToPoint2 ()))
//                {
//                    _toolManager.PushTool (ToolType.MoveSide, handles [i]);
//                    return true;
//                }
//            }
            return false;
        }

        private bool TryMove ()
        {
            HitTest (ConvertToLogicalPoint (_touchPoint));
            if (CurrentLayer.SelectedShapes.Count != 0)
            {
                _toolManager.PushTool (typeof(MoveTool), CurrentLayer.SelectedShapes);
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
            var shape = CurrentLayer.HitTest (point);

            CurrentLayer.ClearSelection ();
            if (null != shape)
            {
                shape = shape.GetSelectableAncestor ();
                if (null != shape)
                    CurrentLayer.Select (shape);
            }
            RefreshToolView ();
        }
    }
}

