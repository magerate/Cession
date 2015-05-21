using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public partial class Shape
    {
        private static Dictionary<Type,HitTestProvider> s_hitTestors;

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


        protected HitTestProvider GetHitTestProvider ()
        {
            if (null == s_hitTestors)
                return null;

            HitTestProvider htp;
            s_hitTestors.TryGetValue (GetType (), out htp);
            return htp;
        }

        public bool Contains (Point point)
        {
            var htp = GetHitTestProvider ();
            if (null != htp)
                return htp.Contains (this, point);
            return DoContains (point);
        }

        public Shape HitTest (Point point)
        {
            if (!CanHitTest)
                return null;

            var htp = GetHitTestProvider ();
            if (null != htp)
                return htp.HitTest (this, point);
            return DoHitTest (point);
        }
            
        protected virtual Shape DoHitTest (Point point)
        {
            if (Contains (point))
                return this;
            return null;
        }

        protected virtual bool DoContains (Point point)
        {
            return false;
        }

        protected virtual Rect DoGetBounds ()
        {
            return Rect.Empty;
        }
    }
}

