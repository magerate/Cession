using System;
using System.Collections.ObjectModel;

namespace Cession.Modeling
{
	public class LayerCollection:Collection<Layer>
	{
		public int SelectedIndex{ get; set; }

		public Layer SelectedLayer
		{
			get{
				if (SelectedIndex >= 0 && SelectedIndex < Count)
					return this [SelectedIndex];
				return null;
			}
		}

		public LayerCollection ()
		{
			SelectedIndex = -1;
		}
	}
}

