namespace Cession.Tools
{
	using System;
	using System.Collections.Generic;

	using MonoTouch.CoreGraphics;

	using Cession.Modeling;
	using Cession.Drawing;
	using Cession.Geometries;
	using Cession.Commands;

	public class MoveSideTool:DragDropTool
	{
		private SideHandle handle;

		public MoveSideTool (ToolManager toolManager):base(toolManager)
		{
		}

		public override void Enter (Tool parentTool, params object[] args)
		{
			base.Enter (parentTool, args);

			if (null == args)
				throw new ArgumentNullException ();

			if (args.Length != 1)
				throw new ArgumentException ();

			handle = args [0] as SideHandle;

			if (null == handle)
				throw new ArgumentException ();
		}

		protected override void DoDraw (CGContext context)
		{
			var segment = handle.Move (endPoint.Value);
			var prevSegment = handle.PreviousSide;
			var nextSegment = handle.NextSide;

			context.StrokeLine (prevSegment.P1, segment.P1);
			context.StrokeLine (segment.P1, segment.P2);
			context.StrokeLine (segment.P2, nextSegment.P2);
		}

		protected override void Commit ()
		{
			var diagram = handle.Parent as RectangleDiagram;
			var rect = diagram.MoveSide(handle.Index,
				endPoint.Value.X - startPoint.Value.X,
				endPoint.Value.Y - startPoint.Value.Y);

			CommandManager.ExecuteSetProperty (diagram, rect, "Rect");
		}
	}
}

