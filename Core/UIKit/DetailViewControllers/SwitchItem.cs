namespace Cession.UIKit
{
	using System;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class SwitchItem:DetailItem
	{
		static readonly NSString cellId = new NSString("switchId");

		public override NSString CellId {
			get {return cellId;}
		}

		public SwitchItem ()
		{
			NeedReloadSelf = false;
		}

		public override UITableViewCell GetCell (UITableView tableView,NSIndexPath indexPath)
		{
			var cell = new SwitchCell (CellId);
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell;
		}

		protected override void DoActive (NSIndexPath indexPath,UITableViewCell cell, object data)
		{
			var switchCell = cell as SwitchCell;
			switchCell.TextLabel.Text = Title;
			switchCell.Switch.On = (bool)GetValue (data);
			switchCell.SwitchValueChangedAction = SwitchValueChange;
		}

		private void SwitchValueChange(bool value)
		{
			var data = detailController.DataContext;
			if (null != data)
				SetValue (data, value);
		}
	}
}

