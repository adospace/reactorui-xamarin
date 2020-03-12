
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxNavigableElement
    {
    }

    public abstract class RxNavigableElement<T> : RxElement, IRxNavigableElement where T : Xamarin.Forms.NavigableElement, new()
    {
        protected RxNavigableElement()
        {
        }

        protected T NativeControl { get => (T)_nativeControl; }

        protected override void OnUpdate()
        {

            base.OnUpdate();
        }


        protected override void OnMount()
        {
            _nativeControl = _nativeControl ?? new T();
            Parent.AddChild(this, NativeControl);

            base.OnMount();
        }

        protected override void OnUnmount()
        {
            Parent.RemoveChild(this, NativeControl);
            _nativeControl = null;

            base.OnUnmount();
        }

    }

    public static class RxNavigableElementExtensions
    {

    }

}
