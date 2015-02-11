using System;

using CoreGraphics;
using UIKit;
using Foundation;

namespace Cession.UIKit
{
    public class TextViewCell:UITableViewCell
    {
        public UITextViewPH TextView{ get; set; }

        public TextViewCell (NSString reuseId) : base (UITableViewCellStyle.Default, reuseId)
        {
            Initialize ();
        }

        public TextViewCell (IntPtr ptr) : base (ptr)
        {
            Initialize ();
        }

        private void Initialize ()
        {
            TextView = new UITextViewPH ();
            TextView.BackgroundColor = UIColor.Clear;
            ContentView.AddSubview (TextView);
            SelectionStyle = UITableViewCellSelectionStyle.None;

            var font = UIFont.PreferredBody;
            TextView.Font = font;
        }

        public override void LayoutSubviews ()
        {
            base.LayoutSubviews ();
            float margin = 8;

            var frame = ContentView.Bounds;
            frame.X = margin;
            frame.Y = margin;
            frame.Width -= 2 * margin;
            frame.Height -= 2 * margin;
            TextView.Frame = frame;
        }
    }
}

