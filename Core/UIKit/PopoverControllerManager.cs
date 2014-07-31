namespace Cession.UIKit
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;

	public static class PopoverControllerManager
	{
		private static UIPopoverController popoverController;

		public static void ShowFromRect(UIViewController viewController,
											RectangleF rect,
											UIView view,
											UIPopoverArrowDirection direction = UIPopoverArrowDirection.Any,
											bool animated = true)
		{
			ShowPopoverController (viewController, 
				p => p.PresentFromRect (rect, view, direction, animated));
		}

		public static void ShowFromBarButton(UIViewController viewController,
											UIBarButtonItem item,
											UIPopoverArrowDirection direction = UIPopoverArrowDirection.Any,
											bool animated = true)
		{
			ShowPopoverController (viewController, 
				p => p.PresentFromBarButtonItem (item, direction, animated));
		}

		public static void ShowPopoverController(UIViewController viewController,
		                           Action<UIPopoverController> presentAction)
		{
			if (null == viewController || null == presentAction)
				throw new ArgumentNullException ();

			//dismiss action sheet if it's visible before present popover
			ActionSheetManager.Dismiss ();

			if (null == popoverController)
			{
				popoverController = new UIPopoverController (viewController);
				presentAction.Invoke (popoverController);
			}
			else
			{
				if (popoverController.ContentViewController != viewController) {
					if (popoverController.PopoverVisible)
						popoverController.Dismiss (false);
					popoverController.ContentViewController = viewController;
					if (viewController.PreferredContentSize != SizeF.Empty)
						popoverController.PopoverContentSize = viewController.PreferredContentSize;
					else
						popoverController.PopoverContentSize = new SizeF (320, 480);
					presentAction.Invoke (popoverController);
				} else {
					if(!popoverController.PopoverVisible)
						presentAction.Invoke (popoverController);
				}
			}
		}

		public static void Dismiss(bool animate)
		{
			if (null != popoverController && popoverController.PopoverVisible)
				popoverController.Dismiss (animate);
		}

		public static void PresentPopover(UIPopoverController popover,UITableViewCell cell)
		{
			popover.PresentFromRect(new RectangleF(cell.Center,new SizeF(1,1)),
				cell.Superview,
				UIPopoverArrowDirection.Any,true);
		}
	}
}

