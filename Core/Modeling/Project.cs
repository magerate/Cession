namespace Cession.Modeling
{
	using System;
	using Cession.Geometries;

	public class Project
	{
		public LayerCollection Layers{ get; private set; }

		public Project ()
		{
			Layers = new LayerCollection ();
		}

//		public static Project Create()
//		{
//			var project = new Project ();
//			var layer = new Layer ("Layer");
//			project.Layers.Add (layer);
//			project.Layers.SelectedIndex = 0;
//			return project;
//		}

		public static Project Create(string name,Matrix transform)
		{
			var project = new Project ();
			var layer = new Layer (name);
			layer.Transform = transform;
			project.Layers.Add (layer);
			project.Layers.SelectedIndex = 0;
			return project;
		}
	}
}

