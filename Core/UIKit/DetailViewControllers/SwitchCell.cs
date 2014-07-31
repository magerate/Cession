namespace Cession.UIKit
{
	using System;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class SwitchCell:UITableViewCell
	{
		public UISwitch Switch { get; private set; }
		public Action<bool> SwitchValueChangedAction{ get; set; }


		public SwitchCell (NSString reuseId):base(UITableViewCellStyle.Default,reuseId)
		{
			Switch = new UISwitch ();
			Switch.ValueChanged += (sender, e) => {
				if (null != SwitchValueChangedAction)
					SwitchValueChangedAction (Switch.On);
			};

			AccessoryView = Switch;
		}
	}
}

