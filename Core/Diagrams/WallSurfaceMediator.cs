using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Cession.Geometries;

namespace Cession.Diagrams
{
    internal class WallSurfaceMediator
    {
        private List<WallSurface> _walls;
        public ReadOnlyCollection<WallSurface> Walls{ get; private set; }

        public WallSurfaceMediator (Shape owner,ClosedShape contour,double height)
        {
            _walls = new List<WallSurface> ();
            Walls = new ReadOnlyCollection<WallSurface> (_walls);
            CreateWalls (owner,contour, height);
        }

        private void CreateWalls (Shape owner,ClosedShape contour, double height)
        {
            if (contour is Path)
            {
                var path = contour as Path; 
                foreach (var segment in path.Segments)
                {
                    var wall = new WallSurface (segment, height);
                    wall.Parent = owner;
                    _walls.Add (wall);
                }
            }
            else if (contour is Circle)
            {
                var circle = contour as Circle;
                var wall = new WallSurface (circle, height);
                wall.Parent = owner;
                _walls.Add (wall);
            }
        }

        public void Layout (Rect bounds)
        {
            double margin = 32;
            double maxWidth = 400;

            double maxX = bounds.Right + margin + maxWidth;

            double ox = bounds.Right + margin;
            double oy = bounds.Y - margin;

            double tx = ox;
            double ty = oy;

            foreach (var w in _walls)
            {
                Matrix m = new Matrix ();
                m.Translate (tx, ty);
                w.Transform = m;

                tx += w.Bounds.Width + margin;
                if (tx >= maxX)
                {
                    tx = ox;
                    ty += w.Height + margin;
                }
            }
        }

    }
}

