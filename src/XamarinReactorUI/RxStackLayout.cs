using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxStackLayout
    {
        StackOrientation Orientation { get; set; }
        double Spacing { get; set; }
    }

    public sealed class RxStackLayout : RxLayout<Xamarin.Forms.StackLayout>, IRxStackLayout
    {
        public RxStackLayout(params VisualNode[] children)
            : base(children)
        { }

        public StackOrientation Orientation { get; set; } = (StackOrientation)StackLayout.OrientationProperty.DefaultValue;
        public double Spacing { get; set; } = (double)StackLayout.SpacingProperty.DefaultValue;

        protected override void OnAddChild(RxElement widget, Xamarin.Forms.Element childControl)
        {
            if (childControl is View view)
                NativeControl.Children.Add(view);
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }
            
            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(RxElement widget, Xamarin.Forms.Element childControl)
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
