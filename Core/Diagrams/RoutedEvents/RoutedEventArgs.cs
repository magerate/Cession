using System;
using System.Collections.Specialized;

namespace Cession.Diagrams
{
	public class RoutedEventArgs : EventArgs
	{
		public RoutedEventArgs()
		{
		}

		public RoutedEventArgs(RoutedEvent routedEvent) : this( routedEvent, null)
		{
		}

		public RoutedEventArgs(RoutedEvent routedEvent, object source)
		{
			_routedEvent = routedEvent;
			_source = _originalSource = source;
		}


		#region External API
		public RoutedEvent RoutedEvent
		{
			get {return _routedEvent;}
			set
			{
				_routedEvent = value;
			}
		}


		public bool Handled{ get; set; }


		public object Source
		{
			get {return _source;}
			set
			{
				if (_routedEvent == null)
				{
					throw new InvalidOperationException();
				}


				object source = value ;
				if (_source == null && _originalSource == null)
				{
					// Gets here when it is the first time that the source is set.
					// This implies that this is also the original source of the event
					_source = _originalSource = source;
					OnSetSource(source);
				}
				else if (_source != source)
				{
					// This is the actiaon taken at all other times when the
					// source is being set to a different value from what it was
					_source = source;
					OnSetSource(source);
				}
			}
		}

		internal void OverrideSource( object source )
		{
			_source = source;
		}

		public object OriginalSource
		{
			get {return _originalSource;}
		}

		protected virtual void OnSetSource(object source)
		{
		}

		#endregion External API



		#region Data

		private RoutedEvent _routedEvent;
		private object _source;
		private object _originalSource;
		#endregion Data
	}

	/// <summary>
	///     RoutedEventHandler Definition
	/// </summary>
	/// <ExternalAPI/>
	public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);
}

