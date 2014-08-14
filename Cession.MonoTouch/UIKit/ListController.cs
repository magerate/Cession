namespace Cession.UIKit
{
	using System;
	using System.Drawing;
	using System.Collections;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class ListController:UITableViewController
	{
		protected static NSString reuseId = new NSString("listCellReuseId");


		public IList DataSource{get;set;}
		public Func<object,string> Formatter{get;set;}
		public Action<object> SelectedAction{get;set;}

		public int SelectedIndex{get;set;}
		public object SelectedValue
		{ 
			get{
				if (SelectedIndex == -1 || DataSource == null)
					return null;
				return DataSource [SelectedIndex];
			} 

			set
			{
				if (null == DataSource)
					return;
				SelectedIndex = DataSource.IndexOf (value);
			}
		}


		private int preSelectedIndex = -1;
		public bool DelayChanges{get;set;}

		public ListController ():this(UITableViewStyle.Grouped)
		{
			DelayChanges = false;
		}

		public ListController(UITableViewStyle style):base(style)
		{
			SelectedIndex = -1;
		}

		protected string GetItemDescription(object obj)
		{
			if(null == Formatter)
				return obj.ToString();
			return Formatter.Invoke(obj);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			if (SelectedAction != null && DelayChanges) {
				if (SelectedIndex >= 0 && SelectedIndex < DataSource.Count) {
					var item = DataSource [SelectedIndex];
					SelectedAction.Invoke (item);
				}
			}
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			if (null == DataSource)
				return 0;
			return DataSource.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var item = DataSource[indexPath.Row];

			var cell = tableView.DequeueReusableCell(reuseId);
			if(null == cell)
				cell = new UITableViewCell(UITableViewCellStyle.Default,reuseId);
			cell.TextLabel.Text = GetItemDescription(item);
			if(indexPath.Row == SelectedIndex)
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			else
				cell.Accessory = UITableViewCellAccessory.None;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var item = DataSource[indexPath.Row];

			tableView.DeselectRow(indexPath,true);
			preSelectedIndex = SelectedIndex;
			SelectedIndex = indexPath.Row;

			if(preSelectedIndex >=0 && 
			   preSelectedIndex < DataSource.Count &&
			   preSelectedIndex != SelectedIndex)
			{
				tableView.ReloadRows(new NSIndexPath[]{indexPath,NSIndexPath.FromRowSection(preSelectedIndex,0)},
					UITableViewRowAnimation.Automatic);
			}
			else
				tableView.ReloadRows(new NSIndexPath[]{indexPath},UITableViewRowAnimation.Automatic);

			if(SelectedAction != null && !DelayChanges)
				SelectedAction.Invoke(item);
		}
	}

}

