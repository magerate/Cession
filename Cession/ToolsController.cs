using System;

using UIKit;
using Foundation;
using CoreGraphics;

using Cession.Tools;
using Cession.Resources;

namespace Cession.UIKit
{
    public class ToolsController:DetailViewController
    {
        private Action<DetailMenuItem> toolSelector;

        public ToolsController (Action<DetailMenuItem> toolSelector) : base (UITableViewStyle.Plain)
        {
            this.toolSelector = toolSelector;
            Title = "Tools";
            PreferredContentSize = new CGSize (320, 280);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

            InitializeItems ();
        }

        private void InitializeItems ()
        {
            var section = new DetailSection ();
            DetailSections.Add (section);

            var polylineItem = new DetailMenuItem ();
            polylineItem.Title = "Add Polyline";
            polylineItem.Image = ImageHelper.GetTemplateImage (ImageFiles.Rectangle25);
            polylineItem.Tag = typeof(AddPolylineTool);
//            polylineItem.Tag = typeof(TriangleTestTool);
            polylineItem.Action = toolSelector;
            section.Items.Add (polylineItem);

            var circleItem = new DetailMenuItem ();
            circleItem.Title = "Add Circle";
            circleItem.Image = ImageHelper.GetTemplateImage (ImageFiles.Polygon25);
            circleItem.Tag = typeof(AddCircleTool);
            circleItem.Action = toolSelector;
            section.Items.Add (circleItem);

            var rectItem = new DetailMenuItem ();
            rectItem.Title = "Add Rect";
            rectItem.Image = ImageHelper.GetTemplateImage (ImageFiles.Polygon25);
            rectItem.Tag = typeof(AddRectTool);
            rectItem.Action = toolSelector;
            section.Items.Add (rectItem);

            var polygonItem = new DetailMenuItem ();
            polygonItem.Title = "Add Polygon";
            polygonItem.Image = ImageHelper.GetTemplateImage (ImageFiles.Polygon25);
            polygonItem.Tag = typeof(AddPathTool);
            polygonItem.Action = toolSelector;
            section.Items.Add (polygonItem);

            var elevationItem = new DetailMenuItem ();
            elevationItem.Title = "Add Elevation";
            elevationItem.Image = ImageHelper.GetTemplateImage (ImageFiles.Polygon25);
            elevationItem.Tag = typeof(AddElevationTool);
            elevationItem.Action = toolSelector;
            section.Items.Add (elevationItem);

            var circleElevationItem = new DetailMenuItem ();
            circleElevationItem.Title = "Add Circle Elevation";
            circleElevationItem.Image = ImageHelper.GetTemplateImage (ImageFiles.Polygon25);
            circleElevationItem.Tag = typeof(AddCircleElevationTool);
            circleElevationItem.Action = toolSelector;
            section.Items.Add (circleElevationItem);
        }
    }
}

