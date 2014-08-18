namespace Cession.Tools
{
	using System;
	using System.Collections.Generic;

	using MonoTouch.CoreGraphics;

	using Cession.Modeling;
	using Cession.Drawing;
	using Cession.Geometries;
	using Cession.Commands;

	public class MoveTool:DragDropTool
	{
		private IEnumerable<Diagram> shapes;

		public MoveTool (ToolManager toolManager):base(toolManager)
		{
		}

		public override void Enter (Tool parentTool, params object[] args)
		{
			base.Enter (parentTool, args);
			if (args.Length < 1)
				throw new ArgumentException ();

			shapes = args [0] as IEnumerable<Diagram>;
			if(shapes == null)
				throw new ArgumentException ();
		}

		protected override void Commit ()
		{
			var vector = endPoint.Value - startPoint.Value;
			var command = Command.Create (shapes, vector, -vector,OffsetShapes);
			CommandManager.Execute (command);
		}

		private void OffsetShapes(IEnumerable<Diagram> shapes,Vector offset)
		{
			foreach (var shape in shapes) {
				shape.Move (offset);
			}
		}

		protected override void DoDraw (CGContext context)
		{
			var vector = DrawHelper.Transform.Transform(endPoint.Value - startPoint.Value);
			context.SaveState ();
			context.SetAlpha (.5f);
			context.TranslateCTM ((float)vector.X, (float)vector.Y);
			foreach (var item in shapes) {
				item.Draw (context);
			}
			context.RestoreState ();
		}
	}
}

