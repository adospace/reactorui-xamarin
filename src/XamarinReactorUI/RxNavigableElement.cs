
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxNavigableElement
    {
    }

    public abstract class RxNavigableElement<T> : RxElement<T>, IRxNavigableElement where T : Xamarin.Forms.NavigableElement, new()
    {
        private readonly Action<T> _componentRefAction;

        protected RxNavigableElement()
        {
        }

        protected RxNavigableElement(Action<T> componentRefAction)
        {
            _componentRefAction = componentRefAction;
        }

        protected override void OnUpdate()
        {

            base.OnUpdate();
        }


        //protected override void OnMount()
        //{
        //    _nativeControl = _nativeControl ?? new T();
        //    Parent.AddChild(this, NativeControl);

        //    _componentRefAction?.Invoke(NativeControl);

        //    base.OnMount();
        //}

        //protected override void OnUnmount()
        //{
        //    if (_nativeControl != null)
        //    {
        //        Parent.RemoveChild(this, _nativeControl);
        //        _nativeControl = null;
        //        _componentRefAction?.Invoke(null);
        //    }

        //    base.OnUnmount();
        //}

    }

    public static class RxNavigableElementExtensions
    {

    }

}
