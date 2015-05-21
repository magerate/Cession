using System;

using Foundation;
using UIKit;

using Cession.Diagrams;

namespace Cession
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        UINavigationController viewController;
        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            window = new UIWindow (UIScreen.MainScreen.Bounds);
            viewController = new UINavigationController (new DiagramListController ());
            window.RootViewController = viewController;
            window.MakeKeyAndVisible ();
            InitializeDiagramComponents ();
            return true;
        }

        private void InitializeDiagramComponents ()
        {
            CustomShape.RegisterHitTestProvider (typeof(Label), new LabelHitTestProvider ());
        }
    }
}

