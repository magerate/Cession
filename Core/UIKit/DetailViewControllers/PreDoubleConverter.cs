//using System;
//
//using Pasasoft.Measurement;
//
//namespace Pasasoft.Fep.UIKit
//{
//	public class PreDoubleConverter:ValueConverter
//	{
//		public override object Convert (object value)
//		{
//			PrecisionDouble preDouble = (PrecisionDouble)value;
//			return preDouble.Value;
//		}
//
//		public override Tuple<bool,object> ConvertBack (object value)
//		{
//			string strValue = value as string;
//			var preDouble = Measure.StringToPrecisionDouble(strValue);
//			if(preDouble.HasValue)
//				return new Tuple<bool, object>(true,preDouble.Value);
//				
//			return new Tuple<bool, object>(false,null);
//		}
//	}
//}
//
