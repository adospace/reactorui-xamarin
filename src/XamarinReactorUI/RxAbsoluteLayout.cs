using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxAbsoluteLayout : IRxLayout
    {
    }

    public class RxAbsoluteLayout<T> : RxLayout<T>, IRxAbsoluteLayout where T : AbsoluteLayout, new()
    {
        public RxAbsoluteLayout()
        {
        }

        public RxAbsoluteLayout(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

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
    }

    public class RxAbsoluteLayout : RxAbsoluteLayout<AbsoluteLayout>
    {
        public RxAbsoluteLayout()
        {
        }

        public RxAbsoluteLayout(Action<AbsoluteLayout> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxAbsoluteLayoutExtensions
    {
        public static T LayoutBounds<T>(this T element, Rectangle bounds) where T : IRxElement
        {
            if (element == null)
                return element;

            element.SetAttachedProperty(AbsoluteLayout.LayoutBoundsProperty, bounds);

            return element;
        }

        public static T LayoutBounds<T>(this T element, Point loc, Size size) where T : IRxElement
        {
            if (element == null)
                return element;

            element.SetAttachedProperty(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(loc, size));

            return element;
        }

        public static T LayoutBounds<T>(this T element, double x, double y, double width, double height) where T : IRxElement
        {
            if (element == null)
                return element;

            element.SetAttachedProperty(AbsoluteLayout.LayoutBoundsProperty, new Rectangle(x, y, width, height));

            return element;
        }

        public static T LayoutFlags<T>(this T element, AbsoluteLayoutFlags flags) where T : IRxElement
        {
            if (element == null)
                return element;

            element.SetAttachedProperty(AbsoluteLayout.LayoutFlagsProperty, flags);

            return element;
        }
    }
}