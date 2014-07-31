namespace Cession.UIKit
{
	using System;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public static class UIFontExtension
	{
		public static bool IsBold(this UIFont font)
		{
			return GetFontAttribute (font, UIFontDescriptorSymbolicTraits.Bold);
		}

		private static bool GetFontAttribute(this UIFont font,UIFontDescriptorSymbolicTraits symbolicTraits)
		{
			var fontDescriptor = font.FontDescriptor;
			var fontDescriptorSymbolicTraits = fontDescriptor.SymbolicTraits;
			return  (fontDescriptorSymbolicTraits & symbolicTraits) != UIFontDescriptorSymbolicTraits.ClassUnknown;
		}

		public static bool IsItalic(this UIFont font)
		{
			return GetFontAttribute (font, UIFontDescriptorSymbolicTraits.Italic);
		}
	}
}

