using System;
using System.Collections.Generic;

namespace Cession.Diagrams
{
    public abstract class LayoutProvider
    {
        public static LayoutProvider CurrentProvider{ get; set; }
        public static readonly LayoutProvider[] Providers;
      

        static LayoutProvider()
        {
            Providers = new LayoutProvider[] {
                new FlowLayoutProvider(),
                new FanLayoutProvider(),
            };
            CurrentProvider = Providers [0];
        }

        public abstract void Layout (ClosedShape contour, IEnumerable<WallSurface> walls);
    }
}

