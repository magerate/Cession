using Cession.Geometries;
using Cession.Drawing;
using Cession.Diagrams;
using G = Cession.Geometries;
using D = Cession.Diagrams;

namespace Cession.Tools
{
    public class AddCircleTool:DragDropTool
    {
        public AddCircleTool (ToolManager toolManager) : base (toolManager)
        {
        }

        protected override void Commit ()
        {
            Point center = G.Segment.GetCenter (StartPoint.Value, EndPoint.Value);
            double radius = Point.DistanceBetween (StartPoint.Value, EndPoint.Value) / 2;
            var circle = new D.Circle (center, radius);
            CommandManager.ExecuteListAdd (CurrentLayer.Shapes, circle);
        }

        protected override void DoDrawDragDrop (DrawingContext drawingContext)
        {
            Point center = G.Segment.GetCenter (StartPoint.Value, EndPoint.Value);
            double radius = Point.DistanceBetween (StartPoint.Value, EndPoint.Value) / 2;
            var rect = new Rect (center.X - radius, center.Y - radius, 2 * radius, 2 * radius);
            drawingContext.StrokeCircle (rect);
        }
    }
}

