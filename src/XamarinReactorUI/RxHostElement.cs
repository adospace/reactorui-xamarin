using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public class RxHostElement : RxElement
    {
        private readonly RxComponent _rootComponent;

        public RxHostElement(RxComponent rootComponent)
        {
            _rootComponent = rootComponent ?? throw new ArgumentNullException(nameof(rootComponent));
        }

        protected sealed override void OnAddChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {
            
        }

        protected sealed override void OnRemoveChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {
        }

        public RxHostElement Run()
        {
            _pendingStop = false;
            Device.BeginInvokeOnMainThread(OnLayout);
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
            Device.BeginInvokeOnMainThread(OnLayout);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _rootComponent;
        }
    }
}
