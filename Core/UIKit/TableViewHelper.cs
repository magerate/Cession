namespace Cession.UIKit
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public static class TableViewHelper
	{
		public static void ReloadCell(this UITableView tableView,int row,int section = 0)
		{
			var indexPath = NSIndexPath.FromRowSection(row,section);
			tableView.ReloadRows(new NSIndexPath[]{indexPath},UITableViewRowAnimation.Automatic);
		}

		public static void ReloadCell(this UITableView tableView,
		                              NSIndexPath indexPath,
		                              UITableViewRowAnimation withRowAnimation = UITableViewRowAnimation.Automatic)
		{
			tableView.ReloadRows (new NSIndexPath[] { indexPath }, withRowAnimation);
		}

		public static void InsertRow(this UITableView tableView,
										NSIndexPath indexPath,
										UITableViewRowAnimation withRowAnimation = UITableViewRowAnimation.Automatic)
		{
			tableView.InsertRows (new NSIndexPath[]{ indexPath }, withRowAnimation);
		}

		public static void InsertRow(this UITableView tableView,
			int row,
			int section = 0,
			UITableViewRowAnimation withRowAnimation = UITableViewRowAnimation.Automatic)
		{
			var indexPath = NSIndexPath.FromRowSection(row,section);
			tableView.InsertRows (new NSIndexPath[]{ indexPath }, withRowAnimation);
		}

		public static void DeleteRow(this UITableView tableView,
			int row,
			int section = 0,
			UITableViewRowAnimation withRowAnimation = UITableViewRowAnimation.Automatic)
		{
			var indexPath = NSIndexPath.FromRowSection(row,section);
			tableView.DeleteRows (new NSIndexPath[]{ indexPath }, withRowAnimation);
		}
	}
}

