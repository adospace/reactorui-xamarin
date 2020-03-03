using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI
{
    public class ReactorContainer : RxElement
    {
        private readonly RxComponent _rootComponent;
        private readonly Xamarin.Forms.ContentPage _containerPage;

        public ReactorContainer(RxComponent rootComponent, Xamarin.Forms.ContentPage page)
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

        public ReactorContainer Run()
        {
            _containerPage.Dispatcher.BeginInvokeOnMainThread(OnLayout);
            return this;
        }

        private void OnLayout()
        {
            new VisualTree(this).Layout();
            _containerPage.Dispatcher.BeginInvokeOnMainThread(OnLayout);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _rootComponent;
        }
    }
}
