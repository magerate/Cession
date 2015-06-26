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
    public class AddElevationTool:AddPolygonalShapeTool
    {
        public AddElevationTool (ToolManager toolManager) : base (toolManager)
        {
        }

        protected override void Commit ()
        {
            if (Measurer.Points.Count > 2)
            {
                var path = Measurer.ToPath ();
                var elevation = new Elevation (path);
                ExecuteAddShape (elevation);
            }
        }
    }
}

