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

        private Dictionary<Type,Tool> _tools = new Dictionary<Type, Tool> ();
        private Stack<Type> _toolStack = new Stack<Type> ();

        public Type CurrentToolType{ get; private set; }

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

            var prevToolType = CurrentToolType;
            CurrentTool.Leave ();
            CurrentToolType = toolType;
            CurrentTool.Enter (null, args);
            _toolStack.Clear ();
        }

        public void PushTool (Type toolType, params object[] args)
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



