namespace Cession
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	using Cession.Modeling;
	using Cession.Drawing;

	public class DiagramView:UIView
	{
		private Project project;

		public Project Project
		{
			get{ return project; }
			set{
				if (value != project) {
					project = value;
					SetNeedsDisplay ();
				}
			}
		}

		public DiagramView (RectangleF frame):base(frame)
		{
			this.BackgroundColor = UIColor.White;
		}

		public override void Draw (RectangleF rect)
		{
			if (null == project)
				return;

			var layer = project.Layers.SelectedLayer;
			using (var context = UIGraphics.GetCurrentContext ()) {
				layer.Draw (context);
			}
		}
	}
}

