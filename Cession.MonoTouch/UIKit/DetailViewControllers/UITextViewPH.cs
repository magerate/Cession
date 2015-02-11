using System;

using CoreGraphics;
using UIKit;
using Foundation;

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

        public UITextViewPH ()
        {
            this.Changed += (sender, e) =>
            {
                this.SetNeedsDisplay ();
            };
        }

        public override void Draw (CGRect rect)
        {
            base.Draw (rect);

            if (!string.IsNullOrEmpty (placeholder) && string.IsNullOrEmpty (this.Text))
                using (var context = UIGraphics.GetCurrentContext ())
                {
                    context.SaveState ();
                    UIColor.LightGray.SetFill ();
                    context.RestoreState ();
                }
        }
    }
}

