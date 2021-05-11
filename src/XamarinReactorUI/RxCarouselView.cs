using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxCarouselView : IRxItemsView
    {
        Thickness PeekAreaInsets { get; set; }
        bool IsBounceEnabled { get; set; }
        bool IsSwipeEnabled { get; set; }
        bool IsScrollAnimated { get; set; }
        object CurrentItem { get; set; }
        Action<CurrentItemChangedEventArgs> CurrentItemChangedAction { get; set; }
        int Position { get; set; }
        Action<PositionChangedEventArgs> PositionChangedAction { get; set; }
        LinearItemsLayout ItemsLayout { get; set; }
        bool Loop { get; set; }
    }

    public class RxCarouselView<T, I> : RxItemsView<T, I>, IRxCarouselView where T : Xamarin.Forms.CarouselView, new()
    {
        public RxCarouselView()
        {
        }

        public RxCarouselView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Thickness PeekAreaInsets { get; set; } = (Thickness)CarouselView.PeekAreaInsetsProperty.DefaultValue;
        public bool IsBounceEnabled { get; set; } = (bool)CarouselView.IsBounceEnabledProperty.DefaultValue;
        public bool IsSwipeEnabled { get; set; } = (bool)CarouselView.IsSwipeEnabledProperty.DefaultValue;
        public bool IsScrollAnimated { get; set; } = (bool)CarouselView.IsScrollAnimatedProperty.DefaultValue;
        public object CurrentItem { get; set; } = (object)CarouselView.CurrentItemProperty.DefaultValue;
        public int Position { get; set; } = (int)CarouselView.PositionProperty.DefaultValue;
        public LinearItemsLayout ItemsLayout { get; set; } = (LinearItemsLayout)CarouselView.ItemsLayoutProperty.DefaultValue;
        public Action<CurrentItemChangedEventArgs> CurrentItemChangedAction { get; set; }
        public Action<PositionChangedEventArgs> PositionChangedAction { get; set; }
        public bool Loop { get; set; } = (bool)CarouselView.LoopProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.PeekAreaInsets = PeekAreaInsets;
            NativeControl.IsBounceEnabled = IsBounceEnabled;
            NativeControl.IsSwipeEnabled = IsSwipeEnabled;
            NativeControl.IsScrollAnimated = IsScrollAnimated;
            NativeControl.CurrentItem = CurrentItem;
            NativeControl.Position = Position;
            NativeControl.ItemsLayout = ItemsLayout;
            NativeControl.Loop = Loop;

            if (CurrentItemChangedAction != null)
                NativeControl.CurrentItemChanged += NativeControl_CurrentItemChanged;
            if (PositionChangedAction != null)
                NativeControl.PositionChanged += NativeControl_PositionChanged;

            base.OnUpdate();
        }

        private void NativeControl_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            PositionChangedAction?.Invoke(e);
        }

        private void NativeControl_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            CurrentItemChangedAction?.Invoke(e);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.CurrentItemChanged -= NativeControl_CurrentItemChanged;
                NativeControl.PositionChanged -= NativeControl_PositionChanged;
            }

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
            {
                NativeControl.CurrentItemChanged -= NativeControl_CurrentItemChanged;
                NativeControl.PositionChanged -= NativeControl_PositionChanged;
            }

            base.OnUnmount();
        }
    }

    public class RxCarouselView<I> : RxCarouselView<CarouselView, I>
    {
        public RxCarouselView()
        {
        }

        public RxCarouselView(Action<CarouselView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxCarouselViewExtensions
    {
        public static T OnCurrentItemChanged<T>(this T carouselview, Action<CurrentItemChangedEventArgs> action) where T : IRxCarouselView
        {
            carouselview.CurrentItemChangedAction = action;
            return carouselview;
        }

        public static T OnPositionChanged<T>(this T carouselview, Action<PositionChangedEventArgs> action) where T : IRxCarouselView
        {
            carouselview.PositionChangedAction = action;
            return carouselview;
        }

        public static T PeekAreaInsets<T>(this T carouselview, Thickness peekAreaInsets) where T : IRxCarouselView
        {
            carouselview.PeekAreaInsets = peekAreaInsets;
            return carouselview;
        }

        public static T PeekAreaInsets<T>(this T carouselview, double leftRight, double topBottom) where T : IRxCarouselView
        {
            carouselview.PeekAreaInsets = new Thickness(leftRight, topBottom);
            return carouselview;
        }

        public static T PeekAreaInsets<T>(this T carouselview, double uniformSize) where T : IRxCarouselView
        {
            carouselview.PeekAreaInsets = new Thickness(uniformSize);
            return carouselview;
        }

        public static T IsBounceEnabled<T>(this T carouselview, bool isBounceEnabled) where T : IRxCarouselView
        {
            carouselview.IsBounceEnabled = isBounceEnabled;
            return carouselview;
        }

        public static T IsSwipeEnabled<T>(this T carouselview, bool isSwipeEnabled) where T : IRxCarouselView
        {
            carouselview.IsSwipeEnabled = isSwipeEnabled;
            return carouselview;
        }

        public static T IsScrollAnimated<T>(this T carouselview, bool isScrollAnimated) where T : IRxCarouselView
        {
            carouselview.IsScrollAnimated = isScrollAnimated;
            return carouselview;
        }

        public static T CurrentItem<T>(this T carouselview, object currentItem) where T : IRxCarouselView
        {
            carouselview.CurrentItem = currentItem;
            return carouselview;
        }

        public static T Position<T>(this T carouselview, int position) where T : IRxCarouselView
        {
            carouselview.Position = position;
            return carouselview;
        }

        public static T ItemsLayout<T>(this T carouselview, LinearItemsLayout itemsLayout) where T : IRxCarouselView
        {
            carouselview.ItemsLayout = itemsLayout;
            return carouselview;
        }

        public static T Loop<T>(this T carouselview, bool loop) where T : IRxCarouselView
        {
            carouselview.Loop = loop;
            return carouselview;
        }
    }
}