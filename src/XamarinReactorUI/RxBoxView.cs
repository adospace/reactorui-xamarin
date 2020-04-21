using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxBoxView : IRxView
    {
        Color Color { get; set; }
        CornerRadius CornerRadius { get; set; }
    }

    public class RxBoxView<T> : RxView<T>, IRxBoxView where T : BoxView, new()
    {
        public RxBoxView()
        {
        }

        public RxBoxView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Color Color { get; set; } = (Color)BoxView.ColorProperty.DefaultValue;
        public CornerRadius CornerRadius { get; set; } = (CornerRadius)BoxView.CornerRadiusProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Color = Color;
            NativeControl.CornerRadius = CornerRadius;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxBoxView : RxBoxView<BoxView>
    {
        public RxBoxView()
        {
        }

        public RxBoxView(Action<BoxView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxBoxViewExtensions
    {
        public static T Color<T>(this T boxview, Color color) where T : IRxBoxView
        {
            boxview.Color = color;
            return boxview;
        }

        public static T CornerRadius<T>(this T boxview, CornerRadius cornerRadius) where T : IRxBoxView
        {
            boxview.CornerRadius = cornerRadius;
            return boxview;
        }
    }
}