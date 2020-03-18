using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxFrame
    {
        Color BorderColor { get; set; }
        bool HasShadow { get; set; }
        float CornerRadius { get; set; }
    }

    public class RxFrame : RxContentView<Frame>, IRxFrame
    {
        public RxFrame()
        {
        }

        public RxFrame(Action<Frame> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Color BorderColor { get; set; } = (Color)Frame.BorderColorProperty.DefaultValue;
        public bool HasShadow { get; set; } = (bool)Frame.HasShadowProperty.DefaultValue;
        public float CornerRadius { get; set; } = (float)Frame.CornerRadiusProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.BorderColor = BorderColor;
            NativeControl.HasShadow = HasShadow;
            NativeControl.CornerRadius = CornerRadius;

            base.OnUpdate();
        }
    }

    public static class RxFrameExtensions
    {
        public static T BorderColor<T>(this T frame, Color borderColor) where T : IRxFrame
        {
            frame.BorderColor = borderColor;
            return frame;
        }

        public static T HasShadow<T>(this T frame, bool hasShadow) where T : IRxFrame
        {
            frame.HasShadow = hasShadow;
            return frame;
        }

        public static T CornerRadius<T>(this T frame, float cornerRadius) where T : IRxFrame
        {
            frame.CornerRadius = cornerRadius;
            return frame;
        }
    }
}