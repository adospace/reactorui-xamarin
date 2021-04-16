using System;
using Xamarin.Forms;
using XamarinReactorUI.Internals;

namespace XamarinReactorUI
{
    public interface IRxView : IRxVisualElement
    {
        LayoutOptions HorizontalOptions { get; set; }
        LayoutOptions VerticalOptions { get; set; }
        Thickness Margin { get; set; }

        Action TapAction { get; set; }
        Action DoubleTapAction { get; set; }

        Action<PinchGestureUpdatedEventArgs> PinchAction { get; set; }
        Action<PanUpdatedEventArgs> PanAction { get; set; }
        Action<SwipedEventArgs> SwipeAction { get; set; }
    }

    public abstract class RxView<T> : RxVisualElement<T>, IRxView where T : Xamarin.Forms.View, new()
    {
        private TapGestureRecognizer _tapGestureRecognizer;
        private TapGestureRecognizer _doubleTapGestureRecognizer;
        private PinchGestureRecognizer _pinchGestureRecognizer;
        private PanGestureRecognizer _panGestureRecognizer;
        private SwipeGestureRecognizer _swipeGestureRecognizer;

        protected RxView()
        {
        }

        protected RxView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public LayoutOptions HorizontalOptions { get; set; } = (LayoutOptions)View.HorizontalOptionsProperty.DefaultValue;

        public LayoutOptions VerticalOptions { get; set; } = (LayoutOptions)View.VerticalOptionsProperty.DefaultValue;

        public Thickness Margin { get; set; } = (Thickness)View.MarginProperty.DefaultValue;

        public Action TapAction { get; set; }
        public Action DoubleTapAction { get; set; }
        public Action<PinchGestureUpdatedEventArgs> PinchAction { get; set; }
        public Action<PanUpdatedEventArgs> PanAction { get; set; }
        public Action<SwipedEventArgs> SwipeAction { get; set; }

        protected override void OnUpdate()
        {
            if (NativeControl.VerticalOptions.Alignment != VerticalOptions.Alignment || NativeControl.VerticalOptions.Expands != VerticalOptions.Expands) NativeControl.VerticalOptions = VerticalOptions;
            if (NativeControl.HorizontalOptions.Alignment != HorizontalOptions.Alignment || NativeControl.HorizontalOptions.Expands != HorizontalOptions.Expands) NativeControl.HorizontalOptions = HorizontalOptions;
            if (NativeControl.Margin != Margin) NativeControl.Margin = Margin;

            AttachEvents();

            base.OnUpdate();
        }

        private void AttachEvents()
        {
            if (TapAction != null)
            {
                NativeControl.GestureRecognizers.Add(_tapGestureRecognizer = new TapGestureRecognizer()
                {
                    Command = new ActionCommand(TapAction)
                });
            }

            if (DoubleTapAction != null)
            {
                NativeControl.GestureRecognizers.Add(_doubleTapGestureRecognizer = new TapGestureRecognizer()
                {
                    Command = new ActionCommand(DoubleTapAction),
                    NumberOfTapsRequired = 2
                });
            }

            if (PinchAction != null)
            {
                NativeControl.GestureRecognizers.Add(_pinchGestureRecognizer = new PinchGestureRecognizer());
                _pinchGestureRecognizer.PinchUpdated += PinchGestureRecognizer_PinchUpdated;
            }

            if (PanAction != null)
            {
                NativeControl.GestureRecognizers.Add(_panGestureRecognizer = new PanGestureRecognizer());
                _panGestureRecognizer.PanUpdated += PanGestureRecognizer_PanUpdated;
            }

            if (SwipeAction != null)
            {
                NativeControl.GestureRecognizers.Add(_swipeGestureRecognizer = new SwipeGestureRecognizer());
                _swipeGestureRecognizer.Swiped += SwipeGestureRecognizer_Swiped;
            }
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            PanAction?.Invoke(e);
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            SwipeAction?.Invoke(e);
        }

        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            PinchAction?.Invoke(e);
        }

        private void DetachEvents()
        {
            if (NativeControl != null)
            {
                if (_tapGestureRecognizer != null)
                    NativeControl.GestureRecognizers.Remove(_tapGestureRecognizer);
                if (_doubleTapGestureRecognizer != null)
                    NativeControl.GestureRecognizers.Remove(_doubleTapGestureRecognizer);
                if (_pinchGestureRecognizer != null)
                    NativeControl.GestureRecognizers.Remove(_pinchGestureRecognizer);
                if (_panGestureRecognizer != null)
                    NativeControl.GestureRecognizers.Remove(_panGestureRecognizer);
                if (_swipeGestureRecognizer != null)
                    NativeControl.GestureRecognizers.Remove(_swipeGestureRecognizer);
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            DetachEvents();

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            DetachEvents();

            base.OnUnmount();
        }
    }

    public static class RxViewExtensions
    {
        public static T OnTap<T>(this T view, Action action) where T : IRxView
        {
            view.TapAction = action;
            return view;
        }

        public static T OnDoubleTap<T>(this T view, Action action) where T : IRxView
        {
            view.DoubleTapAction = action;
            return view;
        }

        public static T OnPinch<T>(this T view, Action<PinchGestureUpdatedEventArgs> action) where T : IRxView
        {
            view.PinchAction = action;
            return view;
        }

        public static T OnPan<T>(this T view, Action<PanUpdatedEventArgs> action) where T : IRxView
        {
            view.PanAction = action;
            return view;
        }

        public static T OnSwipe<T>(this T view, Action<SwipedEventArgs> action) where T : IRxView
        {
            view.SwipeAction = action;
            return view;
        }

        public static T Margin<T>(this T view, Thickness margin) where T : IRxView
        {
            view.Margin = margin;
            return view;
        }

        public static T Margin<T>(this T view, double left, double top, double right, double bottom) where T : IRxView
        {
            view.Margin = new Thickness(left, top, right, bottom);
            return view;
        }

        public static T Margin<T>(this T view, double uniform) where T : IRxView
        {
            view.Margin = new Thickness(uniform);
            return view;
        }

        public static T Margin<T>(this T view, double leftRight, double topBottom) where T : IRxView
        {
            view.Margin = new Thickness(leftRight, topBottom);
            return view;
        }

        public static T HorizontalOptions<T>(this T view, LayoutOptions layoutOptions) where T : IRxView
        {
            view.HorizontalOptions = layoutOptions;
            return view;
        }

        public static T HStart<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.Start;
            return view;
        }

        public static T HCenter<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.Center;
            return view;
        }

        public static T HEnd<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.End;
            return view;
        }

        public static T HFill<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.Fill;
            return view;
        }

        public static T HStartAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.StartAndExpand;
            return view;
        }

        public static T HCenterAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.CenterAndExpand;
            return view;
        }

        public static T HEndAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.EndAndExpand;
            return view;
        }

        public static T HFillAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.FillAndExpand;
            return view;
        }

        public static T VerticalOptions<T>(this T view, LayoutOptions layoutOptions) where T : IRxView
        {
            view.VerticalOptions = layoutOptions;
            return view;
        }

        public static T VStart<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.Start;
            return view;
        }

        public static T VCenter<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.Center;
            return view;
        }

        public static T VEnd<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.End;
            return view;
        }

        public static T VFill<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.Fill;
            return view;
        }

        public static T VStartAndExpand<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.StartAndExpand;
            return view;
        }

        public static T VCenterAndExpand<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.CenterAndExpand;
            return view;
        }

        public static T VEndAndExpand<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.EndAndExpand;
            return view;
        }

        public static T VFillAndExpand<T>(this T view) where T : IRxView
        {
            view.VerticalOptions = LayoutOptions.FillAndExpand;
            return view;
        }
    }
}