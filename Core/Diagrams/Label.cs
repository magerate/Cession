using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class LocationChangedEventArgs:RoutedEventArgs
    {
        public Point OldLocation{ get; set; }
        public Point NewLocation{ get; set; }

        public LocationChangedEventArgs (RoutedEvent routedEvent, object source, Point oldLocation,Point newLocation) : base (routedEvent, source)
        {
            OldLocation = oldLocation;
            NewLocation = newLocation;
        }
    }

    public class Label:CustomShape
    {
        public static readonly RoutedEvent LocationChangeEvent = new RoutedEvent ("LocationChange", 
            typeof(EventHandler<LocationChangedEventArgs>), 
            typeof(Label));
        
        private string _text;
        private Point _location;

        public string Text
        {
            get{ return _text; }
            set{ _text = value; }
        }

        public Point Location
        {
            get{ return _location; }
        }

        public Label (string text, Point location, Shape parent) : base (parent)
        {
            _text = text;
            _location = location;
        }

        public Label (string text) : this (text, Point.Empty, null)
        {
        }

        public void SetLocation(Point location,bool isSideEffect)
        {
            if (location == _location)
                return;
            
            if (isSideEffect)
            {
                var re = new LocationChangedEventArgs (LocationChangeEvent, this, _location, location);
                _location = location;
                RaiseEvent (re);
            }
            else
                _location = location;
        }

        internal override void DoOffset (double x, double y)
        {
            _location.Offset (x, y);
        }

        internal override void DoRotate (Point point, double radian)
        {
            throw new NotSupportedException ();
        }
    }
}

