using System;
using CoreGraphics;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
	public class TextureCell:UITableViewCell
	{
		static readonly float margin = 2.0f;

		public TextureCell (NSString reuseId):base(UITableViewCellStyle.Default,reuseId)
		{

		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews();

			var imageViewSize = ContentView.Bounds.Height - 2 * margin;
			var frame = TextLabel.Frame;
			frame.X = ImageView.Frame.X;
			TextLabel.Frame = frame;
			
			ImageView.Frame = new CGRect (ContentView.Bounds.Width - imageViewSize - 16 , margin, 
			                                  imageViewSize, imageViewSize);

		}
	}
}

