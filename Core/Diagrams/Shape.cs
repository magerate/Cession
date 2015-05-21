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

        public virtual Point Center
        {
            get{ return Bounds.Center; }
        }

        public Rect Bounds
        { 
            get{ return DoGetBounds (); }
        }

        public bool Contains (Point point)
        {
            return DoContains (point);
        }

        public Shape HitTest (Point point)
        {
            if (!CanHitTest)
                return null;

            return DoHitTest (point);
        }

        protected abstract Rect DoGetBounds();

        protected virtual bool DoContains (Point point)
        {
            return Bounds.Contains (point);
        }

        protected virtual Shape DoHitTest (Point point)
        {
            if (Contains (point))
                return this;
            return null;
        }

        public Shape GetSelectableAncestor()
        {
            return GetAncestor (s => s.CanSelect);
        }

        public Shape GetAncestor(Func<Shape,bool> predicate)
        {
            if (null == predicate)
                throw new ArgumentNullException ();
            
            Shape shape = this;
            while (shape != null)
            {
                if (predicate(shape))
                    return shape;
                shape = shape.Parent;
            }
            return null;
        }

        public bool IsAncestor(Shape shape)
        {
            while (shape != null)
            {
                if (shape == this)
                    return true;
                shape = shape.Parent;
            }
            return false;
        }

        protected Shape () : this (null)
        {
        }

        protected Shape (Shape parent)
        {
            this.Parent = parent;
            this.Ability = ShapeAbility.All;
        }

        public void Offset (double x, double y)
        {
            if (!CanOffset)
                throw new InvalidOperationException ();
            DoOffset (x, y);
            RaiseEvent (new OffsetEventArgs (Shape.OffsetEvent, this,x,y));
        }

        public void Offset(Vector vector)
        {
            Offset (vector.X, vector.Y);
        }

        public void Rotate (Point point, double radian)
        {
            if (!CanRotate)
                throw new InvalidOperationException ();
            DoRotate (point, radian);
            RaiseEvent (new RoutedEventArgs (Shape.RotateEvent, this));
        }

        internal abstract void DoOffset (double x, double y);
        internal abstract void DoRotate (Point point, double radian);
    }
}

