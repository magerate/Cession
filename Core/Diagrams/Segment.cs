using System;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class VertexChangedEventArgs:RoutedEventArgs
    {
        public Point Point{get;set;}
        public bool IsFirstVertex{ get; set;}
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

        public static readonly RoutedEvent LengthChangedEvent = new RoutedEvent ("LengthChange",
                                                                    typeof(EventHandler<RoutedEventArgs>),
                                                                    typeof(Segment));

        private Point _point1;

        public event EventHandler<VertexChangedEventArgs> VertexChanged
        {
            add{AddHandler(VertexChangeEvent,value);}
            remove{ RemoveHandler (VertexChangeEvent, value);}
        }

        public event EventHandler<RoutedEventArgs> LengthChanged
        {
            add{ AddHandler (LengthChangedEvent, value);}
            remove{ RemoveHandler (LengthChangedEvent, value);}
        }

        public abstract double Length{ get; }

        internal Point InternalPoint1
        {
            get{ return _point1; }
            set{ _point1 = value; }
        }

        public Point Point1 
        {
            get{ return _point1; }
            set 
            {
                if (value != _point1) {
                    _point1 = value;
                    var rea = new VertexChangedEventArgs (VertexChangeEvent, this,value);
                    rea.IsFirstVertex = true;
                    RaiseEvent (rea);

                    var prev = Previous;
                    if(null != prev)
                    {
                        prev.OnLengthChanged ();
                    }
                    OnLengthChanged ();
                }
            }
        }

        public Point Point2
        {
            get
            { 
                var segment = Next;
                if (segment != null)
                    return segment.Point1;
                return ((Polyline)Parent).LastPoint;
            }
            set
            {
                var segment = Next;
                if (segment != null)
                    segment.Point1 = value;
                else
                    ((Polyline)Parent).LastPoint = value;
            }
        }

        internal Segment (Point point)
        {
            _point1 = point;
            Ability = ShapeAbility.CanAssign;
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

        internal void OnLengthChanged()
        {
            RaiseEvent (new RoutedEventArgs (LengthChangedEvent, this));
        }
    }
}

