using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxScrollView : IRxLayout
    {
        ScrollOrientation Orientation { get; set; }
        ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
        ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
        Action<ScrolledEventArgs> ScrolledAction { get; set; }

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
        public Action<ScrolledEventArgs> ScrolledAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.Orientation = Orientation;
            NativeControl.HorizontalScrollBarVisibility = HorizontalScrollBarVisibility;
            NativeControl.VerticalScrollBarVisibility = VerticalScrollBarVisibility;

            if (ScrolledAction != null)
                NativeControl.Scrolled += NativeControl_Scrolled;

            base.OnUpdate();
        }

        private void NativeControl_Scrolled(object sender, ScrolledEventArgs e)
        {
            ScrolledAction?.Invoke(e);
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is View view)
                NativeControl.Content = view;
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            NativeControl.Content = null;

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
                NativeControl.Scrolled -= NativeControl_Scrolled;
            base.OnUnmount();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.Scrolled += NativeControl_Scrolled;

            base.OnMigrated(newNode);
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

        public static T OnScrolled<T>(this T scrollview, Action<ScrolledEventArgs> action) where T : IRxScrollView
        {
            scrollview.ScrolledAction = action;
            return scrollview;
        }
    }
}