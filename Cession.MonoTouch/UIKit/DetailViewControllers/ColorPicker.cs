namespace Cession.UIKit
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;


	public class ColorPicker:UICollectionViewController
	{
		static NSString reuseId = new NSString("colorId");

		public Action<UIColor> SelectedAction;

		private UIColor[] colors = new UIColor[]{


			UIColor.FromRGB(0,0,0),
			UIColor.FromRGB(105,105,105),
			UIColor.FromRGB(128,128,128),
			UIColor.FromRGB(169,169,169),
			UIColor.FromRGB(192,192,192),
			UIColor.FromRGB(211,211,211),
			UIColor.FromRGB(220,220,220),
			UIColor.FromRGB(245,245,245),


			UIColor.FromRGB(255,69,0),
			UIColor.FromRGB(255,99,71),
			UIColor.FromRGB(250,128,114),
			UIColor.FromRGB(255,127,80),
			UIColor.FromRGB(233,150,122),
			UIColor.FromRGB(255,160,122),
			UIColor.FromRGB(255,228,225),
			UIColor.FromRGB(255,255,255),


			UIColor.FromRGB(139,69,19),
			UIColor.FromRGB(160,82,45),
			UIColor.FromRGB(210,105,30),
			UIColor.FromRGB(205,133,63),
			UIColor.FromRGB(244,164,96),
			UIColor.FromRGB(255,218,185),
			UIColor.FromRGB(250,240,230),
			UIColor.FromRGB(255,245,238),


			UIColor.FromRGB(184,134,11),
			UIColor.FromRGB(255,140,0),
			UIColor.FromRGB(255,165,0),
			UIColor.FromRGB(218,165,32),
			UIColor.FromRGB(210,180,140),
			UIColor.FromRGB(222,184,135),
			UIColor.FromRGB(255,228,196),
			UIColor.FromRGB(250,235,215),


			UIColor.FromRGB(245,222,179),
			UIColor.FromRGB(255,222,173),
			UIColor.FromRGB(255,228,181),
			UIColor.FromRGB(255,235,205),
			UIColor.FromRGB(255,239,213),
			UIColor.FromRGB(253,245,230),
			UIColor.FromRGB(255,248,220),
			UIColor.FromRGB(255,250,240),


			UIColor.FromRGB(255,215,0),
			UIColor.FromRGB(255,255,0),
			UIColor.FromRGB(240,230,140),
			UIColor.FromRGB(238,232,170),
			UIColor.FromRGB(255,250,205),
			UIColor.FromRGB(250,250,210),
			UIColor.FromRGB(245,245,220),
			UIColor.FromRGB(255,255,240),


			UIColor.FromRGB(0,100,0),
			UIColor.FromRGB(0,128,0),
			UIColor.FromRGB(34,139,34),
			UIColor.FromRGB(85,107,47),
			UIColor.FromRGB(128,128,0),
			UIColor.FromRGB(107,142,35),
			UIColor.FromRGB(189,183,107),
			UIColor.FromRGB(143,188,143),


			UIColor.FromRGB(50,205,50),
			UIColor.FromRGB(0,255,0),
			UIColor.FromRGB(124,252,0),
			UIColor.FromRGB(127,255,0),
			UIColor.FromRGB(173,255,47),
			UIColor.FromRGB(154,205,50),
			UIColor.FromRGB(152,251,152),
			UIColor.FromRGB(144,238,144),

			
			UIColor.FromRGB(0,255,127),
			UIColor.FromRGB(0,250,154),
			UIColor.FromRGB(46,139,87),
			UIColor.FromRGB(60,179,113),
			UIColor.FromRGB(102,205,170),
			UIColor.FromRGB(255,255,224),
			UIColor.FromRGB(240,255,240),
			UIColor.FromRGB(245,255,250),

			
			UIColor.FromRGB(47,79,79),
			UIColor.FromRGB(0,128,128),
			UIColor.FromRGB(0,139,139),
			UIColor.FromRGB(95,158,160),
			UIColor.FromRGB(32,178,170),
			UIColor.FromRGB(72,209,204),
			UIColor.FromRGB(64,224,208),
			UIColor.FromRGB(127,255,212),


			UIColor.FromRGB(0,255,255),
			UIColor.FromRGB(0,206,209),
			UIColor.FromRGB(0,191,255),
			UIColor.FromRGB(30,144,255),
			UIColor.FromRGB(173,216,230),	
			UIColor.FromRGB(176,224,230),		
			UIColor.FromRGB(175,238,238),
			UIColor.FromRGB(224,255,255),


			UIColor.FromRGB(70,130,180),
			UIColor.FromRGB(65,105,225),
			UIColor.FromRGB(100,149,237),
			UIColor.FromRGB(112,128,144),
			UIColor.FromRGB(119,136,153),
			UIColor.FromRGB(135,206,235),
			UIColor.FromRGB(135,206,250),
			UIColor.FromRGB(176,196,222),


			UIColor.FromRGB(0,0,128),
			UIColor.FromRGB(0,0,139),
			UIColor.FromRGB(25,25,112),
			UIColor.FromRGB(0,0,205),
			UIColor.FromRGB(0,0,255),
			UIColor.FromRGB(230,230,250),
			UIColor.FromRGB(240,248,255),
			UIColor.FromRGB(248,248,255),


			UIColor.FromRGB(72,61,139),
			UIColor.FromRGB(106,90,205),
			UIColor.FromRGB(123,104,238),
			UIColor.FromRGB(147,112,219),
			UIColor.FromRGB(216,191,216),
			UIColor.FromRGB(255,240,245),
			UIColor.FromRGB(240,255,255),
			UIColor.FromRGB(255,250,250),		


			UIColor.FromRGB(75,0,130),
			UIColor.FromRGB(128,0,128),			
			UIColor.FromRGB(139,0,139),
			UIColor.FromRGB(199,21,133),
			UIColor.FromRGB(255,20,147),
			UIColor.FromRGB(255,0,255),
			UIColor.FromRGB(255,105,180),
			UIColor.FromRGB(219,112,147),

			
			UIColor.FromRGB(148,0,211),
			UIColor.FromRGB(138,43,226),
			UIColor.FromRGB(153,50,204),
			UIColor.FromRGB(186,85,211),
			UIColor.FromRGB(218,112,214),
			UIColor.FromRGB(238,130,238),
			UIColor.FromRGB(221,160,221),
			UIColor.FromRGB(255,192,203),

			
			UIColor.FromRGB(255,0,0),
			UIColor.FromRGB(139,0,0),
			UIColor.FromRGB(128,0,0),
			UIColor.FromRGB(165,42,42),
			UIColor.FromRGB(178,34,34),
			UIColor.FromRGB(220,20,60),
			UIColor.FromRGB(205,92,92),
			UIColor.FromRGB(188,143,143),

			
			UIColor.FromRGB(240,128,128),
			UIColor.FromRGB(255,182,193),
		};

		public ColorPicker ():base(new ColorPickerLayout())
		{
			CollectionView.RegisterClassForCell (typeof(UICollectionViewCell), reuseId);
			CollectionView.BackgroundColor = UIColor.White;
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override int GetItemsCount (UICollectionView collectionView, int section)
		{
			return colors.Length;
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var color = colors[indexPath.Row];
			var cell = collectionView.DequeueReusableCell(reuseId,indexPath) as UICollectionViewCell;
			cell.ContentView.BackgroundColor = color;
			cell.Layer.BorderWidth = 1.0f;
			cell.Layer.BorderColor = UIColor.Gray.CGColor;
			return cell;
		}

		public override void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var color = colors[indexPath.Row];
			if(SelectedAction != null)
				SelectedAction.Invoke(color);
		}
	}
}

