using System;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
    public class FontItem:LabelItem
    {
        static readonly NSString cellId = new NSString ("fontId");

        public override NSString CellId
        {
            get { return cellId; }
        }

        public FontItem ()
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell (UITableViewCellStyle.Value1, CellId);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }

        protected override void DoActive (NSIndexPath indexPath, UITableViewCell cell, object data)
        {
            var fontName = GetValueDescription (data);
            cell.TextLabel.Text = Title;
            cell.DetailTextLabel.Text = fontName;

            var font = UIFont.FromName (fontName, UIFont.SystemFontSize);
            if (null == font)
                font = UIFont.SystemFontOfSize (UIFont.SystemFontSize);
            cell.DetailTextLabel.Font = font;
			
            cell.Accessory = Accessory;
        }

        public override void Select (DetailViewController controller, NSIndexPath indexPath)
        {
            var fontName = GetValueDescription (controller.DataContext).ToString ();
            var fontController = new FontsController ();
            fontController.SelectedFontName = fontName;
            fontController.DelayChanges = true;
            fontController.SelectedAction = (o) =>
            {
                var font = o as UIFont;
                SetValue (controller.DataContext, font.Name);
            };
            controller.NavigationController.PushViewController (fontController, true);
        }
    }
}

