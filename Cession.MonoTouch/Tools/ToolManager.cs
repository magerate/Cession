using System;
using System.Linq;
using System.Collections.Generic;

using UIKit;
using CoreGraphics;
using Foundation;

using Cession.Utilities;
using Cession.Diagrams;
using Cession.Commands;
using Cession.Projects;
using Cession.Handles;

namespace Cession.Tools
{
    public interface IToolHost
    {
        UIView DiagramView{ get; }
        UIView ToolView{ get; }
        Project Project{ get; }
        CommandManager CommandManager{ get; }
        HandleManager HandleManager{ get; }
    }

    public class ToolManager
    {
        public IToolHost Host{ get; set; }

        public event EventHandler<EventArgs> CurrentToolTypeChanged;

        private Dictionary<Type,Tool> _tools = new Dictionary<Type, Tool> ();
        private Stack<Type> _toolStack = new Stack<Type> ();
        private Type _currentToolType; 

        public Type CurrentToolType
        { 
            get{ return _currentToolType; }
            private set
            {
                if (value != _currentToolType)
                {
                    _currentToolType = value;
                    if (null != CurrentToolTypeChanged)
                        CurrentToolTypeChanged (this, EventArgs.Empty);
                }
            }
        }

        public ToolManager ()
        {
            CurrentToolType = typeof(SelectTool);
        }

        public ToolManager (Type toolType)
        {
            CurrentToolType = toolType;
        }

        private Tool CreateTool (Type toolType)
        {
            var tool = Activator.CreateInstance (toolType, this) as Tool;
            return tool;
        }

        public Tool CurrentTool
        {
            get{ return GetTool (CurrentToolType); }
        }

        public Tool RootTool
        {
            get
            {
                if (_toolStack.Count == 0)
                    return CurrentTool;

                Type type = _toolStack.First ();
                return GetTool(type);
            }
        }

        public Tool GetTool (Type toolType)
        {
            Tool tool;
            if (_tools.TryGetValue (toolType, out tool))
                return tool;

            tool = CreateTool (toolType);
            _tools [toolType] = tool;
            return tool;
        }

        public void SelectTool (Type toolType, params object[] args)
        {
            if (CurrentToolType == toolType)
                return;

            Type prevToolType = CurrentToolType;
            CurrentTool.Leave ();
            Tool tool = GetTool (toolType);
            tool.Enter (null, args);
            CurrentToolType = toolType;

            _toolStack.Clear ();
        }

        public void PushTool (Type toolType, params object[] args)
        {
            if (CurrentToolType == toolType)
                return;

            SaveState ();
            Tool parentTool = CurrentTool;
            Tool tool = GetTool (toolType);
            tool.Enter (parentTool, args);
            CurrentToolType = toolType;
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



