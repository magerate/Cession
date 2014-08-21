namespace Cession.Modeling
{
	using System;

	public class DoorRemovedEventArgs:RoutedEventArgs
	{
		public bool IsSideEffect{get;set;}
		public Door Door{get;set;}

		public DoorRemovedEventArgs(RoutedEvent routedEvent) : this( routedEvent, null)
		{
		}

		public DoorRemovedEventArgs(RoutedEvent routedEvent, object source):base(routedEvent,source)
		{
			IsSideEffect = false;
		}
	}
}

