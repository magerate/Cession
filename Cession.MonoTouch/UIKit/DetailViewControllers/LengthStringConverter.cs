//namespace Cession.UIKit
//{
//	using System;
//
//	public class LengthStringConverter:ValueConverter
//	{
//		public LengthStringConverter ()
//		{
//		}
//
//		public override object Convert (object value)
//		{
//			return MeasureHelper.LengthToString (value);
//		}
//
//		public override Tuple<bool, object> ConvertBack (object value)
//		{
//			var length = Measure.StringToPrecisionLength(value.ToString());
//			if(length.HasValue)
//				return new Tuple<bool, object>(true,length.Value.Value);
//
//			return new Tuple<bool, object>(false,null);
//		}
//	}
//}
//
