namespace Cession.UIKit
{
	using System;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class ButtonItem:DetailItem
	{
		static readonly NSString cellId = new NSString("buttonId");

		public override NSString CellId {
			get {return cellId;}
		}

		private UIButton button;


		public Action Action{ get; set; }
		public UIColor TitleColor{ get; set; }

		public ButtonItem ()
		{
			button = new UIButton ();
			TitleColor = UIColor.Blue;
		}

		public override UITableViewCell GetCell (UITableView tableView,NSIndexPath indexPath)
		{
			var cell = new UITableViewCell(UITableViewCellStyle.Default,CellId);
			cell.AccessoryView = button;
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			return cell;
		}

		protected override void DoActive (NSIndexPath indexPath, UITableViewCell cell, object data)
		{
			var btn = cell.AccessoryView as UIButton;
			btn.SetTitleColor (TitleColor, UIControlState.Normal);
			btn.SetTitle (Title, UIControlState.Normal);
			btn.SizeToFit ();

			if (btn.AllTargets.Count > 0)
				btn.RemoveTarget (ButtonClicked, UIControlEvent.TouchUpInside);
			btn.AddTarget(ButtonClicked, UIControlEvent.TouchUpInside);
		}

		private void ButtonClicked(object sender,EventArgs e)
		{
			if (null != Action)
				Action.Invoke ();
		}
	}
}

