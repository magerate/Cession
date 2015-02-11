using System;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
    public class ColorItem:DetailItem
    {
        static readonly NSString cellId = new NSString ("colorId");

        public override NSString CellId
        {
            get { return cellId; }
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new ColorCell (CellId);
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            return cell;
        }

        protected override void DoActive (NSIndexPath indexPath, UITableViewCell cell, object data)
        {
            var colorCell = cell as ColorCell;
            colorCell.TextLabel.Text = Title;

            var color = GetValue (data) as UIColor;
            colorCell.ColorView.BackgroundColor = color;
        }

        public override void Select (DetailViewController controller, NSIndexPath indexPath)
        {
            var cp = new ColorPicker ();
            cp.SelectedAction = c =>
            {
                controller.NavigationController.PopViewController (true);
                SetValue (controller.DataContext, c);
            };

            controller.NavigationController.PushViewController (cp, true);
        }

    }
}

