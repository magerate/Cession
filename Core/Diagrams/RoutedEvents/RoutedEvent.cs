using System;

namespace Cession.Diagrams
{
    public sealed class RoutedEvent
    {
        //		public RoutedEvent AddOwner(Type ownerType)
        //		{
        //			GlobalEventManager.AddOwner(this, ownerType);
        //			return this;
        //		}

        public string Name
        {
            get { return _name; }
        }

        public Type HandlerType
        {
            get { return _handlerType; }
        }

        internal bool IsLegalHandler (Delegate handler)
        {
            Type handlerType = handler.GetType ();

            return ((handlerType == HandlerType) ||
            (handlerType == typeof(RoutedEventHandler)));
        }

        public Type OwnerType
        {
            get { return _ownerType; }
        }

        public override string ToString ()
        {
            return string.Format ("{0}.{1}", _ownerType.Name, _name);
        }


        #region Construction

        // Constructor for a RoutedEvent (is internal
        // to the EventManager and is onvoked when a new
        // RoutedEvent is registered)
        internal RoutedEvent (
            string name,
            Type handlerType,
            Type ownerType)
        {
            _name = name;
            _handlerType = handlerType;
            _ownerType = ownerType;

//			_globalIndex = GlobalEventManager.GetNextAvailableGlobalIndex(this);
        }

        /// <summary>
        ///    Index in GlobalEventManager 
        /// </summary>
        //		internal int GlobalIndex
        //		{
        //			get { return _globalIndex; }
        //		}
        #endregion Construction

		#region Data

		private string _name;
        private Type _handlerType;
        private Type _ownerType;

        //		private int _globalIndex;

        #endregion Data
    }
}

