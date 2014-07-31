using System;

namespace Cession.Modeling
{
	public class Project
	{
		public LayerCollection Layers{ get; private set; }

		public Project ()
		{
			Layers = new LayerCollection ();
		}

		public static Project Create()
		{
			var project = new Project ();
			var layer = new Layer ("Layer");
			project.Layers.Add (layer);
			project.Layers.SelectedIndex = 0;
			return project;
		}
	}
}

