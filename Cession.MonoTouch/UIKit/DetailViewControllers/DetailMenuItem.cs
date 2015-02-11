using System;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
    public class DetailMenuItem:DetailItem
    {
        static readonly NSString cellId = new NSString ("detailMenuId");

        public override NSString CellId
        {
            get { return cellId; }
        }

        public UIImage Image{ get; set; }

        public Action<DetailMenuItem> Action{ get; set; }

        public UITableViewCellAccessory Accessory{ get; set; }

        public DetailMenuItem ()
        {
            Accessory = UITableViewCellAccessory.None;
        }

        public DetailMenuItem (string title, UIImage image, Action<DetailMenuItem> action)
        {
            Accessory = UITableViewCellAccessory.None;

            this.Title = title;
            this.Image = image;
            this.Action = action;
        }

        public DetailMenuItem (string title, UIImage image, Action action) :
            this (title, image, i => action.Invoke ())
        {

        }

        public DetailMenuItem (string title, string imagePath, Action action) :
            this (title, UIImage.FromBundle (imagePath), action)
        {
        }

        public static DetailMenuItem FromTemplateImage (string title, string imagePath, Action action)
        {
            return new DetailMenuItem (title, ImageHelper.GetTemplateImage (imagePath), action);
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            return new UITableViewCell (UITableViewCellStyle.Default, CellId);
        }

        protected override void DoActive (NSIndexPath indexPath, UITableViewCell cell, object data)
        {
            cell.TextLabel.Text = Title;
            cell.ImageView.Image = Image;
            cell.Accessory = Accessory;
        }

        public override void Select (DetailViewController controller, NSIndexPath indexPath)
        {
            if (null != Action)
                Action.Invoke (this);
        }
    }
}

