using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxScrollView : IRxLayout
    {
        ScrollOrientation Orientation { get; set; }
        ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
        ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
    }

    public class RxScrollView<T> : RxLayout<T>, IRxScrollView where T : ScrollView, new()
    {
        public RxScrollView()
        {
        }

        public RxScrollView(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public ScrollOrientation Orientation { get; set; } = (ScrollOrientation)ScrollView.OrientationProperty.DefaultValue;
        public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; } = (ScrollBarVisibility)ScrollView.HorizontalScrollBarVisibilityProperty.DefaultValue;
        public ScrollBarVisibility VerticalScrollBarVisibility { get; set; } = (ScrollBarVisibility)ScrollView.VerticalScrollBarVisibilityProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Orientation = Orientation;
            NativeControl.HorizontalScrollBarVisibility = HorizontalScrollBarVisibility;
            NativeControl.VerticalScrollBarVisibility = VerticalScrollBarVisibility;

            base.OnUpdate();
        }

        protected override void OnAddChild(VisualNode widget, Xamarin.Forms.Element childControl)
        {
            if (childControl is View view)
                NativeControl.Content = view;
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, Xamarin.Forms.Element childControl)
        {
            NativeControl.Content = null;

            base.OnRemoveChild(widget, childControl);
        }
    }

    public class RxScrollView : RxScrollView<ScrollView>
    {
        public RxScrollView()
        {
        }

        public RxScrollView(Action<ScrollView> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static class RxScrollViewExtensions
    {
        public static T Orientation<T>(this T scrollview, ScrollOrientation orientation) where T : IRxScrollView
        {
            scrollview.Orientation = orientation;
            return scrollview;
        }

        public static T HorizontalScrollBarVisibility<T>(this T scrollview, ScrollBarVisibility horizontalScrollBarVisibility) where T : IRxScrollView
        {
            scrollview.HorizontalScrollBarVisibility = horizontalScrollBarVisibility;
            return scrollview;
        }

        public static T VerticalScrollBarVisibility<T>(this T scrollview, ScrollBarVisibility verticalScrollBarVisibility) where T : IRxScrollView
        {
            scrollview.VerticalScrollBarVisibility = verticalScrollBarVisibility;
            return scrollview;
        }
    }
}