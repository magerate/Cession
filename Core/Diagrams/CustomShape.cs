using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public abstract class CustomShape:Shape
    {
        private static Dictionary<Type,HitTestProvider> s_hitTestors;

        protected CustomShape (Shape parent):base(parent)
        {
        }

        public static void RegisterHitTestProvider (Type type, HitTestProvider hitTestProvider)
        {
            if (null == type)
                throw new ArgumentNullException ();
            if (null == hitTestProvider)
                throw new ArgumentNullException ();

            if (!type.IsSubclassOf (typeof(Shape)))
                throw new ArgumentException ();

            if (null == s_hitTestors)
                s_hitTestors = new Dictionary<Type, HitTestProvider> ();
            s_hitTestors [type] = hitTestProvider;
        }

        public static void UnregisterHitTestProvider (Type type, HitTestProvider hitTestProvider)
        {
            if (null == type)
                throw new ArgumentNullException ();
            if (null == hitTestProvider)
                throw new ArgumentNullException ();

            if (!type.IsSubclassOf (typeof(Shape)))
                throw new ArgumentException ();

            if (null == s_hitTestors)
                return;

            s_hitTestors.Remove (type);
        }

        protected HitTestProvider HitTestProvider
        {
            get
            {
                if (null == s_hitTestors)
                    throw new InvalidOperationException ($"{GetType().Name} haven't set HitTestProvider yet!");

                HitTestProvider htp;
                s_hitTestors.TryGetValue (GetType (), out htp);
                if (null == htp)
                    throw new InvalidOperationException ($"{GetType().Name} haven't set HitTestProvider yet!");
                return htp;
            }
        }

        protected override bool DoContains (Point point)
        {
            return HitTestProvider.Contains (this,point);
        }

        protected override Shape DoHitTest (Point point)
        {
            return HitTestProvider.HitTest (this,point);
        }

        protected override Rect DoGetBounds ()
        {
            return HitTestProvider.GetBounds (this);
        }



    }
}

