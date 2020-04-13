
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
    }

    public static class RxNavigableElementExtensions
    {

    }

}
