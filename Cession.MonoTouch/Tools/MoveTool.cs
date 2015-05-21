using System;
using System.Collections.Generic;

using CoreGraphics;

using Cession.Diagrams;
using Cession.Drawing;
using Cession.Geometries;
using Cession.Commands;

namespace Cession.Tools
{
    public class MoveTool:DragDropTool
    {
        private IEnumerable<Shape> _shapes;

        public MoveTool (ToolManager toolManager) : base (toolManager)
        {
        }

        public override void Enter (Tool parentTool, params object[] args)
        {
            base.Enter (parentTool, args);
            if (args.Length < 1)
                throw new ArgumentException ();

            _shapes = args [0] as IEnumerable<Shape>;
            if (_shapes == null)
                throw new ArgumentException ();
        }

        protected override void Commit ()
        {
            var vector = EndPoint.Value - StartPoint.Value;
            var command = Command.Create (_shapes, vector, -vector, OffsetShapes);
            CommandManager.Execute (command);
        }

        private void OffsetShapes (IEnumerable<Shape> shapes, Vector offset)
        {
            foreach (var shape in shapes)
            {
                shape.Offset (offset);
            }
        }

        protected override void DoDrawDragDrop (DrawingContext drawingContext)
        {
            Vector vector = EndPoint.Value - StartPoint.Value;
//            vector = CurrentLayer.ConvertToViewVector (vector);

            CGContext context = drawingContext.CGContext;
            context.SaveState ();
            context.SetAlpha (.5f);
            context.TranslateCTM ((nfloat)vector.X, (nfloat)vector.Y);
            foreach (var shape in _shapes)
            {
                shape.Draw (drawingContext);
            }
            context.RestoreState ();
        }
    }
}

