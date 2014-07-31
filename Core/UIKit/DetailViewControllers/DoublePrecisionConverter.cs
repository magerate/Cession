
namespace Cession.UIKit
{
	using System;

	public class DoublePrecisionConverter:ValueConverter
	{
		public static readonly DoublePrecisionConverter Instance = new DoublePrecisionConverter();

		private DoublePrecisionConverter()
		{
		}

		public override object Convert(object value)
		{
			if (value is double)
				return  ((double)value).ToString ("P");
			return value;
		}

		public override Tuple<bool,object> ConvertBack (object value)
		{
			string strValue = value as string;
			if (!string.IsNullOrEmpty (strValue)) {
				double outvalue;
				double.TryParse (strValue.TrimEnd (new char[]{ '%' }), out outvalue);
				return new Tuple<bool, object> (true, outvalue / 100);
			} else
				return new Tuple<bool, object> (false, null);
		}
	}
}

