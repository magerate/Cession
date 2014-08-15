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
			var segment = handle.MoveSide (endPoint.Value);
			var prevSegment = handle.PreviousSide;
			var nextSegment = handle.NextSide;

			if(prevSegment.HasValue)
				context.StrokeLine (prevSegment.Value.P1, segment.P1);
			context.StrokeLine (segment.P1, segment.P2);
			if(nextSegment.HasValue)
				context.StrokeLine (segment.P2, nextSegment.Value.P2);
		}

		protected override void Commit ()
		{
			if (handle.Parent is RectangleDiagram) {
				var rectDiagram = handle.Parent as RectangleDiagram;
				var rect = rectDiagram.MoveSide (handle.Index,
					           endPoint.Value.X - startPoint.Value.X,
					           endPoint.Value.Y - startPoint.Value.Y);

				CommandManager.ExecuteSetProperty (rectDiagram, rect, "Rect");
			} else if (handle.Parent is PathDiagram) {
				var segment = handle.MoveSide (endPoint.Value);
				var pathDiagram = handle.Parent as PathDiagram;

				var nextIndex = (handle.Index + 1 + pathDiagram.SideCount) % pathDiagram.SideCount;
				CommandManager.ExecuteListReplace (pathDiagram.Points, handle.Index, segment.P1,false);
				CommandManager.ExecuteListReplace (pathDiagram.Points, nextIndex, segment.P2);
			}
		}
	}
}

