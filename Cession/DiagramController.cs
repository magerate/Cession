namespace Cession
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	using Cession.Modeling;
	using Cession.Tools;
	using Cession.UIKit;
	using Cession.Commands;

	public partial class DiagramController:UIViewController,IToolHost
	{
		private DiagramView diagramView;
		private ToolView toolView;

		private Project project;
		private ProjectInfo projectInfo;
		private CommandManager commandManager;

		private ToolManager toolManager;

		public DiagramController ()
		{
		}

		public void SetProject(Project project,ProjectInfo projectInfo)
		{
			this.project = project;
			this.projectInfo = projectInfo;
			commandManager = new CommandManager ();

			project.Layers [0].AddHandler (Room.DoorRemovedEvent, 
				new EventHandler<DoorRemovedEventArgs> (OnDoorRemoved));

			commandManager.Committed += CommandCommited;
			commandManager.CanUndoChanged += CanUndoChanged;
			commandManager.CanRedoChanged += CanRedoChanged;
		}

		private void OnDoorRemoved(object sender,DoorRemovedEventArgs e){
			if (e.IsSideEffect) {
				var room = e.OriginalSource as Room;
				var command = new OneArgumentCommand<Door> (e.Door, room.RemoveDoor, room.AddDoor);
				commandManager.Queue (command);
			}
		}

		private void CommandCommited(object sender,EventArgs e)
		{
			diagramView.SetNeedsDisplay();
			toolView.SetNeedsDisplay ();
		}

		private void CanUndoChanged(object sender,EventArgs e)
		{
			undoButton.Enabled = CommandManager.CanUndo;
		}

		private void CanRedoChanged(object sender,EventArgs e)
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

			toolManager = new ToolManager ();
			toolManager.Host = this;

			toolView.ToolManager = toolManager;
		}

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
			get{
				return commandManager;
			}
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
			toolView.SetNeedsDisplay ();
			diagramView.SetNeedsDisplay ();
		}

		private void Undo()
		{
			CommandManager.Undo ();
		}

		private void Redo()
		{
			CommandManager.Redo ();
		}
	}
}

