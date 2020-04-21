using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;

namespace XamarinReactorUI
{
    public interface IRxSKGLView
    {
        bool HasRenderLoop { get; set; }
        bool EnableTouchEvents { get; set; }
 
        Action<SKPaintGLSurfaceEventArgs> PaintSurfaceAction { get; set; }
    }

    public class RxSKGLView<T> : RxView<T>, IRxSKGLView where T : SKGLView, new()
    {
        public RxSKGLView()
        {
        }

        public RxSKGLView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public bool HasRenderLoop { get; set; } = (bool)SKGLView.HasRenderLoopProperty.DefaultValue;
        public bool EnableTouchEvents { get; set; } = (bool)SKGLView.EnableTouchEventsProperty.DefaultValue;
        public Action<SKPaintGLSurfaceEventArgs> PaintSurfaceAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.HasRenderLoop = HasRenderLoop;
            NativeControl.EnableTouchEvents = EnableTouchEvents;

            if (PaintSurfaceAction != null)
                NativeControl.PaintSurface += NativeControl_PaintSurface;

            base.OnUpdate();
        }

        private void NativeControl_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            PaintSurfaceAction?.Invoke(e);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.PaintSurface -= NativeControl_PaintSurface;

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
                NativeControl.PaintSurface -= NativeControl_PaintSurface;

            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxSKGLView : RxSKGLView<SKGLView>
    {
        public RxSKGLView()
        {
        }

        public RxSKGLView(Action<SKGLView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxSKGLViewExtensions
    {
        public static T OnPaintSurface<T>(this T skglview, Action<SKPaintGLSurfaceEventArgs> action) where T : IRxSKGLView
        {
            skglview.PaintSurfaceAction = action;
            return skglview;
        }

        public static T HasRenderLoop<T>(this T skglview, bool hasRenderLoop) where T : IRxSKGLView
        {
            skglview.HasRenderLoop = hasRenderLoop;
            return skglview;
        }

        public static T EnableTouchEvents<T>(this T skglview, bool enableTouchEvents) where T : IRxSKGLView
        {
            skglview.EnableTouchEvents = enableTouchEvents;
            return skglview;
        }
    }
}