using System;
using CoreGraphics;

using UIKit;
using Foundation;

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
            polylineItem.Tag = ToolType.AddPolyline;
            polylineItem.Action = toolSelector;
            section.Items.Add (polylineItem);
//
//            var polygonItem = new DetailMenuItem ();
//            polygonItem.Title = "Add Polygonal Room";
//            polygonItem.Image = ImageHelper.GetTemplateImage (ImageFiles.Polygon25);
//            polygonItem.Tag = ToolType.AddPolygonalRoom;
//            polygonItem.Action = toolSelector;
//            section.Items.Add (polygonItem);
        }



    }
}

