using System;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
	public partial class Shape
	{
		private static Dictionary<Type,HitTestProvider> s_hitTestors;

		public static void RegisterHitTestProvider(Type type,HitTestProvider hitTestProvider){
			if (null == type)
				throw new ArgumentNullException ();
			if (null == hitTestProvider)
				throw new ArgumentNullException ();

			if(!type.IsSubclassOf(typeof(Shape)))
				throw new ArgumentException();

			if (null == s_hitTestors)
				s_hitTestors = new Dictionary<Type, HitTestProvider> ();
			s_hitTestors [type] = hitTestProvider;
		}

		public static void UnregisterHitTestProvider(Type type,HitTestProvider hitTestProvider){
			if (null == type)
				throw new ArgumentNullException ();
			if (null == hitTestProvider)
				throw new ArgumentNullException ();

			if(!type.IsSubclassOf(typeof(Shape)))
				throw new ArgumentException();

			if (null == s_hitTestors)
				return;

			s_hitTestors.Remove (type);
		}


		protected HitTestProvider GetHitTestProvider(){
			if (null == s_hitTestors)
				return null;
			return s_hitTestors [this.GetType ()];
		}

		public bool Contains (Point2 point){
			var htp = GetHitTestProvider ();
			if (null != htp)
				return htp.Contains (this, point);
			return DoContains (point);
		}

		public Shape HitTest(Point2 point){
			if (!CanHitTest)
				return null;

			var htp = GetHitTestProvider ();
			if (null != htp)
				return htp.HitTest (this, point);
			return DoHitTest (point);
		}

		public  Rect GetBounds(){
			var htp = GetHitTestProvider ();
			if (null != htp)
				return htp.GetBounds (this);
			return DoGetBounds ();
		}

		protected virtual Shape DoHitTest (Point2 point){
			if (Contains (point))
				return this;
			return null;
		}

		protected virtual bool DoContains (Point2 point){
			return false;
		}

		protected virtual Rect DoGetBounds (){
			return Rect.Empty;
		}
	}
}

