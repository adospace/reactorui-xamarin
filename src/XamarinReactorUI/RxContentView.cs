using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxContentView : IRxLayout
    {
    }

    public abstract class RxContentView<T> : RxLayout<T>, IRxContentView where T : ContentView, new()
    {
        public RxContentView()
        { }

        protected RxContentView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is View view)
                NativeControl.Content = view;
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
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

    public class RxContentView : RxContentView<ContentView>
    {
        public RxContentView()
        { }

        protected RxContentView(Action<ContentView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }
}