namespace Cession.UIKit
{
	using System;
	using System.Collections;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class ComboItem:TextFieldItem
	{
		public IList Values{get;set;}
		public Func<object,string> ListFormatter{get;set;}
		public bool DelayChanges{ get; set; }

		public ComboItem ()
		{
			DelayChanges = true;
		}

		protected override void DoActive (NSIndexPath indexPath, UITableViewCell cell, object data)
		{
			base.DoActive (indexPath, cell, data);
			cell.Accessory = UITableViewCellAccessory.DetailButton;
		}

		public override void AccessoryButtonTapped (DetailViewController controller, NSIndexPath indexPath)
		{
			var selector = new ListController();
			selector.DataSource = Values;
			selector.Formatter = ListFormatter;
			selector.SelectedValue = GetValue(controller.DataContext);
			selector.DelayChanges = DelayChanges;
			selector.SelectedAction = (o) => {
				NeedReloadSelf = true;
				SetValue (controller.DataContext, o);
			};
			controller.NavigationController.PushViewController (selector, true);
		}
	}
}

