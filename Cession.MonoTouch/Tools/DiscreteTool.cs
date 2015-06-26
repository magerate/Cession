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
            InitializeNavigationItem ();
        }

        protected virtual void Exit()
        {
        }

        protected virtual void InitializeNavigationItem()
        {
            NavigationItem = new UINavigationItem ();

            var doneButton = new UIBarButtonItem ();
            doneButton.Title = "Done";
            doneButton.Clicked += delegate
            {
                Exit();
            };

            NavigationItem.LeftBarButtonItem = doneButton;

            var undoButton = new UIBarButtonItem (UIBarButtonSystemItem.Undo);
            undoButton.Clicked += delegate
            {
            };

            var redoButton = new UIBarButtonItem (UIBarButtonSystemItem.Redo);
            redoButton.Clicked += delegate
            {

            };

            NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{
                redoButton,
                undoButton,
            };
        }
    }
}

