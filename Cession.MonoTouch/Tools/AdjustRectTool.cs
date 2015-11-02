using System;
using System.Linq;

using Cession.Handles;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.Geometries;
using Cession.Commands;
using D = Cession.Diagrams;
using UIKit;

namespace Cession.Tools
{
    public class AdjustRectTool:DragDropTool
    {
        private RectangleHandle _handle;

        public AdjustRectTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void Enter (Tool parentTool, params object[] args)
        {
            base.Enter (parentTool, args);
            if (args.Length != 1)
                throw new ArgumentException ("args");
            _handle = args [0] as RectangleHandle;
            if(null == _handle)
                throw new ArgumentException ("args");
        }

        protected override void DoDrawDragDrop (DrawingContext drawingContext)
        {
            drawingContext.SaveState ();
            drawingContext.CGContext.SetAlpha (.5f);
            Rect rect = GetTargetRect ();
            drawingContext.StrokeRect (rect);
            drawingContext.RestoreState ();
        }

        protected override void Commit ()
        {
            Rect rect = GetTargetRect ();
            var command = Command.Create (_handle.Rectangle, rect, _handle.Rectangle.Rect, (r, rc) => r.Rect = rc);
            CommandManager.Execute (command);
        }

        private Rect GetTargetRect()
        {
            Rect rect = _handle.Rectangle.Rect;
            if (_handle.Type == RectangleHandleTypes.Left)
            {
                rect.Width = rect.Right - EndPoint.Value.X;
                rect.X = EndPoint.Value.X;
            }
            else if (_handle.Type == RectangleHandleTypes.Top)
            {
                rect.Height = rect.Bottom - EndPoint.Value.Y;
                rect.Y = EndPoint.Value.Y;
            }
            else if (_handle.Type == RectangleHandleTypes.Right)
            {
                rect.Width = EndPoint.Value.X - rect.X;
            }
            else if (_handle.Type == RectangleHandleTypes.Bottom)
            {
                rect.Height = EndPoint.Value.Y - rect.Y;
            }

            if (rect.Width < 0)
            {
                rect.X = rect.X + rect.Width;
                rect.Width = -rect.Width;
            }

            if (rect.Height < 0)
            {
                rect.Y = rect.Y + rect.Height;
                rect.Height = -rect.Height;
            }

            return rect;
        }
    }
}

