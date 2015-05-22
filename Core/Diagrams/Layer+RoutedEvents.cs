using System;

namespace Cession.Diagrams
{
    public partial class Layer
    {
        public event EventHandler<LocationChangedEventArgs> LabelLocationChanged
        {
            add{ AddHandler (Label.LocationChangeEvent, value);}
            remove{ RemoveHandler (Label.LocationChangeEvent, value);}
        }
    }
}

