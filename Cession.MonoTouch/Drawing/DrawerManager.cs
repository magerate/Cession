namespace Cession.Drawing
{
	using System;
	using System.Collections.Generic;

	using MonoTouch.CoreGraphics;

	using Cession.Modeling;

	public static class DrawerManager
	{
		private static Dictionary<Type,DiagramDrawer> drawers=new Dictionary<Type, DiagramDrawer>();

		static DrawerManager ()
		{
			RegisterDrawer(typeof(Room),new RoomDrawer());
			RegisterDrawer(typeof(Layer),new LayerDrawer());
		}

		public static void RegisterDrawer(Type type,DiagramDrawer drawer)
		{
			if(null == type)
				throw new ArgumentNullException();

			if(null == drawer)
				throw new ArgumentNullException();

			drawers[type] = drawer;
		}

		public static DiagramDrawer GetDrawer(Diagram diagramElement)
		{
			var type = diagramElement.GetType ();
			DiagramDrawer drawer;
			drawers.TryGetValue(type,out drawer);
			return drawer;
		}

		public static void Draw(this Diagram diagramElement,CGContext context)
		{
			var drawer = GetDrawer (diagramElement);
			if (null == drawer)
				throw new InvalidOperationException ();

			drawer.Draw (context, diagramElement);
		}

		public static void DrawSelected(this Diagram diagramElement,CGContext context)
		{
			var drawer = GetDrawer (diagramElement);
			if (null == drawer)
				throw new InvalidOperationException ();

			drawer.DrawSelected (context, diagramElement);
		}
	}
}

