namespace Cession.UIKit
{
	using System;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class LabelItem:DetailItem
	{
		static readonly NSString cellId = new NSString("labelId");

		public override NSString CellId {
			get {return cellId;}
		}

		public UITableViewCellAccessory Accessory{get;set;}

		public override UITableViewCell GetCell (UITableView tableView,NSIndexPath indexPath)
		{
			var cell = new UITableViewCell (UITableViewCellStyle.Value1, CellId);
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			return cell;
		}

		protected override void DoActive(NSIndexPath indexPath,UITableViewCell cell,object data)
		{
			cell.TextLabel.Text = Title;
			cell.DetailTextLabel.Text = GetValueDescription (data);
			cell.Accessory = Accessory;
		}
	}
}

