namespace Cession.Utilities
{
	using System;
	using System.ComponentModel;

	public static class EnumExtension
	{
		public static string GetDescription<T>(this T enumValue) where T : struct
		{
			var attribute = enumValue.GetAttribute (typeof(DescriptionAttribute)) as DescriptionAttribute;
			if (null == attribute)
				return string.Empty;

			return attribute.Description;
		}

		public static Attribute GetAttribute<T>(this T enumValue,Type type) where T : struct 
		{
			if(!type.IsSubclassOf(typeof(Attribute)))
				throw new ArgumentException("type must be an Attribute type");

			var enumType = typeof(T);
			if (!enumType.IsEnum) 
				throw new ArgumentException("T must be an enumerated type");

			var fieldMember = enumType.GetMember(enumValue.ToString())[0];
			return Attribute.GetCustomAttribute(fieldMember,type);
		}
	}
}

