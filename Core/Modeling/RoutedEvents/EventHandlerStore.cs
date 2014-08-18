﻿namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;

	internal static class EventHandlerStore
	{
		private static Dictionary<RoutedEvent,EventChain> eventStore = new Dictionary<RoutedEvent, EventChain>();

		internal static void AddHandler(object target,RoutedEvent routedEvent,
											Delegate handler){
			EventChain eventChain;
			if (!eventStore.TryGetValue (routedEvent, out eventChain)) {
				eventChain = new EventChain ();
				eventStore.Add (routedEvent, eventChain);
			}

			eventChain.Add (target,handler);
		}

		internal static void RemoveHandler(object target,RoutedEvent routedEvent,
											Delegate handler){

			EventChain eventChain;
			if (eventStore.TryGetValue (routedEvent, out eventChain)) {
				eventChain.Remove (target,handler);
			}
		}

		internal static EventChain GetEventChain(RoutedEvent routedEvent){
			EventChain eventChain;
			eventStore.TryGetValue (routedEvent, out eventChain);
			return eventChain;
		}
	}
}

