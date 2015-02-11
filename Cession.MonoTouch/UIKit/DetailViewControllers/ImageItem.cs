using System;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
	public class ImageItem:DetailItem
	{
		static readonly NSString cellId = new NSString("imageId");

		public override NSString CellId {
			get {return cellId;}
		}


		public ImageItem ()
		{
		}

		public override UITableViewCell GetCell (UITableView tableView,NSIndexPath indexPath)
		{
			var cell = new TextureCell (CellId);
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell;
		}

		protected override void DoActive (NSIndexPath indexPath, UITableViewCell cell, object data)
		{
			var imageCell = cell as TextureCell;
			imageCell.TextLabel.Text = Title;

			var value = Getter.Invoke (data);
			var path = value == null ? string.Empty : value.ToString ();
			var image = UIImage.FromFile (path);
			if (null == image)
				image = UIImage.FromBundle (string.Empty);
			cell.ImageView.Image = image;
		}

		public override void Select (DetailViewController controller, NSIndexPath indexPath)
		{
			var tableView = controller.TableView;
			var cell = tableView.CellAt(indexPath);

			if (DeviceHelper.IsPad ()) {
				var ipc = new ImagePickerController ();
				ipc.PostSelectImageAction = i => {
					SetValue(controller.DataContext,i);
					PopoverControllerManager.Dismiss(true);
					tableView.ReloadCell(indexPath.Row,indexPath.Section);
				};

				PopoverControllerManager.ShowPopoverController (ipc, 
					p => PopoverControllerManager.PresentPopover (p, cell));

			} else {
				var ipc = new ImagePickerController();
				ipc.PostSelectImageAction = i => {
					SetValue(controller.DataContext,i);
					controller.DismissViewController(true,null);
				};

				controller.PresentViewController(ipc,true,null);
			}
		}
	}
}

