namespace Cession.UIKit
{
	using System;

	using UIKit;

	public class ColorConverter:ValueConverter
	{
		public override object Convert (object value)
		{
			return null;
//			return ColorHelper.UIntToUIColor ((uint)value);
		}

		public override Tuple<bool, object> ConvertBack (object value)
		{
			return null;
//			if (null == value)
//				return new Tuple<bool, object> (false, null);
//
//			return new Tuple<bool,object> (true, ColorHelper.ColorToUInt (value as UIColor));
		}
	}
}

