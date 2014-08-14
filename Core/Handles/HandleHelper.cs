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
			int count;
			if (diagram is RectangleDiagram)
				count = 4;
			else
				count = (diagram as PathDiagram).Points.Count;

			Handle[] handles = new Handle[count];
			for (int i = 0; i < handles.Length; i++) {
				handles [i] = new SideHandle (matrix.Transform (diagram [i].Center),i,diagram);
			}
			return handles;
		}

	}
}

