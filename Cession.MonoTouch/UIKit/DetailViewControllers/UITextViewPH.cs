using System;

using CoreGraphics;
using UIKit;
using Foundation;

using Cession.Drawing;

namespace Cession.UIKit
{


	public class UITextViewPH : UITextView
	{
		private string placeholder = string.Empty;
		public string Placeholder 
		{
			get{ return placeholder; }
			set{ placeholder = value; }
		}

		public UITextViewPH()
		{
			this.Changed += (sender, e) => {
				this.SetNeedsDisplay();
			};
		}

		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			if (!string.IsNullOrEmpty (placeholder) && string.IsNullOrEmpty (this.Text))
				using (var context = UIGraphics.GetCurrentContext ()) {
					context.SaveState ();
					UIColor.LightGray.SetFill ();
//					DrawUtil.DrawString (placeholder, UIFont.SystemFontOfSize(18), new PointF (5, 5));
					context.RestoreState ();
				}	
		}
	}
}

