//using System;
//using System.Drawing;
//
//using MonoTouch.UIKit;
//using MonoTouch.Foundation;
//
//using Pasasoft.Measurement;
//using Pasasoft.Dimensions;
//using Pasasoft.Utilities;
//
//namespace Pasasoft.Fep.UIKit
//{
//	public class LengthItem:LabelItem
//	{
//		private static UIPopoverController popoverController;
//		private static M2PickerView lengthPickerView;
//
//		public Length MinLength{ get; set; }
//		public Length MaxLength{ get; set; }
//
//		public LengthItem ()
//		{
//			Accessory = UITableViewCellAccessory.DisclosureIndicator;
//			Formatter = MeasureHelper.LengthToString;
//		}
//
//		public override void Select (DetailViewController controller, NSIndexPath indexPath)
//		{
//			EditLength (controller,indexPath);
//		}
//
//		private void EditLengthWithPopover(DetailViewController controller, NSIndexPath indexPath)
//		{
//			var data = controller.DataContext;
//			var length = (double)this.GetValue(data);
//			var	editLengthController = new EditLengthViewController();
//
//			editLengthController.PreferredContentSize = new SizeF(300,216);
//			editLengthController.MinLength = MinLength;
//			editLengthController.MaxLength = MaxLength;
//			editLengthController.Handler = (l) => {
//				this.SetValue(data,l.Value);
//			};
//			editLengthController.Length = new Length(length);
//
//
//			var cell = controller.TableView.CellAt(indexPath);
//
//			if (null == popoverController)
//				popoverController = new UIPopoverController (editLengthController);
//			else
//				popoverController.ContentViewController = editLengthController;
//
//			PopoverControllerManager.PresentPopover (popoverController, cell);
//		}
//
//		private void InitializeLengthEditor(RectangleF bounds)
//		{
//			var frame = new RectangleF(0,bounds.Height,bounds.Width,216 + 44);
//
//			lengthPickerView = new M2PickerView(frame);
//
//			lengthPickerView.PickerView.Source = new LengthPickerViewModel();
//		}
//
//		protected void EditLengthWithPickerView(DetailViewController controller,NSIndexPath indexPath)
//		{
//			if (null == lengthPickerView) {
//				InitializeLengthEditor (controller.View.Bounds);
//			}
//
//			var pm = lengthPickerView.PickerView.Source as LengthPickerViewModel;
//			pm.MinLength = MinLength;
//			pm.MaxLength = MaxLength;
//
//			lengthPickerView.PickerView.ReloadAllComponents();
//			var length = new Length((double)GetValue(controller.DataContext));
//
//			pm.SelectLength (lengthPickerView.PickerView, length, true);
//
//			if(lengthPickerView.Superview == null)
//			{
//				controller.View.AddSubview(lengthPickerView);
//				lengthPickerView.TranslateAnimate(0,-lengthPickerView.Bounds.Height,true);
//			}
//
//			lengthPickerView.DoneAction = delegate {
//				lengthPickerView.TranslateAnimate(0,lengthPickerView.Bounds.Height,true,
//					() => lengthPickerView.RemoveFromSuperview());
//
//				this.SetValue(controller.DataContext,pm.GetLength(lengthPickerView.PickerView).Value);
//			};
//		}
//
//		private void EditLength(DetailViewController controller,NSIndexPath indexPath)
//		{
//			if (DeviceHelper.IsPad ())
//				EditLengthWithPopover (controller, indexPath);
//			else
//				EditLengthWithPickerView (controller, indexPath);
//		}
//	}
//}
//
