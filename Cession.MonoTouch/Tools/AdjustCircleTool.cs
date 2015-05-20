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
    public class AdjustCircleTool:DragDropTool
    {
        private CircleHandle _handle;

        public AdjustCircleTool  (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void Enter (Tool parentTool, params object[] args)
        {
            base.Enter (parentTool, args);
            if (args.Length != 1)
                throw new ArgumentException ("args");
            _handle = args [0] as CircleHandle;
            if(null == _handle)
                throw new ArgumentException ("args");
        }

        protected override void DoDrawDragDrop (DrawingContext drawingContext)
        {
            drawingContext.SaveState ();
            drawingContext.CGContext.SetAlpha (.5f);

            Point center = _handle.Circle.Center;
            double radius = center.DistanceBetween (EndPoint.Value);
            Rect bounds = D.Circle.CalcBounds (center, radius);
            drawingContext.StrokeCircle (bounds);
            drawingContext.RestoreState ();
        }

        protected override void Commit ()
        {
            Point center = _handle.Circle.Center;
            double radius = center.DistanceBetween (EndPoint.Value);
            CommandManager.Execute(_handle.Circle,radius,_handle.Circle.Radius,(c,r) => c.Radius = r);
        }
    }
}

