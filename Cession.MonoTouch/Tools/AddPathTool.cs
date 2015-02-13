using System;
using System.Linq;

using CoreGraphics;
using UIKit;

using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using Cession.UIKit;

namespace Cession.Tools
{
    public class AddPathTool:AddPolygonalShapeTool
    {
        public AddPathTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void DoubleTap (UITapGestureRecognizer gestureRecognizer)
        {
            Complete ();
        }

        protected override void Commit ()
        {
            var path = new Path (Measurer.Points);
            ExecuteAddShape (path);
        }
    }
}

