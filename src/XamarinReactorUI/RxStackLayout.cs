using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxStackLayout : IRxLayout
    {
        StackOrientation Orientation { get; set; }
        double Spacing { get; set; }
    }

    public class RxStackLayout<T> : RxLayout<T>, IRxStackLayout where T : StackLayout, new()
    {
        public RxStackLayout(params VisualNode[] children)
            : base(children)
        { }

        public RxStackLayout(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public StackOrientation Orientation { get; set; } = (StackOrientation)StackLayout.OrientationProperty.DefaultValue;
        public double Spacing { get; set; } = (double)StackLayout.SpacingProperty.DefaultValue;

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is View view)
            {
                //System.Diagnostics.Debug.WriteLine($"StackLayout ({Key ?? GetType()}) inserting {widget.Key ?? widget.GetType()} at index {widget.ChildIndex}");
                NativeControl.Children.Insert(widget.ChildIndex, view);
            }
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            NativeControl.Children.Remove((View)childControl);

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUpdate()
        {
            NativeControl.Orientation = Orientation;
            NativeControl.Spacing = Spacing;

            base.OnUpdate();
        }
    }

    public class RxStackLayout : RxStackLayout<StackLayout>
    {
        public RxStackLayout(params VisualNode[] children)
            : base(children)
        { }

        public RxStackLayout(Action<StackLayout> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxStackLayoutExtensions
    {
        public static T Orientation<T>(this T stackLayout, StackOrientation orientation) where T : IRxStackLayout
        {
            stackLayout.Orientation = orientation;
            return stackLayout;
        }

        public static T WithHorizontalOrientation<T>(this T stackLayout) where T : IRxStackLayout
        {
            stackLayout.Orientation = StackOrientation.Horizontal;
            return stackLayout;
        }

        public static T WithVerticalOrientation<T>(this T stackLayout) where T : IRxStackLayout
        {
            stackLayout.Orientation = StackOrientation.Vertical;
            return stackLayout;
        }

        public static T Spacing<T>(this T stacklayout, double spacing) where T : IRxStackLayout
        {
            stacklayout.Spacing = spacing;
            return stacklayout;
        }
    }
}