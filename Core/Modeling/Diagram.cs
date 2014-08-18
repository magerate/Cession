namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;

	using Cession.Geometries;

	public abstract class Diagram
	{
		#region "routed events"
		public static readonly RoutedEvent MoveEvent = new RoutedEvent ("Move", 
			typeof(RoutedEventHandler), 
			typeof(Diagram));

		public static readonly RoutedEvent ShapeChangeEvent = new RoutedEvent ("ShapeChange", 
			typeof(RoutedEventHandler), 
			typeof(Diagram));

		public void AddHandler(RoutedEvent routedEvent,Delegate handler){
			EventHandlerStore.AddHandler (routedEvent, handler);
		}

		public void RaiseEvent(RoutedEventArgs e){
			var chain = EventHandlerStore.GetEventChain (e.RoutedEvent);
			if (null != chain)
				chain.InvokeHandlers (this, e);
		}
//		public void AddHandler(
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

		public abstract Diagram HitTest (Point2 point);

		public void Offset(Vector vector)
		{
			this.Offset ((int)vector.X, (int)vector.Y);
		}

		public abstract void Offset (int x, int y);
	}
}

