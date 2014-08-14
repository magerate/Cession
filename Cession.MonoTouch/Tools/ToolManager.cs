namespace Cession.Tools
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.CoreGraphics;
	using MonoTouch.Foundation;

	using Cession.Utilities;
	using Cession.Modeling;
	using Cession.Commands;

	public interface IToolHost
	{
		UIView DiagramView{ get; }
		UIView ToolView{ get; }
		Project Project{ get; }
		CommandManager CommandManager{ get; }
	}
	

	public class ToolManager
	{
		public IToolHost Host{get;set;}
		
		private Dictionary<ToolType,Tool> tools = new Dictionary<ToolType, Tool>();
		private Stack<ToolType> toolStack = new Stack<ToolType>();

		public ToolType CurrentToolType{get;private set;}

		public ToolManager ()
		{
			CurrentToolType = ToolType.Select;
		}

		public ToolManager(ToolType toolType)
		{
			CurrentToolType = toolType;
		}

		private Tool CreateTool(ToolType toolType)
		{
			var attribute = toolType.GetAttribute (typeof(ToolAttribute)) as ToolAttribute;
			var tool = Activator.CreateInstance(attribute.Type,this) as Tool;
			return tool;
		}

		public Tool CurrentTool
		{
			get{return GetTool(CurrentToolType);}
		}

		public Tool GetTool(ToolType toolType)
		{
			Tool tool;
			if(tools.TryGetValue(toolType,out tool))
				return tool;
			
			tool = CreateTool(toolType);
			tools[toolType] = tool;
			return tool;
		}
		
		public void SelectTool(ToolType toolType,params object[] args)
		{
			if (CurrentToolType == toolType)
				return;

			var prevToolType = CurrentToolType;
			CurrentTool.Leave();
			CurrentToolType = toolType;
			CurrentTool.Enter(null,args);
			toolStack.Clear();
		}
		
		public void PushTool(ToolType toolType,params object[] args)
		{
			if(CurrentToolType == toolType)
				return;

			CurrentTool.WillPushTool (toolType);
			SaveState();
			var parentTool = CurrentTool;
			CurrentToolType = toolType;
			CurrentTool.Enter(parentTool,args);
		}


		public void SaveState()
		{
			toolStack.Push(CurrentToolType);
		}
		
		public void RestoreState()
		{
			var tt = CurrentToolType;
			CurrentTool.Leave();
			CurrentToolType = toolStack.Pop();
			CurrentTool.RestoredFrom(tt);
		}
	}
}



