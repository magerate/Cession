namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;

	internal struct RoutedItem{
		private object target;
		private Delegate handler;

		internal object Target{
			get{ return target; }
		}

		internal Delegate Handler{
			get{ return handler; }
			set{ handler = value; }
		}

		internal RoutedItem(object target,Delegate handler){
			this.target = target;
			this.handler = handler;
		}
	}

	public sealed class EventChain
	{
		private List<RoutedItem> routeItemList = new List<RoutedItem>();

		public EventChain ()
		{
		}

		public void Add(object target,Delegate handler){
			int index = routeItemList.FindIndex (i => i.Target == target);
			if (index != -1) {
				routeItemList[index] = new RoutedItem(target,
					Delegate.Combine(routeItemList[index].Handler,handler));
			} else {
				var routedItem = new RoutedItem (target, handler);
				routeItemList.Add (routedItem);
			}
		}

		public void Remove(object target,Delegate handler){
			int index = routeItemList.FindIndex (i => i.Target == target);
			if (index != -1) {
				if (null == Delegate.Remove (routeItemList[index].Handler, handler))
					routeItemList.RemoveAt (index);
			}
		}

		public void InvokeHandlers(object source,RoutedEventArgs args){
			args.Source = source;
			foreach (var item in routeItemList) {
				var node = item.Target as Diagram;
				if(node.IsAncestorOf(source as Diagram))
					DoInvoke (source,item.Handler, args);
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

