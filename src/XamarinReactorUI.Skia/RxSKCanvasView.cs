using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;

namespace XamarinReactorUI
{
    public interface IRxSKCanvasView
    {
        bool IgnorePixelScaling { get; set; }
        bool EnableTouchEvents { get; set; }

        Action<SKPaintSurfaceEventArgs> PaintSurfaceAction { get; set; }
        Action<SKTouchEventArgs> TouchAction { get; set; }
    }

    public class RxSKCanvasView<T> : RxView<T>, IRxSKCanvasView where T : SKCanvasView, new()
    {
        public RxSKCanvasView()
        {
        }

        public RxSKCanvasView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public bool IgnorePixelScaling { get; set; } = (bool)SKCanvasView.IgnorePixelScalingProperty.DefaultValue;
        public bool EnableTouchEvents { get; set; } = (bool)SKCanvasView.EnableTouchEventsProperty.DefaultValue;
        public Action<SKPaintSurfaceEventArgs> PaintSurfaceAction { get; set; }
        public Action<SKTouchEventArgs> TouchAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.IgnorePixelScaling = IgnorePixelScaling;
            NativeControl.EnableTouchEvents = EnableTouchEvents;

            if (PaintSurfaceAction != null)
                NativeControl.PaintSurface += NativeControl_PaintSurface;
            if (TouchAction != null)
                NativeControl.Touch += NativeControl_Touch;

            base.OnUpdate();
        }

        private void NativeControl_Touch(object sender, SKTouchEventArgs e)
        {
            TouchAction?.Invoke(e);
        }

        private void NativeControl_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            PaintSurfaceAction?.Invoke(e);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.PaintSurface -= NativeControl_PaintSurface;
                NativeControl.Touch -= NativeControl_Touch;
            }

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
            {
                NativeControl.PaintSurface -= NativeControl_PaintSurface;
                NativeControl.Touch -= NativeControl_Touch;
            }

            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxSKCanvasView : RxSKCanvasView<SKCanvasView>
    {
        public RxSKCanvasView()
        {
        }

        public RxSKCanvasView(Action<SKCanvasView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxSKCanvasViewExtensions
    {
        public static T OnPaintSurface<T>(this T skcanvasview, Action<SKPaintSurfaceEventArgs> action) where T : IRxSKCanvasView
        {
            skcanvasview.PaintSurfaceAction = action;
            return skcanvasview;
        }

        public static T OnTouch<T>(this T skcanvasview, Action<SKTouchEventArgs> action) where T : IRxSKCanvasView
        {
            skcanvasview.TouchAction = action;
            return skcanvasview;
        }

        public static T IgnorePixelScaling<T>(this T skcanvasview, bool ignorePixelScaling) where T : IRxSKCanvasView
        {
            skcanvasview.IgnorePixelScaling = ignorePixelScaling;
            return skcanvasview;
        }

        public static T EnableTouchEvents<T>(this T skcanvasview, bool enableTouchEvents) where T : IRxSKCanvasView
        {
            skcanvasview.EnableTouchEvents = enableTouchEvents;
            return skcanvasview;
        }
    }
}