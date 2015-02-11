//using System;
//using MonoTouch.UIKit;
//using System.Collections.Generic;
//using System.Text;
//using MonoTouch.Foundation;
//using Pasasoft.Fep.Resources;
//using Pasasoft.Utilities;
//
//namespace Pasasoft.Fep.UIKit
//{
//	public class PredefinedComboItem : ComboItem
//	{
//		public new IList<string> Values{get;set;}
//
//		public string SavePath {
//			get;
//			set;
//		}
//
//		public PredefinedComboItem ()
//		{
//
//		}
//
//		public override void AccessoryButtonTapped (DetailViewController controller, NSIndexPath indexPath)
//		{
//			var selector = new PredefinedController();
//			selector.SavePath = this.SavePath;
//			selector.DataSource = Values;
//			selector.Formatter = ListFormatter;
//			var selValue = GetValue (controller.DataContext);
//			if (null != selValue)
//				selector.SelectedValue = selValue.ToString();
//			selector.DelayChanges = DelayChanges;
//			selector.SelectedAction = (o) => {
//				NeedReloadSelf = true;
//				SetValue (controller.DataContext, o);
//			};
//			controller.NavigationController.PushViewController (selector, true);
//		}
//	}
//}
//
