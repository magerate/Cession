using System;
using UIKit;

using Cession.Commands;

namespace Cession.Tools
{
    public abstract class DiscreteTool:Tool
    {
        public UINavigationItem NavigationItem{ get; protected set; }

        private CommandManager _commandManager = new CommandManager ();

        public CommandManager CommandContainer
        {
            get{ return _commandManager; }
        }

        protected DiscreteTool (ToolManager toolManager) : base (toolManager)
        {
        }
    }
}

