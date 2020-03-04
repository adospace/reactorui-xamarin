using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public class RxPageContentContainer : RxElement
    {
        private readonly RxComponent _rootComponent;
        private readonly Xamarin.Forms.ContentPage _containerPage;

        public RxPageContentContainer(RxComponent rootComponent, Xamarin.Forms.ContentPage page)
        {
            _rootComponent = rootComponent ?? throw new ArgumentNullException(nameof(rootComponent));
            _containerPage = page ?? throw new ArgumentNullException(nameof(page));
        }

        protected override void OnAddChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {
            _containerPage.Content = (Xamarin.Forms.View)nativeControl;

            base.OnAddChild(widget, nativeControl);
        }

        protected override void OnRemoveChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {
            _containerPage.Content = null;

            base.OnRemoveChild(widget, nativeControl);
        }

        public RxPageContentContainer Run()
        {
            _pendingStop = false;
            _containerPage.Dispatcher.BeginInvokeOnMainThread(OnLayout);
            return this;
        }

        public void Stop()
        {
            _pendingStop = true;   
        }

        private bool _pendingStop = false;

        private void OnLayout()
        {
            if (_pendingStop)
                return;

            new VisualTree(this).Layout();
            _containerPage.Dispatcher.BeginInvokeOnMainThread(OnLayout);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _rootComponent;
        }
    }


    public static class RxPageContentContainerExtensions
    {
        public static RxPageContentContainer Host(this ContentPage pageContent, RxComponent component) 
            => new RxPageContentContainer(component, pageContent);
    }
}
