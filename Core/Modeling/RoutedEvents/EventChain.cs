namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;


	public sealed class EventChain
	{
		private List<Delegate> routeItemList = new List<Delegate>();

		public EventChain ()
		{
		}

		public void Add(Delegate handler){
			routeItemList.Add (handler);
		}

		public void Remove(Delegate handler){
			routeItemList.Remove (handler);
		}

		public void InvokeHandlers(object source,RoutedEventArgs args){
			args.Source = source;
			foreach (var item in routeItemList) {
				DoInvoke (source,item, args);
			}
		}

		private void DoInvoke(object source,Delegate handler,RoutedEventArgs args){
			if (handler is RoutedEventHandler)
				((RoutedEventHandler)handler).Invoke (source, args);
			else
				handler.DynamicInvoke (new object[]{ source, args });
		}
	}
}

