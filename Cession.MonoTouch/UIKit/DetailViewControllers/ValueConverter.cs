namespace Cession.UIKit
{
	using System;

	public abstract class ValueConverter
	{
		public virtual object Convert(object value)
		{
			return value;
		}

		public abstract Tuple<bool,object> ConvertBack(object value);
	}
}

