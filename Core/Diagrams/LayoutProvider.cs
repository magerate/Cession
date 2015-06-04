using System;
using System.Collections.Generic;

namespace Cession.Diagrams
{
    public abstract class LayoutProvider
    {
        private static LayoutProvider s_currentProvider;

        public static event EventHandler<EventArgs> CurrentProviderChanged;

        public static LayoutProvider CurrentProvider
        { 
            get{ return s_currentProvider; }
            set
            {
                if (null == value)
                    throw new ArgumentNullException ();

                if (value != s_currentProvider)
                {
                    s_currentProvider = value;
                    CurrentProviderChanged?.Invoke (null, EventArgs.Empty);
                }
            }
        }

        public static readonly LayoutProvider[] Providers;
      

        static LayoutProvider()
        {
            Providers = new LayoutProvider[] {
                new FlowLayoutProvider(),
                new FanLayoutProvider(),
            };
            s_currentProvider = Providers [0];
        }

        public abstract void Layout (ClosedShape contour, IEnumerable<WallSurface> walls);
    }
}

