namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;

	using Cession.Geometries;

	public abstract class Diagram
	{
		#region "routed events"
		private Dictionary<RoutedEvent,Delegate> eventStore ;

		public static readonly RoutedEvent MoveEvent = new RoutedEvent ("Move", 
			typeof(RoutedEventHandler), 
			typeof(Diagram));

		public static readonly RoutedEvent ShapeChangeEvent = new RoutedEvent ("ShapeChange", 
			typeof(RoutedEventHandler), 
			typeof(Diagram));

		public void AddHandler(RoutedEvent routedEvent,Delegate handler){
			if (null == handler)
				throw new ArgumentNullException ();

			if(!routedEvent.IsLegalHandler(handler))
				throw new ArgumentException("handler");

			if (null == eventStore)
				eventStore = new Dictionary<RoutedEvent, Delegate>();

			Delegate currentHandler = GetHandler(routedEvent);
			if (null == currentHandler)
				eventStore.Add (routedEvent, handler);
			else
				eventStore [routedEvent] = Delegate.Combine (currentHandler, handler);
		}

		public void RemoveHandler(RoutedEvent routedEvent,Delegate handler){
			if (null == handler)
				throw new ArgumentNullException ();

			if(!routedEvent.IsLegalHandler(handler))
				throw new ArgumentException("handler");

			if (null == eventStore)
				return;

			Delegate currentHandler = GetHandler(routedEvent);
			if (null == currentHandler)
				return;
			currentHandler = Delegate.Remove (currentHandler, handler);
			if (null == currentHandler)
				eventStore.Remove (routedEvent);
			else
				eventStore [routedEvent] = currentHandler;
		}

		private Delegate GetHandler(RoutedEvent routedEvent){
			Delegate handler;
			eventStore.TryGetValue (routedEvent, out handler);
			return handler;
		}

		protected void RaiseEvent(RoutedEventArgs args){
			Diagram diagram = this;
			while (diagram != null) {
				diagram.InvokeHandler (args);
				diagram = diagram.Parent;
			}
		}

		protected void InvokeHandler(RoutedEventArgs args){
			if (null == eventStore)
				return;

			args.Source = this;
			var handler = GetHandler (args.RoutedEvent);
			if (null != handler) {
				if (handler is RoutedEventHandler)
					((RoutedEventHandler)handler).Invoke (this, args);
				else
					handler.DynamicInvoke (new object[]{ this, args });
			}
		}
		#endregion

		public Diagram Parent{ get; internal set; }

		public Diagram Owner
		{
			get{
				var parent = Parent;
				while (parent != null && parent.Parent != null)
					parent = parent.Parent;
				return parent;
			}
		}

		public Diagram GetSelectableAncestor(){
			var diagram = this;
			while (diagram != null) {
				if (diagram.CanSelect)
					return diagram;
				diagram = diagram.Parent;
			}
			return diagram;
		}

		public virtual bool CanSelect{
			get{ return false; }
		}

		protected Diagram()
		{
		}

		protected Diagram (Diagram parent)
		{
			this.Parent = parent;
		}

		public abstract Rect Bounds{ get; }
		public abstract Diagram HitTest (Point2 point);

		internal void InternalOffset(Vector vector)
		{
			this.InternalOffset ((int)vector.X, (int)vector.Y);
		}

		internal abstract void InternalOffset (int x, int y);


		public virtual void Offset(int x,int y){
			InternalOffset (x, y);
			RaiseEvent (new RoutedEventArgs (Diagram.MoveEvent, this));
		}

		public void Offset(Vector offset){
			this.Offset ((int)offset.X, (int)offset.Y);
		}
	}
}

