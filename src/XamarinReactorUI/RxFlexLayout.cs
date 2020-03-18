using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxFlexLayout
    {
        FlexDirection Direction { get; set; }
        FlexJustify JustifyContent { get; set; }
        FlexAlignContent AlignContent { get; set; }
        FlexAlignItems AlignItems { get; set; }
        FlexPosition Position { get; set; }
        FlexWrap Wrap { get; set; }
    }

    public class RxFlexLayout : RxLayout<Xamarin.Forms.FlexLayout>, IRxFlexLayout
    {
        public RxFlexLayout()
        {
        }

        public RxFlexLayout(Action<FlexLayout> componentRefAction)
            : base(componentRefAction)
        {
        }

        public FlexDirection Direction { get; set; } = (FlexDirection)FlexLayout.DirectionProperty.DefaultValue;
        public FlexJustify JustifyContent { get; set; } = (FlexJustify)FlexLayout.JustifyContentProperty.DefaultValue;
        public FlexAlignContent AlignContent { get; set; } = (FlexAlignContent)FlexLayout.AlignContentProperty.DefaultValue;
        public FlexAlignItems AlignItems { get; set; } = (FlexAlignItems)FlexLayout.AlignItemsProperty.DefaultValue;
        public FlexPosition Position { get; set; } = (FlexPosition)FlexLayout.PositionProperty.DefaultValue;
        public FlexWrap Wrap { get; set; } = (FlexWrap)FlexLayout.WrapProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Direction = Direction;
            NativeControl.JustifyContent = JustifyContent;
            NativeControl.AlignContent = AlignContent;
            NativeControl.AlignItems = AlignItems;
            NativeControl.Position = Position;
            NativeControl.Wrap = Wrap;

            base.OnUpdate();
        }

        protected override void OnAddChild(VisualNode widget, Element childControl)
        {
            if (childControl is View view)
            {
                //System.Diagnostics.Debug.WriteLine($"FlexLayout ({Key ?? GetType()}) inserting {widget.Key ?? widget.GetType()} at index {widget.ChildIndex}");
                NativeControl.Children.Insert(widget.ChildIndex, view);
            }
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, Xamarin.Forms.Element childControl)
        {
            NativeControl.Children.Remove((View)childControl);

            base.OnRemoveChild(widget, childControl);
        }

    }

    public static class RxFlexLayoutExtensions
    {
        public static T Direction<T>(this T flexlayout, FlexDirection direction) where T : IRxFlexLayout
        {
            flexlayout.Direction = direction;
            return flexlayout;
        }

        public static T JustifyContent<T>(this T flexlayout, FlexJustify justifyContent) where T : IRxFlexLayout
        {
            flexlayout.JustifyContent = justifyContent;
            return flexlayout;
        }

        public static T AlignContent<T>(this T flexlayout, FlexAlignContent alignContent) where T : IRxFlexLayout
        {
            flexlayout.AlignContent = alignContent;
            return flexlayout;
        }

        public static T AlignItems<T>(this T flexlayout, FlexAlignItems alignItems) where T : IRxFlexLayout
        {
            flexlayout.AlignItems = alignItems;
            return flexlayout;
        }

        public static T Position<T>(this T flexlayout, FlexPosition position) where T : IRxFlexLayout
        {
            flexlayout.Position = position;
            return flexlayout;
        }

        public static T Wrap<T>(this T flexlayout, FlexWrap wrap) where T : IRxFlexLayout
        {
            flexlayout.Wrap = wrap;
            return flexlayout;
        }
    }
}