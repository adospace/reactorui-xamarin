using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public abstract class RxContentView<T> : RxLayout<T> where T : ContentView, new()
    {
        public RxContentView()
        { }

        protected RxContentView(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        protected override void OnAddChild(VisualNode widget, Element childControl)
        {
            if (childControl is View view)
                NativeControl.Content = view;
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, Element childControl)
        {
            if (childControl is View _)
                NativeControl.Content = null;
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnRemoveChild(widget, childControl);
        }

    }
}
