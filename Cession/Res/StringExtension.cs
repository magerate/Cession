

namespace Cession.Resources
{
	using System;
	using MonoTouch.Foundation;

	public static class StringExtension
	{
		public static string Localize(this string key)
		{
			return NSBundle.MainBundle.LocalizedString(key,string.Empty);
		}

		public static string Localize(string key,string comment)
		{
			return NSBundle.MainBundle.LocalizedString(key,comment);
		}
	}
}

