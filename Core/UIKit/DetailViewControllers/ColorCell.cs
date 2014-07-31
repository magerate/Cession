namespace Cession.UIKit
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class ColorCell:UITableViewCell
	{
		static readonly float margin = 2.0f;

		public UIImageView ColorView{ get; private set; }

		public ColorCell (NSString reuseId):base(UITableViewCellStyle.Default,reuseId)
		{
			ColorView = new UIImageView();
			ContentView.AddSubview(ColorView);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			var size = ContentView.Bounds.Height - 2 * margin;
			ColorView.Frame = new RectangleF(ContentView.Bounds.Width - size - 16 ,
			                                   margin,size,size);

			var frame = TextLabel.Frame;
			frame.Width -= size + 16;
			TextLabel.Frame = frame;
		}
	}
}

