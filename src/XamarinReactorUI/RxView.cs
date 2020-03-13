using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxView
    {
        LayoutOptions HorizontalOptions { get; set; }
        LayoutOptions VerticalOptions { get; set; }
        Thickness Margin { get; set; }
    }

    public abstract class RxView<T> : RxVisualElement<T>, IRxView where T : Xamarin.Forms.View, new()
    {
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

        protected override void OnUpdate()
        {
            NativeControl.VerticalOptions = VerticalOptions;
            NativeControl.HorizontalOptions = HorizontalOptions;
            NativeControl.Margin = Margin;

            base.OnUpdate();
        }
    }

    public static class RxViewExtensions
    {
        public static T Margin<T>(this T view, Thickness margin) where T : IRxView
        {
            view.Margin = margin;
            return view;
        }

        public static T Margin<T>(this T view, double left, double right, double top, double bottom) where T : IRxView
        {
            view.Margin = new Thickness(left, right, top, bottom);
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
