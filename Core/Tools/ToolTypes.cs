namespace Cession.Tools
{
	using System;

	[AttributeUsage(AttributeTargets.Field)]
	public class ToolAttribute:Attribute
	{
		public Type Type{get;set;}
		public ToolAttribute(Type type)
		{
			this.Type = type;
		}
	}

	public enum ToolType
	{
		[Tool(typeof(SelectTool))]
		Select,

		[Tool(typeof(MoveTool))]
		Move,

		[Tool(typeof(AddRectRoomTool))]
		AddRectangularRoom,

		[Tool(typeof(AddPolygonalRoomTool))]
		AddPolygonalRoom,

	}
}




