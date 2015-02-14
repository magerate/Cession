using System;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class VertexChangedEventArgs:RoutedEventArgs
    {
        public Point Point{get;set;}

        public VertexChangedEventArgs(RoutedEvent routedEvent,object source,Point point):base(routedEvent,source)
        {
            Point = point;
        }
    }

    public abstract class Segment:Shape
    {
        public static readonly RoutedEvent VertexChangeEvent = new RoutedEvent ("VertexChange", 
                                                           typeof(EventHandler<VertexChangedEventArgs>), 
                                                           typeof(Segment));

        private Point _point1;

//        public event EventHandler<VertexChangedEventArgs> VertexChanged
//        {
//            add{AddHandler(VertexChangeEvent,value);}
//            remove{ RemoveHandler (VertexChangeEvent, value);}
//        }

        public Point Point1 
        {
            get{ return _point1; }
            set 
            {
                if (value != _point1) {
                    _point1 = value;
                    var rea = new VertexChangedEventArgs (VertexChangeEvent, this,value);
                    RaiseEvent (rea);
                }
            }
        }

        internal Segment (Point point)
        {
            _point1 = point;
            Ability = ShapeAbility.CanAssign | ShapeAbility.CanHitTest;
        }

        public Segment Next 
        {
            get { 
                if (Parent is Path)
                    return ((Path)Parent).GetNextSide (this);
                else if (Parent is Polyline)
                    return ((Polyline)Parent).GetNextSide (this);
                return null;
            }
        }

        public Segment Previous {
            get {
                if (Parent is Path)
                    return ((Path)Parent).GetPreviousSide (this);
                else if (Parent is Polyline)
                    return ((Polyline)Parent).GetPreviousSide (this);
                return null;
            }
        }

        internal override void DoOffset (double x, double y)
        {
            _point1.Offset (x, y);
        }

        internal override void DoRotate (Point point, double radian)
        {
            _point1.Rotate (point, radian);
        }
    }
}

