namespace Cession.UIKit
{
	using System;

	using MonoTouch.UIKit;

	public struct ActionSheetItem
	{
		private string title;
		private Action action;

		public ActionSheetItem(string title,Action action)
		{
			this.title = title;
			this.action = action;
		}

		public string Title
		{
			get{ return title; }
			set{ title = value; }
		}

		public Action Action
		{
			get{ return action; }
			set{ action = value; }
		}
	}

	public static class ActionSheetManager
	{
		private static UIActionSheet prevActionSheet;

		public static UIActionSheet CreateActionSheet(ActionSheetItem[] items)
		{
			if (null == items)
				throw new ArgumentNullException ("items can't be null");

			if (items.Length == 0)
				throw new ArgumentException ("items length can't be 0");


			var actionSheet = new UIActionSheet ();
			foreach (var item in items) {
				actionSheet.AddButton (item.Title);
			}


			//iPad will ignore the cancel button automatically
			actionSheet.AddButton ("Cancel");
			actionSheet.CancelButtonIndex = items.Length;

			actionSheet.Clicked += (object sender, UIButtonEventArgs e) => {
				if(e.ButtonIndex < items.Length && e.ButtonIndex >= 0){
					if(items[e.ButtonIndex].Action != null)
						items[e.ButtonIndex].Action.Invoke();
				}
			};

			return actionSheet;
		}

		public static void ShowFromToolbar(UIToolbar toolbar,ActionSheetItem[] items)
		{
			Show (items, a => a.ShowFromToolbar (toolbar));
		}

		public static void ShowFrom(UIBarButtonItem barButtonItem,ActionSheetItem[] items)
		{
			Show (items, a => a.ShowFrom (barButtonItem,true));
		}

		public static void ShowInView(UIView view,ActionSheetItem[] items)
		{
			Show (items, a => a.ShowInView (view));
		}

		public static void ShowFromTabBar(UITabBar tabBar,ActionSheetItem[] items)
		{
			Show (items, a => a.ShowFromTabBar (tabBar));
		}



		public static void Show(ActionSheetItem[] items,Action<UIActionSheet> showAction)
		{
			if (null == showAction)
				throw new ArgumentNullException ("showAction can't be null");
			//dismiss popover if it is visible
			if(DeviceHelper.IsPad())
				PopoverControllerManager.Dismiss (false);

			//dimiss previous action sheet if it is visible
			Dismiss ();
			var actionSheet = CreateActionSheet (items);
			showAction (actionSheet);
			prevActionSheet = actionSheet;
		}

		public static void Dismiss(bool animated = false)
		{
			if (null != prevActionSheet && prevActionSheet.Visible) {
				prevActionSheet.DismissWithClickedButtonIndex(prevActionSheet.ButtonCount - 1,animated);
				prevActionSheet.Dispose ();
				prevActionSheet = null;
			}
		}

	}
}

