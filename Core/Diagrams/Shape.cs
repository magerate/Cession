using System;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public abstract partial class Shape
    {
        internal ShapeAbility Ability{ get; set; }

        public bool CanSelect 
        {
            get{ return (Ability & ShapeAbility.CanSelect) != 0; }
        }

        public bool CanHitTest 
        {
            get{ return (Ability & ShapeAbility.CanHitTest) != 0; }
        }

        public bool CanOffset 
        {
            get{ return (Ability & ShapeAbility.CanOffset) != 0; }
        }

        public bool CanRotate 
        {
            get{ return (Ability & ShapeAbility.CanRotate) != 0; }
        }

        public bool CanAssign 
        {
            get{ return (Ability & ShapeAbility.CanAssign) != 0; }
        }


        public Shape Parent{ get; internal set; }


        public Shape Owner 
        {
            get 
            {
                var parent = Parent;
                while (parent != null && parent.Parent != null)
                    parent = parent.Parent;
                return parent;
            }
        }


        protected Shape () : this (null)
        {
        }

        protected Shape (Shape parent)
        {
            this.Parent = parent;
            this.Ability = ShapeAbility.All;
        }

        public void Offset (int x, int y)
        {
            if (!CanOffset)
                throw new InvalidOperationException ();
            DoOffset (x, y);
            RaiseEvent (new RoutedEventArgs (Shape.OffsetEvent, this));
        }

        public void Rotate (Point point, double radian)
        {
            if (!CanRotate)
                throw new InvalidOperationException ();
				
            DoRotate (point, radian);
            RaiseEvent (new RoutedEventArgs (Shape.RotateEvent, this));
        }

        internal abstract void DoOffset (int x, int y);

        internal abstract void DoRotate (Point point, double radian);
    }
}

