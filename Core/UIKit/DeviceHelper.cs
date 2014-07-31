namespace Cession.UIKit
{
	using MonoTouch.UIKit;

	public static class DeviceHelper
	{
		public static bool IsRetina()
		{
			return UIScreen.MainScreen.Scale > 1.0f;
		}

		public static bool IsPad()
		{
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
		}

		public static string GetName()
		{
			return UIDevice.CurrentDevice.Name;
		}

		public static string GetId()
		{
			return UIDevice.CurrentDevice.IdentifierForVendor.AsString ();
		}

		public static string GetPlatformInfo()
		{
			return string.Format ("{0} {1}", UIDevice.CurrentDevice.SystemName,
			                     UIDevice.CurrentDevice.SystemVersion);
		}
	}
}

