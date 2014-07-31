namespace Cession.UIKit
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	using Cession.Tools;
	using Cession.Resources;


	public class ToolsController:DetailViewController
	{
		private Action<DetailMenuItem> toolSelector;

		public ToolsController (Action<DetailMenuItem> toolSelector):base(UITableViewStyle.Plain)
		{
			this.toolSelector = toolSelector;
			Title = "Tools";
			PreferredContentSize = new SizeF (320, 280);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			InitializeItems ();
		}

		private void InitializeItems()
		{
			var section = new DetailSection ();
			DetailSections.Add (section);

			var rectItem = new DetailMenuItem ();
			rectItem.Title = "Add Rect Room";
			rectItem.Image = ImageHelper.GetTemplateImage (ImageFiles.Rectangle25);
			rectItem.Tag = ToolType.AddRectangularRoom;
			rectItem.Action = toolSelector;
			section.Items.Add(rectItem);

			var polygonItem = new DetailMenuItem ();
			polygonItem.Title = "Add Polygonal Room";
			polygonItem.Image = ImageHelper.GetTemplateImage (ImageFiles.Polygon25);
			polygonItem.Tag = ToolType.AddPolygonalRoom;
			polygonItem.Action = toolSelector;
			section.Items.Add(polygonItem);


		}



	}
}

