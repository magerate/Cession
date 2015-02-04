using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Cession.Dimensions;

namespace Cession
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			int x = int.MaxValue;
			int y = x;
			y += 1;
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
