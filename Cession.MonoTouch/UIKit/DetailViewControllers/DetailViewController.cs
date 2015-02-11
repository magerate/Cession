using System;
using System.Collections.Generic;

using UIKit;
using Foundation;
using CoreGraphics;

namespace Cession.UIKit
{
    public class DetailViewController:UITableViewController
    {
        private object dataContext;

        public object DataContext
        {
            get{ return dataContext; }
            set
            {
                if (value != dataContext)
                {
                    dataContext = value;
                    if (IsViewLoaded)
                        TableView.ReloadData ();
                }
            }
        }


        public List<DetailSection> DetailSections{ get; private set; }

        public Action<DetailItem> ValueDidChangeAction{ get; set; }

        public Action<DetailItem> ValueWillChangeAction{ get; set; }

		
        public DetailViewController () : base (UITableViewStyle.Grouped)
        {
            DetailSections = new List<DetailSection> ();
        }

        public DetailViewController (UITableViewStyle style) : base (style)
        {
            DetailSections = new List<DetailSection> ();
        }

        public override nint NumberOfSections (UITableView tableView)
        {
            return DetailSections.Count;
        }

        public override nint RowsInSection (UITableView tableview, nint section)
        {
            var ds = DetailSections [(int)section];
            return ds.Items.Count;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var item = GetDetailItem (indexPath);
            var cell = tableView.DequeueReusableCell (item.CellId);
            if (null == cell)
                cell = item.GetCell (tableView, indexPath);

            item.Active (indexPath, cell, this);

            return cell;
        }

        //		public override void WillDisplay (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        //		{
        //			var item = GetDetailItem (indexPath);
        //			item.WillDisplay (indexPath, cell, dataContext);
        //		}


        protected DetailItem GetDetailItem (NSIndexPath indexPath)
        {
            return DetailSections [indexPath.Section].Items [indexPath.Row];
        }

        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            var item = GetDetailItem (indexPath);
            item.Select (this, indexPath);
        }

        public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
        {
            var item = GetDetailItem (indexPath);
            item.AccessoryButtonTapped (this, indexPath);
        }

        public override string TitleForFooter (UITableView tableView, nint section)
        {
            var ds = DetailSections [(int)section];
            return ds.FooterTitle;
        }

        public override string TitleForHeader (UITableView tableView, nint section)
        {
            var ds = DetailSections [(int)section];
            return ds.HeaderTitle;
        }

        public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
        {
            var item = GetDetailItem (indexPath);
            if (item.Height.HasValue)
                return item.Height.Value;
            return tableView.RowHeight;
        }

        public override void ViewWillDisappear (bool animated)
        {
            base.ViewWillDisappear (animated);
        }

        protected void FromItems (IEnumerable<DetailItem> items)
        {
            DetailSections.Clear ();

            var section = new DetailSection ();
            DetailSections.Add (section);

            section.Items.AddRange (items);
        }
    }
}

