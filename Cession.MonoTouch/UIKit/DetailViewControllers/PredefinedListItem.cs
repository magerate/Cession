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
//	public class PredefinedListItem : ListItem
//	{
//		public new IList<string> Values{get;set;}
//
//		public string SavePath {
//			get;
//			set;
//		}
//
//		public PredefinedListItem ()
//		{
//
//		}
//
//		public override void Select (DetailViewController controller, NSIndexPath indexPath)
//		{
//			if (!CanEdit)
//				return;
//
//			var selector = new PredefinedController();
//			selector.SavePath = this.SavePath;
//			selector.DataSource = Values;
//			selector.Formatter = Formatter;
//			var selValue = GetValue (controller.DataContext);
//			if (null != selValue)
//				selector.SelectedValue = selValue.ToString ();
//			selector.DelayChanges = true;
//			selector.SelectedAction = (o) => {
//				SetValue (controller.DataContext, o);
//			};
//			controller.NavigationController.PushViewController (selector, true);
//		}
//	}
//}
//
