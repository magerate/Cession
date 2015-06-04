using System;

using CoreGraphics;
using UIKit;
using Foundation;

using Cession.Diagrams;
using Cession.Tools;
using Cession.UIKit;
using Cession.Commands;
using Cession.Mediators;
using Cession.Projects;
using Cession.Handles;

namespace Cession
{
    public partial class DiagramController:UIViewController,IToolHost
    {
        private DiagramView diagramView;
        private ToolView toolView;

        private Project project;
        private ProjectInfo projectInfo;
        private DiagramCommandMediator commandMediator;
        private ToolManager toolManager;
        private HandleManager handleManager;

        public DiagramController ()
        {
            handleManager = new HandleManager ();

            commandMediator = new DiagramCommandMediator ();
            commandMediator.CommandManager.Committed += CommandCommited;
            commandMediator.CommandManager.CanUndoChanged += CanUndoChanged;
            commandMediator.CommandManager.CanRedoChanged += CanRedoChanged;

            toolManager = new ToolManager ();
            toolManager.Host = this;
            toolManager.CurrentToolTypeChanged += delegate
            {
                ToolTypeChanged();
            };
        }

        public void SetProject (Project project, ProjectInfo projectInfo)
        {
            this.project = project;
            this.projectInfo = projectInfo;

            commandMediator.AttachProject (project);
            handleManager.AttachProject (project);
        }

        public override void DidMoveToParentViewController (UIViewController parent)
        {
            if (parent == null)
            {
                DetachProject (project);
            }
        }

        private void DetachProject(Project project)
        {
            commandMediator.DetachProject (project);
            handleManager.DetachProject (project);
        }

        private void CommandCommited (object sender, EventArgs e)
        {
            diagramView.SetNeedsDisplay ();
            toolView.SetNeedsDisplay ();
        }

        private void CanUndoChanged (object sender, EventArgs e)
        {
            undoButton.Enabled = CommandManager.CanUndo;
        }

        private void CanRedoChanged (object sender, EventArgs e)
        {
            redoButton.Enabled = CommandManager.CanRedo;
        }

        public override void LoadView ()
        {
            var view = new UIView (UIScreen.MainScreen.Bounds);

            diagramView = new DiagramView (UIScreen.MainScreen.Bounds);
            diagramView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
            view.AddSubview (diagramView);

            toolView = new ToolView (UIScreen.MainScreen.Bounds);
            toolView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
            view.AddSubview (toolView);

            View = view;
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            InitializeNavigationItems ();
            diagramView.Project = project;

            toolView.ToolManager = toolManager;
            toolView.HandleManager = handleManager;
        }

        private void ToolTypeChanged()
        {
            DiscreteTool tool = toolManager.CurrentTool as DiscreteTool;
            if (null != tool && tool.NavigationItem != null)
                SetToolNavigationItems (tool.NavigationItem);
            else
            {
                if (toolManager.CurrentTool.ParentTool == null)
                    SetDefaultNavigationItems ();
            }

            if (toolManager.RootTool.GetType() == typeof(SelectTool))
                toolSegment.SelectedSegment = 0;
            else
                toolSegment.SelectedSegment = 1;
        }

        #region "Tool host"
        public UIView DiagramView
        {
            get{ return diagramView; }
        }

        public UIView ToolView
        {
            get{ return toolView; }
        }

        public Project Project
        {
            get{ return project; }
        }

        public CommandManager CommandManager
        {
            get{return commandMediator.CommandManager;}
        }

        public HandleManager HandleManager
        {
            get{ return handleManager; }
        }
        #endregion

        public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
        {
            base.DidRotate (fromInterfaceOrientation);
            toolView.SetNeedsDisplay ();
            diagramView.SetNeedsDisplay ();
        }

        private void Undo ()
        {
            CommandManager.Undo ();
        }

        private void Redo ()
        {
            CommandManager.Redo ();
        }
    }
}

