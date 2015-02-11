using System;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
    public class ActionItem:LabelItem
    {
        public Action<NSIndexPath,ActionItem> Action{ get; set; }

        public ActionItem ()
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public ActionItem (Action<NSIndexPath,ActionItem> action, string title)
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator;
            this.Action = action;
            this.Title = title;
        }

        public ActionItem (Action action) : this (action, string.Empty)
        {
        }

        public ActionItem (Action action, string title)
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator;
            this.Action = (ip, ai) => action.Invoke ();
            this.Title = title;
        }

        protected override void DoActive (NSIndexPath indexPath, UITableViewCell cell, object data)
        {
            base.DoActive (indexPath, cell, data);
        }

        public override void Select (DetailViewController controller, NSIndexPath indexPath)
        {
            if (null != Action)
                Action.Invoke (indexPath, this);
        }
    }
}

