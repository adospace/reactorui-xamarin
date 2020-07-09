using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxRelativeLayout : IRxLayout
    {
    }

    public class RxRelativeLayout<T> : RxLayout<T>, IRxRelativeLayout where T : RelativeLayout, new()
    {
        public RxRelativeLayout()
        {
        }

        public RxRelativeLayout(Action<T> componentRefAction)
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

    public class RxRelativeLayout : RxRelativeLayout<RelativeLayout>
    {
        public RxRelativeLayout()
        {
        }

        public RxRelativeLayout(Action<RelativeLayout> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxRelativeLayoutExtensions
    {
        public static T XConstraint<T>(this T element, Constraint constraint) where T : IRxElement
        {
            if (element == null)
                return element;

            element.SetAttachedProperty(RelativeLayout.XConstraintProperty, constraint);

            return element;
        }

        public static T YConstraint<T>(this T element, Constraint constraint) where T : IRxElement
        {
            if (element == null)
                return element;

            element.SetAttachedProperty(RelativeLayout.YConstraintProperty, constraint);

            return element;
        }

        public static T WidthConstraint<T>(this T element, Constraint constraint) where T : IRxElement
        {
            if (element == null)
                return element;

            element.SetAttachedProperty(RelativeLayout.WidthConstraintProperty, constraint);

            return element;
        }

        public static T HeightConstraint<T>(this T element, Constraint constraint) where T : IRxElement
        {
            if (element == null)
                return element;

            element.SetAttachedProperty(RelativeLayout.HeightConstraintProperty, constraint);

            return element;
        }

        public static T BoundsConstraint<T>(this T element, Constraint constraint) where T : IRxElement
        {
            if (element == null)
                return element;

            element.SetAttachedProperty(RelativeLayout.BoundsConstraintProperty, constraint);

            return element;
        }
    }
}