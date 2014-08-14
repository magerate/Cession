namespace Cession.UIKit
{
	using System;
	using System.Drawing;
	using MonoTouch.UIKit;

	public class ColorPickerLayout:UICollectionViewFlowLayout
	{
		const float margin = 4.0f;
		
		public ColorPickerLayout ()
		{
			float size;
			int countPerRow;
			float viewWidth;

			if (DeviceHelper.IsPad ()) {
				countPerRow = 8;
				viewWidth = 540;
			} else {
				countPerRow = 6;
				viewWidth = UIScreen.MainScreen.Bounds.Width;
			}
			size = (viewWidth - (countPerRow+1) * margin) / countPerRow;

			ItemSize = new SizeF(size,size);
			MinimumLineSpacing = margin;
			MinimumInteritemSpacing = 0;

			SectionInset = new UIEdgeInsets(margin, margin, .0f, margin);
		}
	}
}

