namespace Cession.Modeling
{
	using System;

	using Cession.Geometries;
	using Cession.Modeling;

	//helper for create handles
	public static class HandleHelper
	{
		public static Handle[] GetHandles (this ClosedShapeDiagram diagram,Matrix matrix)
		{
			var polygon = diagram as IPolygonal;
			if (null != polygon) {
				Handle[] handles = new Handle[polygon.SideCount];
				for (int i = 0; i < handles.Length; i++) {
					handles [i] = new SideHandle (matrix.Transform (polygon [i].Center), i, diagram);
				}
				return handles;
			}
			return null;
		}

	}
}

