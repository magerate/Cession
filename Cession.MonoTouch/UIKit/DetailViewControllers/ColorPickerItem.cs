//using System;
//
//using MonoTouch.UIKit;
//using MonoTouch.Foundation;
//
//using Pasasoft.Utilities;
//using AdvancedColorPicker;
//
//namespace Pasasoft.Fep.UIKit
//{
//	public class ColorPickerItem:ColorItem
//	{
//		public override void Select (DetailViewController controller, NSIndexPath indexPath)
//		{
//			var color = ColorHelper.UIntToUIColor ((uint)GetValue (controller.DataContext));
//			var colorPicker = new ColorPickerViewController ();
//			colorPicker.SelectedColor = color;
//			colorPicker.ColorPicked += () => {
//				SetValue(controller.DataContext,colorPicker.SelectedColor);
//			};
//			colorPicker.EdgesForExtendedLayout = UIRectEdge.None;
////			var nav = new UINavigationController (colorPicker);
////			nav.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
//			controller.NavigationController.PushViewController (colorPicker, true);
//		}
//	}
//}
//
