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
    }

    public abstract class RxView<T> : RxVisualElement, IRxView where T : Xamarin.Forms.View, new()
    {
        protected RxView()
        {
        }

        public LayoutOptions HorizontalOptions { get; set; }
        public LayoutOptions VerticalOptions { get; set; }

        protected T NativeControl { get => (T)_nativeControl; }

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
            NativeControl.HorizontalOptions = HorizontalOptions;
            NativeControl.VerticalOptions = VerticalOptions;

            base.OnUpdate();
        }
    }

    public static class RxViewExtensions
    {
        public static T HorizontalOptions<T>(this T view, LayoutOptions layoutOptions) where T : IRxView
        {
            view.HorizontalOptions = layoutOptions;
            return view;
        }

        public static T VerticalOptions<T>(this T view, LayoutOptions layoutOptions) where T : IRxView
        {
            view.VerticalOptions = layoutOptions;
            return view;
        }
    }
}
