//using System;
//
//using Pasasoft.Measurement;
//
//namespace Pasasoft.Fep.UIKit
//{
//	public class PrecisionLengthConverter:ValueConverter
//	{
//		public static readonly PrecisionLengthConverter Instance = new PrecisionLengthConverter();
//
//		private PrecisionLengthConverter()
//		{
//		}
//
//		public override Tuple<bool,object> ConvertBack (object value)
//		{
//			string strValue = value as string;
//			var preDouble = Measure.StringToPrecisionLength(strValue);
//			if(preDouble.HasValue)
//				return new Tuple<bool, object>(true,preDouble.Value);
//
//			return new Tuple<bool, object>(false,null);
//		}
//	}
//}
//
