using System;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

using Cession.Geometries;
using Cession.Diagrams;
using Cession.Projects;
using D = Cession.Diagrams;

namespace Cession.Handles
{
    public class HandleManager
    {
        private List<Handle> _handles = new List<Handle> ();

        public IReadOnlyList<Handle> Handles
        {
            get{ return _handles; }
        }

        public HandleManager ()
        {
        }

        public void AttachProject (Project project)
        {
            RegisterLayerEvents (project.SelectedLayer);
            project.SelectedLayerChanging += SelectedLayerChanging;
            project.SelectedLayerChanged += SelectedLayerChanged;
        }

        public void DetachProject (Project project)
        {
            UnregisterLayerEvents (project.SelectedLayer);
            project.SelectedLayerChanging -= SelectedLayerChanging;
            project.SelectedLayerChanged -= SelectedLayerChanged;
            _handles.Clear ();
        }

        private void SelectedLayerChanging (object sender, EventArgs e)
        {
            var p = sender as Project;
            UnregisterLayerEvents (p.SelectedLayer);
        }

        private void SelectedLayerChanged (object sender, EventArgs e)
        {
            var p = sender as Project;
            RegisterLayerEvents (p.SelectedLayer);
        }

        private void SelectionCleared (object sender, EventArgs e)
        {
            _handles.Clear ();
        }

        private void ShapeSelected (object sender, ShapeSelectedEventArgs e)
        {
            var handles = e.Shape.CreateHandles ();
            if(null != handles && handles.Length > 0)
                _handles.AddRange (handles);
        }

        private void RegisterLayerEvents (Layer layer)
        {
            layer.SelectionClear += SelectionCleared;
            layer.ShapeSelected += ShapeSelected;
            layer.Shapes.CollectionChanged += ShapeCollectionChanged;
        }

        private void UnregisterLayerEvents (Layer layer)
        {
            layer.SelectionClear -= SelectionCleared;
            layer.ShapeSelected -= ShapeSelected;
            layer.Shapes.CollectionChanged -= ShapeCollectionChanged;
        }

        private bool ContainsHandleShape(IList shapes,Shape shape)
        {
            foreach (var item in shapes)
            {
                var ss = item as Shape;
                if (ss.IsAncestor (shape))
                    return true;
            }
            return false;
        }

        private void ShapeCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Handle[] handles = _handles.Where (h => ContainsHandleShape (e.OldItems, h.Shape)).ToArray ();
                foreach (var item in handles)
                {
                    _handles.Remove (item);
                }
            }
        }
    }
}

