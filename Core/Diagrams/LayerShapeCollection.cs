using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class LayerShapeCollection:ObservableCollection<Shape>
    {
        private Layer _layer;

        public LayerShapeCollection (Layer layer)
        {
            _layer = layer;
        }

        protected override void ClearItems ()
        {
            foreach (var item in Items)
            {
                item.Parent = null;
            }
            base.ClearItems ();
        }

        protected override void InsertItem (int index, Shape item)
        {
            base.InsertItem (index, item);
            item.Parent = _layer;
        }

        protected override void RemoveItem (int index)
        {
            this[index].Parent = null;
            base.RemoveItem (index);
        }

        protected override void SetItem (int index, Shape item)
        {
            this [index].Parent = null;
            base.SetItem (index, item);
            item.Parent = _layer;
        }
    }
}

