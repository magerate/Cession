namespace Cession.Drawing
{
	using System;
	using System.Drawing;
	using System.Collections.Generic;

	using MonoTouch.CoreGraphics;
	using MonoTouch.UIKit;

	using Cession.Geometries;
	using Cession.Handles;

	public static class HandleDrawing
	{
		private static Dictionary<Type,Action<Handle,CGContext>> drawers = new Dictionary<Type, Action<Handle, CGContext>>();

		private static readonly UIImage SideHandleImage = UIImage.FromBundle("expand-32");

		static HandleDrawing(){
			drawers.Add (typeof(SideHandle), DrawSideHandle);
		}

		private static Action<Handle,CGContext> GetDrawer(Type type){
			Action<Handle,CGContext> drawer;
			drawers.TryGetValue (type, out drawer);
			return drawer;
		}

		public static void Draw(this Handle handle,CGContext context){
			var drawer = GetDrawer (handle.GetType ());
			if (null == drawer)
				throw new ArgumentException ("handle");
			drawer.Invoke (handle, context);
		}

		public static void DrawSideHandle(Handle handle,CGContext context){
			var sideHandle = handle as SideHandle;
			if (null == sideHandle)
				throw new ArgumentException ("handle");

			var side = sideHandle.Side;
			var angle = (float)((side.P2 - side.P1).Angle - Math.PI / 4);
			context.SaveState ();
			context.TranslateCTM ((float)handle.Location.X, 
				(float)handle.Location.Y);
			context.RotateCTM (angle);
			context.TranslateCTM (-SideHandleImage.Size.Width / 2, -SideHandleImage.Size.Height / 2);
			SideHandleImage.Draw (PointF.Empty);
			context.RestoreState ();
		}
	}
}

