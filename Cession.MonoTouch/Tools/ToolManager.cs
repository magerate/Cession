using System;
using System.Collections.Generic;

using UIKit;
using CoreGraphics;
using Foundation;

using Cession.Utilities;
using Cession.Diagrams;
using Cession.Commands;

namespace Cession.Tools
{
    public interface IToolHost
    {
        UIView DiagramView{ get; }

        UIView ToolView{ get; }

        Layer Layer{ get; }

        CommandManager CommandManager{ get; }
    }


    public class ToolManager
    {
        public IToolHost Host{ get; set; }

        private Dictionary<ToolType,Tool> _tools = new Dictionary<ToolType, Tool> ();
        private Stack<ToolType> _toolStack = new Stack<ToolType> ();

        public ToolType CurrentToolType{ get; private set; }

        public ToolManager ()
        {
            CurrentToolType = ToolType.Select;
        }

        public ToolManager (ToolType toolType)
        {
            CurrentToolType = toolType;
        }

        private Tool CreateTool (ToolType toolType)
        {
            var attribute = toolType.GetAttribute (typeof(ToolAttribute)) as ToolAttribute;
            var tool = Activator.CreateInstance (attribute.Type, this) as Tool;
            return tool;
        }

        public Tool CurrentTool
        {
            get{ return GetTool (CurrentToolType); }
        }

        public Tool GetTool (ToolType toolType)
        {
            Tool tool;
            if (_tools.TryGetValue (toolType, out tool))
                return tool;

            tool = CreateTool (toolType);
            _tools [toolType] = tool;
            return tool;
        }

        public void SelectTool (ToolType toolType, params object[] args)
        {
            if (CurrentToolType == toolType)
                return;

            var prevToolType = CurrentToolType;
            CurrentTool.Leave ();
            CurrentToolType = toolType;
            CurrentTool.Enter (null, args);
            _toolStack.Clear ();
        }

        public void PushTool (ToolType toolType, params object[] args)
        {
            if (CurrentToolType == toolType)
                return;

            CurrentTool.WillPushTool (toolType);
            SaveState ();
            var parentTool = CurrentTool;
            CurrentToolType = toolType;
            CurrentTool.Enter (parentTool, args);
        }


        public void SaveState ()
        {
            _toolStack.Push (CurrentToolType);
        }

        public void RestoreState ()
        {
            var tt = CurrentToolType;
            CurrentTool.Leave ();
            CurrentToolType = _toolStack.Pop ();
            CurrentTool.RestoredFrom (tt);
        }
    }
}



