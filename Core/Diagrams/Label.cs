using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Label:Shape
    {
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
            set{ _location = value; }
        }

        public Label (string text, Point location, Shape parent) : base (parent)
        {
            _text = text;
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

