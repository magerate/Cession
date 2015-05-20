using System;
using CoreGraphics;

using UIKit;
using Foundation;

using Cession.Diagrams;
using Cession.Drawing;
using Cession.Projects;

namespace Cession
{
    public class DiagramView:UIView
    {
        private Project _project;

        public Project Project
        {
            get{ return _project; }
            set
            {
                if (value != _project)
                {
                    _project = value;
                    SetNeedsDisplay ();
                }
            }
        }

        public DiagramView (CGRect frame) : base (frame)
        {
            this.BackgroundColor = UIColor.White;
        }

        public override void Draw (CGRect rect)
        {
            if (null == _project)
                return;

            Layer layer = _project.SelectedLayer;
            using (CGContext context = UIGraphics.GetCurrentContext ())
            {
                var dc = new DrawingContext (context);
                dc.PushTransform (layer.Transform);
                layer.Draw (dc);
                dc.PopTransform ();
            }
        }
    }
}

