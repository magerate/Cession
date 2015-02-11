using System;
using CoreGraphics;
using System.IO;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
    public class ImagePickerController:UIImagePickerController
    {
        public Action<UIImage> PostSelectImageAction{ get; set; }


        public ImagePickerController ()
        {
            this.ImagePickerControllerDelegate = new PickerDelegate ();
        }

        class PickerDelegate:UIImagePickerControllerDelegate
        {
            public override void FinishedPickingImage (UIImagePickerController picker, UIImage image, NSDictionary editingInfo)
            {
                var pp = picker as ImagePickerController;
                if (pp.PostSelectImageAction != null)
                {
                    var fixedImage = image.FixOrientation ();
                    pp.PostSelectImageAction.Invoke (fixedImage);
                }
            }
        }
    }
}

