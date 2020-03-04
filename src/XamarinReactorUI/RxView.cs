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

        private readonly NullableField<LayoutOptions> _horizonalOptions = new NullableField<LayoutOptions>();
        public LayoutOptions HorizontalOptions { get => _horizonalOptions.GetValueOrDefault(); set => _horizonalOptions.Value = value; }

        private readonly NullableField<LayoutOptions> _verticalOptions = new NullableField<LayoutOptions>();
        public LayoutOptions VerticalOptions { get => _verticalOptions.GetValueOrDefault(); set => _verticalOptions.Value = value; }

        private readonly NullableField<Thickness> _margin = new NullableField<Thickness>();
        public Thickness Margin { get => _margin.GetValueOrDefault(); set => _margin.Value = value; }

        protected override void OnMount()
        {
            _nativeControl = new T();
            Parent.AddChild(this, NativeControl);

            base.OnMount();
        }

        protected override void OnUnmount()
        {
            Parent.RemoveChild(this, NativeControl);
            _nativeControl = null;

            base.OnUnmount();
        }

        protected override void OnUpdate()
        {
            if (_horizonalOptions.HasValue) NativeControl.HorizontalOptions = _horizonalOptions.Value;
            if (_verticalOptions.HasValue) NativeControl.VerticalOptions = _verticalOptions.Value;
            if (_margin.HasValue) NativeControl.Margin = _margin.Value;

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
            view.HorizontalOptions = LayoutOptions.Start;
            return view;
        }

        public static T VCenter<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.Center;
            return view;
        }

        public static T VEnd<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.End;
            return view;
        }

        public static T VFill<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.Fill;
            return view;
        }
        public static T VStartAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.StartAndExpand;
            return view;
        }
        public static T VCenterAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.CenterAndExpand;
            return view;
        }
        public static T VEndAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.EndAndExpand;
            return view;
        }
        public static T VFillAndExpand<T>(this T view) where T : IRxView
        {
            view.HorizontalOptions = LayoutOptions.FillAndExpand;
            return view;
        }

    }
}
