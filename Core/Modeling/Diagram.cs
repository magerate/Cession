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
			EventHandlerStore.AddHandler (this,routedEvent, handler);
		}

		public void RemoveHandler(RoutedEvent routedEvent,Delegate handler){
			EventHandlerStore.RemoveHandler (this,routedEvent, handler);
		}

		public void RaiseEvent(RoutedEventArgs e){
			var chain = EventHandlerStore.GetEventChain (e.RoutedEvent);
			if (null != chain)
				chain.InvokeHandlers (this, e);
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

		public bool IsAncestorOf(Diagram diagram){
			var parent = diagram.Parent;

			while (true) {
				if (parent == this)
					return true;

				if (parent == null)
					break;

				parent = parent.Parent;
			}
			return false;
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

		internal void InternalOffset(Vector vector)
		{
			this.InternalOffset ((int)vector.X, (int)vector.Y);
		}

		internal abstract void InternalOffset (int x, int y);


		public virtual void Move(int x,int y){
			InternalOffset (x, y);
			RaiseEvent (new RoutedEventArgs (Diagram.MoveEvent, this));
		}

		public void Move(Vector offset){
			this.Move ((int)offset.X, (int)offset.Y);
		}
	}
}

