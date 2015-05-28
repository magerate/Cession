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

        public void Layout (ClosedShape contour)
        {
            LayoutProvider.CurrentProvider.Layout (contour, _walls);
        }

    }
}

