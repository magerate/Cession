using System;
using System.Collections.Generic;

using Cession.Geometries;
using Cession.Diagrams;
using Cession.Projects;

namespace Cession.Handles
{
    public class HandleManager
    {
        private List<Handle> _handles = new List<Handle>();

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

        private void SelectedLayerChanging(object sender,EventArgs e)
        {
            var p = sender as Project;
            UnregisterLayerEvents (p.SelectedLayer);
        }

        private void SelectedLayerChanged(object sender,EventArgs e)
        {
            var p = sender as Project;
            RegisterLayerEvents (p.SelectedLayer);
        }

        private void SelectionCleared(object sender,EventArgs e)
        {
            _handles.Clear();
        }

        private void ShapeSelected(object sender,ShapeSelectedEventArgs e)
        {
            var handles = e.Shape.CreateHandles ();
            _handles.AddRange (handles);
        }

        private void RegisterLayerEvents(Layer layer)
        {
            layer.SelectionClear += SelectionCleared;
            layer.ShapeSelected += ShapeSelected;
            layer.Offseted += ShapeOffseted;
        }

        private void UnregisterLayerEvents(Layer layer)
        {
            layer.SelectionClear -= SelectionCleared;
            layer.ShapeSelected -= ShapeSelected;
            layer.Offseted -= ShapeOffseted;
        }

        private void ShapeOffseted(object sender,OffsetEventArgs e)
        {
            foreach (var h in _handles)
            {
                var shape = e.OriginalSource as Shape;
                if (shape.IsAncestor (h.Shape))
                    h.Offset (e.OffsetX, e.OffsetY);
            }
        }
    }
}

