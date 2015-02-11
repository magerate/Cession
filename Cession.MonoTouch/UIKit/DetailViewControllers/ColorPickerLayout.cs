using System;
using CoreGraphics;
using UIKit;

namespace Cession.UIKit
{


    public class ColorPickerLayout:UICollectionViewFlowLayout
    {
        const float margin = 4.0f;

        public ColorPickerLayout ()
        {
            nfloat size;
            int countPerRow;
            nfloat viewWidth;

            if (DeviceHelper.IsPad ())
            {
                countPerRow = 8;
                viewWidth = 540;
            } else
            {
                countPerRow = 6;
                viewWidth = UIScreen.MainScreen.Bounds.Width;
            }
            size = (viewWidth - (countPerRow + 1) * margin) / countPerRow;

            ItemSize = new CGSize (size, size);
            MinimumLineSpacing = margin;
            MinimumInteritemSpacing = 0;

            SectionInset = new UIEdgeInsets (margin, margin, .0f, margin);
        }
    }
}

