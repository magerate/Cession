using System;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

using Cession.Modeling;
using Cession.Drawing;

namespace Cession
{
	public class DiagramListController:UITableViewController
	{
		private static readonly NSString reuseId = new NSString("reuseId");

		private List<ProjectInfo> projects = new List<ProjectInfo>();

		private Dictionary<ProjectInfo,Project> projectDictionary = new Dictionary<ProjectInfo, Project>();

		public DiagramListController ()
		{
			this.Title = "Diagrams";
		}


		public override int RowsInSection (UITableView tableview, int section)
		{
			return projects.Count;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), reuseId);

			var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			addButton.Clicked += delegate {
				AddNew();
			};
			NavigationItem.RightBarButtonItem = addButton;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (reuseId);
			cell.TextLabel.Text = projects [indexPath.Row].Name;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var projectInfo = projects [indexPath.Row];
			var project = projectDictionary [projectInfo];

			var controller = new DiagramController ();
			controller.SetProject (project, projectInfo);

			var nav = new UINavigationController (controller);
			PresentViewController (nav, true, null);
		}

		private void AddNew()
		{
			var projectInfo = new ProjectInfo ();
			projectInfo.Name = "Untitled diagram";
			projects.Insert (0, projectInfo);

			var project = Project.Create ("Layer",LayerDrawing.GetLayerDefaultTransform());

			projectDictionary.Add (projectInfo, project);

			TableView.InsertRows (new NSIndexPath[]{ NSIndexPath.FromRowSection (0, 0) }, UITableViewRowAnimation.Automatic);
		}
	}
}

