namespace Cession.UIKit
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	using Cession.Drawing;

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

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);

			if (!string.IsNullOrEmpty (placeholder) && string.IsNullOrEmpty (this.Text))
				using (var context = UIGraphics.GetCurrentContext ()) {
					context.SaveState ();
					UIColor.LightGray.SetFill ();
					DrawHelper.DrawString (placeholder, UIFont.SystemFontOfSize(18), new PointF (5, 5));
					context.RestoreState ();
				}	
		}
	}
}

