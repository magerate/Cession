using System;

namespace Cession.Tools
{
    [AttributeUsage (AttributeTargets.Field)]
    public class ToolAttribute:Attribute
    {
        public Type Type{ get; set; }

        public ToolAttribute (Type type)
        {
            this.Type = type;
        }
    }

    public enum ToolType
    {
        [Tool (typeof(SelectTool))]
        Select,

        [Tool (typeof(PanTool))]
        Pan,

        [Tool (typeof(ZoomTool))]
        Zoom,

        [Tool (typeof(MoveTool))]
        Move,

        [Tool (typeof(AddPolylineTool))]
        AddPolyline,

        //		[Tool(typeof(AddRectRoomTool))]
        //		AddRectangularRoom,
        //
        //		[Tool(typeof(AddPolygonalRoomTool))]
        //		AddPolygonalRoom,
        //
        //		[Tool(typeof(MoveSideTool))]
        //		MoveSide,

    }
}




