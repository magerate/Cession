namespace Cession.UIKit
{
	using System;
	using System.Drawing;
	using System.Collections;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class ListItem:LabelItem
	{
		public IList Values{get;set;}
		public bool CanEdit{ get; set; }
		 
		public ListItem (IList values):this()
		{
			if (null == values)
				throw new ArgumentNullException ("values can't be null");
			this.Values = values;
		}

		public ListItem()
		{
			Accessory = UITableViewCellAccessory.DisclosureIndicator;
			CanEdit = true;
		}

		public override void Select (DetailViewController controller, NSIndexPath indexPath)
		{
			if (!CanEdit)
				return;

			var selector = new ListController();
			selector.DataSource = Values;
			selector.Formatter = Formatter;
			selector.SelectedValue = GetValue(controller.DataContext);
			selector.DelayChanges = true;
			selector.SelectedAction = (o) => {
				SetValue (controller.DataContext, o);
			};
			controller.NavigationController.PushViewController (selector, true);
		}
	}
}

