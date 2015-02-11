using System;
using System.Linq;
using System.Collections.Generic;

namespace Cession.Utilities
{
	public static class StringExtension
	{
		public static string CreateDuplicateName(this string prefix,IEnumerable<string> strings)
		{
			if (null == strings)
				throw new ArgumentNullException ();

			if (null == prefix)
				throw new ArgumentNullException ();

			var values = strings.
				Where (s => s.Contains (prefix)).
				Select(s => GetIntValue(s,prefix));

			int max = 0;
			if(values.Count() > 0)
				max = values.Max ();

			var result = prefix + (max + 1).ToString ();
			return result;
		}

		private static int GetIntValue(string str,string prefix){
			return SafeParse (str.Substring (prefix.Length));
		}

		private static int SafeParse(string str)
		{
			int num = 0;
			int.TryParse (str, out num);
			return num;
		}
	}
}

