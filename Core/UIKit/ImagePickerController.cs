namespace Cession.UIKit
{
	using System;
	using System.Drawing;
	using System.IO;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class ImagePickerController:UIImagePickerController
	{
		public Action<UIImage> PostSelectImageAction{get;set;}


		public ImagePickerController ()
		{
			this.ImagePickerControllerDelegate = new PickerDelegate();
		}

		class PickerDelegate:UIImagePickerControllerDelegate
		{
			public override void FinishedPickingImage (UIImagePickerController picker, UIImage image, NSDictionary editingInfo)
			{
				var pp = picker as ImagePickerController;
				if (pp.PostSelectImageAction != null) {
					var fixedImage = image.FixOrientation ();
					pp.PostSelectImageAction.Invoke (fixedImage);
				}
			}
		}
	}
}

