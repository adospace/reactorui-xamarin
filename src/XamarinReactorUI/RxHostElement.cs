using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxHostElement
    {
        void Run();

        void Stop();
    }

    public class RxHostElement : RxElement, IRxHostElement
    {
        private readonly RxComponent _rootComponent;

        public RxHostElement(RxComponent rootComponent)
        {
            _rootComponent = rootComponent ?? throw new ArgumentNullException(nameof(rootComponent));
        }

        protected sealed override void OnAddChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {
            
        }

        protected sealed override void OnRemoveChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {
        }

        public void Run()
        {
            _pendingStop = false;
            Device.BeginInvokeOnMainThread(OnLayout);
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
