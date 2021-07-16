using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxActivityIndicator : IRxView
    {
        bool IsRunning { get; set; }
        Color Color { get; set; }
    }

    public class RxActivityIndicator<T> : RxView<T>, IRxActivityIndicator where T : ActivityIndicator, new()
    {
        public RxActivityIndicator()
        {
        }

        public RxActivityIndicator(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }

        public bool IsRunning { get; set; } = (bool)ActivityIndicator.IsRunningProperty.DefaultValue;
        public Color Color { get; set; } = (Color)ActivityIndicator.ColorProperty.DefaultValue;

        protected override void OnUpdate()
        {
            if (NativeControl.IsRunning != IsRunning) NativeControl.IsRunning = IsRunning;
            if (NativeControl.Color != Color) NativeControl.Color = Color;

            base.OnUpdate();
        }
    }

    public class RxActivityIndicator : RxActivityIndicator<ActivityIndicator>
    {
        public RxActivityIndicator()
        {
        }

        public RxActivityIndicator(Action<ActivityIndicator> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxActivityIndicatorExtensions
    {
        public static T IsRunning<T>(this T activityIndicator, bool isRunning) where T : IRxActivityIndicator
        {
            activityIndicator.IsRunning = isRunning;
            return activityIndicator;
        }

        public static T Color<T>(this T activityIndicator, Color color) where T : IRxActivityIndicator
        {
            activityIndicator.Color = color;
            return activityIndicator;
        }
    }
}