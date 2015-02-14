using System;
using System.Collections.Generic;

namespace Cession.Diagrams
{
    public class OffsetEventArgs:RoutedEventArgs
    {
        public double OffsetX{ get; set; }

        public double OffsetY{ get; set; }

        public OffsetEventArgs (RoutedEvent routedEvent, object source, double offsetX, double offsetY) :
            base (routedEvent, source)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }

    public  partial class Shape
    {
        private Dictionary<RoutedEvent,Delegate> _eventStore;

        public static readonly RoutedEvent OffsetEvent = new RoutedEvent ("Offset", 
                                                             typeof(EventHandler<OffsetEventArgs>), 
                                                             typeof(Shape));

        public static readonly RoutedEvent RotateEvent = new RoutedEvent ("Rotate", 
                                                             typeof(RoutedEventHandler), 
                                                             typeof(Shape));

        public event EventHandler<OffsetEventArgs> Offseted
        {
            add{ AddHandler (OffsetEvent, value);}
            remove{ RemoveHandler (OffsetEvent, value);}
        }

        public event RoutedEventHandler Rotated
        {
            add{ AddHandler (RotateEvent, value);}
            remove{ RemoveHandler (RotateEvent, value);}
        }

        public void AddHandler (RoutedEvent routedEvent, Delegate handler)
        {
            if (null == handler)
                throw new ArgumentNullException ();

            if (!routedEvent.IsLegalHandler (handler))
                throw new ArgumentException ("handler");

            if (null == _eventStore)
                _eventStore = new Dictionary<RoutedEvent, Delegate> ();

            Delegate currentHandler = GetHandler (routedEvent);
            if (null == currentHandler)
                _eventStore.Add (routedEvent, handler);
            else
                _eventStore [routedEvent] = Delegate.Combine (currentHandler, handler);
        }

        public void RemoveHandler (RoutedEvent routedEvent, Delegate handler)
        {
            if (null == handler)
                throw new ArgumentNullException ();

            if (!routedEvent.IsLegalHandler (handler))
                throw new ArgumentException ("handler");

            if (null == _eventStore)
                return;

            Delegate currentHandler = GetHandler (routedEvent);
            if (null == currentHandler)
                return;
            currentHandler = Delegate.Remove (currentHandler, handler);
            if (null == currentHandler)
                _eventStore.Remove (routedEvent);
            else
                _eventStore [routedEvent] = currentHandler;
        }

        private Delegate GetHandler (RoutedEvent routedEvent)
        {
            Delegate handler;
            _eventStore.TryGetValue (routedEvent, out handler);
            return handler;
        }

        protected void RaiseEvent (RoutedEventArgs args)
        {
            var shape = this;
            while (shape != null)
            {
                shape.InvokeHandler (args);
                shape = shape.Parent;
            }
        }

        protected void InvokeHandler (RoutedEventArgs args)
        {
            if (null == _eventStore)
                return;

            args.Source = this;
            var handler = GetHandler (args.RoutedEvent);
            if (null != handler)
            {
                if (handler is RoutedEventHandler)
                    ((RoutedEventHandler)handler).Invoke (this, args);
                else
                    handler.DynamicInvoke (new object[]{ this, args });
            }
        }
    }
}

