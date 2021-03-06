using System;
using System.Collections.Generic;

using CoreGraphics;
using Cession.Diagrams;

namespace Cession.Drawing
{
    public static class DrawerManager
    {
        private static Dictionary<Type,ShapeDrawer> s_drawers = new Dictionary<Type, ShapeDrawer> ();

        static DrawerManager ()
        {
            RegisterDrawer (typeof(WallSurface), new WallSurfaceDrawer ());
            RegisterDrawer (typeof(Room), new RoomDrawer ());
            RegisterDrawer (typeof(Layer), new LayerDrawer ());
            RegisterDrawer (typeof(Label), new LabelDrawer ());
            RegisterDrawer (typeof(Polyline), new PolyLineDrawer ());
            RegisterDrawer (typeof(Circle), new CircleDrawer ());
            RegisterDrawer (typeof(Rectangle), new RectangleDrawer ());
            RegisterDrawer (typeof(Path), new PathDrawer ());
            RegisterDrawer (typeof(Elevation), new ElevationDrawer ());
        }

        public static void RegisterDrawer (Type type, ShapeDrawer drawer)
        {
            if (null == type)
                throw new ArgumentNullException ();

            if (null == drawer)
                throw new ArgumentNullException ();

            s_drawers [type] = drawer;
        }

        public static ShapeDrawer GetDrawer (Shape shape)
        {
            if (null == shape)
                throw new ArgumentNullException ();

            var type = shape.GetType ();
            ShapeDrawer drawer;
            s_drawers.TryGetValue (type, out drawer);
            return drawer;
        }

        public static void Draw (this Shape shape, DrawingContext context)
        {
            if (null == shape)
                throw new ArgumentNullException ();
            if (null == context)
                throw new ArgumentNullException ();

            var drawer = GetDrawer (shape);
            if (null == drawer)
                throw new InvalidOperationException ();

            drawer.Draw (context, shape);
        }

        public static void DrawSelected (this Shape shape, DrawingContext context)
        {
            if (null == shape)
                throw new ArgumentNullException ();
            if (null == context)
                throw new ArgumentNullException ();

            var drawer = GetDrawer (shape);
            if (null == drawer)
                throw new InvalidOperationException ();

            drawer.DrawSelected (context, shape);
        }
    }
}

