using System;
using CoreGraphics;

using UIKit;
using Foundation;

using Cession.Diagrams;
using Cession.Tools;
using Cession.UIKit;
using Cession.Commands;
using Cession.Resources;

namespace Cession
{
    public partial class DiagramController
    {
        private UIBarButtonItem homeButton;

        private UISegmentedControl toolSegment;
        private UIBarButtonItem segmentButton;

        private UIBarButtonItem layersButton;
        private UIBarButtonItem estimateButton;



        private UIBarButtonItem settingButton;

        private UIBarButtonItem undoButton;
        private UIBarButtonItem redoButton;

        private ToolType segmentToolType = ToolType.Select;

        private void InitializeNavigationItems ()
        {
            homeButton = new UIBarButtonItem ();
            homeButton.Image = UIImage.FromBundle (ImageFiles.Home25);
            homeButton.Clicked += delegate
            {
                this.DismissViewController (true, null);
            };

            var images = new UIImage[] {
                UIImage.FromBundle (ImageFiles.Select25),
                UIImage.FromBundle (ImageFiles.Rectangle25),
                UIImage.FromBundle (ImageFiles.Down25),
            };

            toolSegment = new UISegmentedControl (images);
            toolSegment.SelectedSegment = 0;
            toolSegment.ValueChanged += SegmentedValueChanged;
            segmentButton = new UIBarButtonItem (toolSegment);

            var fixedSpace32 = new UIBarButtonItem (UIBarButtonSystemItem.FixedSpace);
            fixedSpace32.Width = 32;

            layersButton = new UIBarButtonItem ();
            layersButton.Image = UIImage.FromBundle (ImageFiles.Layers25);
            layersButton.Clicked += delegate
            {
//				PopoverControllerManager.ShowPopoverController(GetLayersController(),
//					(p) => p.PresentFromBarButtonItem(layerButtonItem,UIPopoverArrowDirection.Any,true));
            };


            estimateButton = new UIBarButtonItem ();
            estimateButton.Image = UIImage.FromBundle (ImageFiles.Estimate25);
            estimateButton.Clicked += delegate
            {
//				ShowEstimateActions ();
            };


//			View3dItem = new UIBarButtonItem ();
//			View3dItem.Image = UIImage.FromBundle (ImageFiles.Cube25);
//			View3dItem.Clicked += delegate {
//				ShowViewActions ();
//			};
//
//			OptionItem = new UIBarButtonItem ();
//			OptionItem.Image = UIImage.FromBundle (ImageFiles.Option25);
//			OptionItem.Clicked += delegate {
//				ShowViewOptions();
//			};

//			ReportItem = new UIBarButtonItem (UIBarButtonSystemItem.Action);
//			ReportItem.Clicked += delegate {
//				ShowShareAction();
//			};
//
//			HelpItem = new UIBarButtonItem ();
//			HelpItem.Image = UIImage.FromBundle (ImageFiles.Help25);
//			HelpItem.Clicked += delegate {
//				HelpControllerUtil.ShowDiagramHelp(HelpItem,this);
//			};


            settingButton = new UIBarButtonItem ();
            settingButton.Image = UIImage.FromBundle (ImageFiles.Gear25);
            settingButton.Clicked += delegate
            {
//				ShowSetting ();	
            };

            undoButton = new UIBarButtonItem ();
            undoButton.Enabled = false;
            undoButton.Image = UIImage.FromBundle (ImageFiles.Undo25);
            undoButton.Clicked += delegate
            {
                Undo ();
            };

			

            redoButton = new UIBarButtonItem ();
            redoButton.Enabled = false;
            redoButton.Image = UIImage.FromBundle (ImageFiles.Redo25);
            redoButton.Clicked += delegate
            {
                Redo ();
            };


            NavigationItem.LeftBarButtonItems = new UIBarButtonItem[] {
                homeButton,
                segmentButton,
                fixedSpace32,
                layersButton,
                estimateButton,
            };

            NavigationItem.RightBarButtonItems = new UIBarButtonItem[] {
                settingButton,
                redoButton,
                undoButton,
            };
        }


        private int preSelectedSegment = 0;

        public void SegmentedValueChanged (object sender, EventArgs e)
        {
            if (toolSegment.SelectedSegment == 0)
            {
                preSelectedSegment = 0;
                SelectTool (ToolType.Select);
            } else if (toolSegment.SelectedSegment == 1)
            {
                preSelectedSegment = 1;
                SelectTool (segmentToolType);
            } else if (toolSegment.SelectedSegment == 2)
            {
                toolSegment.SelectedSegment = preSelectedSegment;
                PopoverControllerManager.ShowPopoverController (GetToolsController (),
                    p => p.PresentFromBarButtonItem (segmentButton, UIPopoverArrowDirection.Any, true));
            }
        }

        private void SelectTool (DetailMenuItem item)
        {
            PopoverControllerManager.Dismiss (true);

            var toolSegmented = toolSegment;
            var targetToolType = (ToolType)item.Tag;
            segmentToolType = targetToolType;
            if (item.Image != null)
            {
                toolSegmented.SetImage (item.Image, 1);
                toolSegmented.SelectedSegment = preSelectedSegment = 1;
            }
            SelectTool (targetToolType);
        }

        public void SelectTool (ToolType toolType)
        {
            toolManager.SelectTool (toolType);
        }

        private UIViewController GetToolsController ()
        {
            var tc = new ToolsController (SelectTool);
            return new UINavigationController (tc);
        }
    }
}

