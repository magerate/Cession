using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace Cession.UIKit
{
	public class TextViewItem:DetailItem
	{
		static readonly NSString cellId = new NSString("textViewId");

		public override NSString CellId {
			get {return cellId;}
		}

		public string Placeholder{get;set;}	

		public TextViewItem ()
		{
			Height = 120.0f;
			NeedReloadSelf = false;
		}

		public override UITableViewCell GetCell (UITableView tableView,NSIndexPath indexPath)
		{
			var cell = new TextViewCell (CellId);
			return cell;
		}

		protected override void DoActive (NSIndexPath indexPath,UITableViewCell cell, object data)
		{
			var textCell = cell as TextViewCell;
			textCell.TextView.Text = GetValueDescription (data);		
			textCell.TextView.Placeholder = Placeholder;
			textCell.TextView.SetNeedsDisplay ();
			textCell.TextView.ShouldEndEditing = (tf) => {
				SetValue (data, tf.Text);
				return true;
			};
		}
	}
}

