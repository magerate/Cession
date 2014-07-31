namespace Cession.UIKit
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.CoreGraphics;
	using MonoTouch.Foundation;

	public static class ImageHelper
	{
		public static UIImage GetTemplateImage(string path)
		{
			var image = UIImage.FromBundle (path);
			if (null == image)
				return null;

			return image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysTemplate);
		}

		public static UIImage Scale(this UIImage image,float maxSize)
		{
			if(null == image)
				throw new ArgumentNullException();

			var ratioX = GetSizeRatio (image.Size.Width,maxSize);
			var ratioY = GetSizeRatio (image.Size.Height,maxSize);

			var ratio = Math.Min (ratioX, ratioY);

			if (ratio == 1) 
				return image;

			var size = new SizeF (image.Size.Width * ratio, image.Size.Height * ratio);
			return image.Scale(size);
		}

		private static float GetSizeRatio(float size,float maxSize)
		{
			return Math.Min (size, maxSize) / size;
		}

		public static UIImage Crop(this UIImage image,RectangleF bounds)
		{
			if (bounds.Width < 1 || bounds.Height < 1)
				return null;

			var tb = bounds;

			var cgImage = image.CGImage.WithImageInRect (tb);
			if (cgImage == null)
				return null;

			var uiImage = new UIImage (cgImage);
			cgImage.Dispose ();

			return uiImage;
		}

		public static void SaveAsPng(this UIImage image,string path)
		{
			var data = image.AsPNG();
			NSError error;
			data.Save(path,false,out error);
		}

		public static UIImage FixOrientation(this UIImage image)
		{
			if (image.Orientation == UIImageOrientation.Up)
				return image;

			CGAffineTransform transform = CGAffineTransform.MakeIdentity ();

			switch (image.Orientation) {
			case UIImageOrientation.Down:
			case UIImageOrientation.DownMirrored:
				transform.Rotate ((float)Math.PI);
				transform.Translate (image.Size.Width, image.Size.Height);
				break;

			case UIImageOrientation.Left:
			case UIImageOrientation.LeftMirrored:
				transform.Rotate ((float)Math.PI / 2);
				transform.Translate (image.Size.Width, 0);
				break;

			case UIImageOrientation.Right:
			case UIImageOrientation.RightMirrored:
				transform.Rotate ((float)-Math.PI / 2);
				transform.Translate (0, image.Size.Height);
				break;
			default:
				break;
			}

			switch (image.Orientation) {
			case UIImageOrientation.UpMirrored:
			case UIImageOrientation.DownMirrored:
				transform.Scale(-1, 1);
				transform.Translate(image.Size.Width, 0);
				break;

			case UIImageOrientation.LeftMirrored:
			case UIImageOrientation.RightMirrored:
				transform.Scale(-1, 1);
				transform.Translate(image.Size.Height, 0);
				break;
			default:
				break;
			}


			var context = new CGBitmapContext(null,
				(int)image.Size.Width,
				(int)image.Size.Height,
				image.CGImage.BitsPerComponent,0,
				image.CGImage.ColorSpace,
				image.CGImage.BitmapInfo);
			context.ConcatCTM (transform);

			switch (image.Orientation) {
			case UIImageOrientation.Left:
			case UIImageOrientation.LeftMirrored:
			case UIImageOrientation.Right:
			case UIImageOrientation.RightMirrored:
				// Grr...
				context.DrawImage(new RectangleF(0,0,image.Size.Height,image.Size.Width),image.CGImage);
				break;

			default:
				context.DrawImage (new RectangleF (PointF.Empty, image.Size), image.CGImage);
				break;
			}

// And now we just create a new UIImage from the drawing context
			var cgImg = context.ToImage ();
			var uiImage = new UIImage (cgImg);
			context.Dispose ();
			cgImg.Dispose ();
			return uiImage;
		}

		public static UIImage PdfPageToImage (CGPDFPage page)
		{ 		
			RectangleF pageRect = page.GetBoxRect (CGPDFBox.Media);
			UIGraphics.BeginImageContext (pageRect.Size); 
			var context = UIGraphics.GetCurrentContext (); 
			context.SetRGBFillColor (1.0f, 1.0f, 1.0f, 1.0f);
			context.FillRect (pageRect);
			context.TranslateCTM (0,pageRect.Size.Height); 
			context.ScaleCTM (1f, -1f);
//			context.ConcatCTM (page.GetDrawingTransform (CGPDFBox.Media, pageRect, 0, true));
			context.DrawPDFPage (page);
			var scaledImage = UIGraphics.GetImageFromCurrentImageContext (); 
			UIGraphics.EndImageContext (); 
			return scaledImage;          
		}
	}
}

