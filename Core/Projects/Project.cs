using System;

using Cession.Geometries;
using Cession.Diagrams;

namespace Cession.Projects
{
    public class Project
    {
        private int _selectedLayerIndex;

        public LayerCollection Layers{ get; private set; }

        public event EventHandler<EventArgs> SelectedLayerChanging;
        public event EventHandler<EventArgs> SelectedLayerChanged;

        public int SelectedLayerIndex
        {
            get{ return _selectedLayerIndex; }
            set
            {
                if (value < 0 || value >= Layers.Count)
                    throw new ArgumentOutOfRangeException ();
                if (value != _selectedLayerIndex)
                {
                    if (null != SelectedLayerChanging)
                        SelectedLayerChanging (this, EventArgs.Empty);
                    _selectedLayerIndex = value;
                    if (null != SelectedLayerChanged)
                        SelectedLayerChanged (this, EventArgs.Empty);
                }
            }
        }

        public Layer SelectedLayer
        {
            get
            {
                if (_selectedLayerIndex >= 0 && _selectedLayerIndex < Layers.Count)
                    return Layers [_selectedLayerIndex];
                return null;
            }
        }

        public Project ()
        {
            Layers = new LayerCollection ();
        }

        public static Project Create ()
        {
            var project = new Project ();
            var layer = new Layer ("Layer");
            project.Layers.Add (layer);
            project.SelectedLayerIndex = 0;
            return project;
        }

//        public static Project Create (string name, Matrix transform)
//        {
//            var project = new Project ();
//            var layer = new Layer (name);
//            layer.Transform = transform;
//            project.Layers.Add (layer);
//            project.Layers.SelectedIndex = 0;
//            return project;
//        }
    }
}

