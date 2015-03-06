using System;
using System.Collections.Generic;

using Cession.Handles;
using Cession.Geometries;
using Cession.Diagrams;

using CoreGraphics;
using UIKit;

namespace Cession.Drawing.Handles
{
    public static class HandleDrawing
    {
        private static Dictionary<Type,Action<Handle,DrawingContext>> s_drawers = new Dictionary<Type, Action<Handle, DrawingContext>>();

        static HandleDrawing()
        {
            s_drawers [typeof(VertexHandle)] = DrawVertexHandle;
            s_drawers [typeof(LineHandle)] = DrawLineHandle;
        }

        public static void Draw(this Handle handle,DrawingContext drawingContext)
        {
            if (null == handle)
                throw new ArgumentNullException ();
            if (null == drawingContext)
                throw new ArgumentNullException ();

            Action<Handle,DrawingContext> drawAction;
            if (!s_drawers.TryGetValue (handle.GetType (), out drawAction))
            {
                throw new InvalidOperationException ();
            }
            drawAction (handle, drawingContext);
        }

        private static void DrawVertexHandle(Handle handle,DrawingContext drawingContext)
        {
            var vh = handle as VertexHandle;
            CGPoint point = drawingContext.Transform.Transform(vh.Location).ToCGPoint();
            var rect = new CGRect (point.X - (nfloat)VertexHandle.Size / 2, 
                point.Y - (nfloat)VertexHandle.Size / 2, 
                (nfloat)VertexHandle.Size, 
                (nfloat)VertexHandle.Size);

            CGContext context = drawingContext.CGContext;
            context.SaveState ();
            UIColor.Blue.SetFill ();
            drawingContext.CGContext.FillRect (rect);
            context.RestoreState ();
        }

        private static void DrawLineHandle(Handle handle,DrawingContext drawingContext)
        {
            var lineHandle = handle as LineHandle;
            CGPoint point = drawingContext.Transform.Transform(lineHandle.Location).ToCGPoint();
            var rect = new CGRect (- (nfloat)VertexHandle.Size / 2, 
                - (nfloat)VertexHandle.Size / 2, 
                (nfloat)LineHandle.Size, 
                (nfloat)LineHandle.Size);

            LineSegment line = handle.Shape as LineSegment;
            nfloat angle = (nfloat)(line.Angle);

            CGContext context = drawingContext.CGContext;
            context.SaveState ();
            context.TranslateCTM (point.X, point.Y);
            context.RotateCTM(angle);
            UIColor.Blue.SetFill ();
            context.FillRect (rect);
            context.RestoreState ();
        }
    }
}

