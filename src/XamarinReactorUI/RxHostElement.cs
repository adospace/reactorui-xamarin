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

        event EventHandler<UnhandledExceptionEventArgs> UnhandledException;
    }

    public sealed class RxHostElement : VisualNode, IRxHostElement
    {
        private readonly RxComponent _rootComponent;
        private readonly VisualTree _visualTree;

        public RxHostElement(RxComponent rootComponent)
        {
            _rootComponent = rootComponent ?? throw new ArgumentNullException(nameof(rootComponent));
            _visualTree = new VisualTree(this);
        }

        protected sealed override void OnAddChild(VisualNode widget, Xamarin.Forms.Element nativeControl)
        {
            
        }

        protected sealed override void OnRemoveChild(VisualNode widget, Xamarin.Forms.Element nativeControl)
        {
        }

        public void Run()
        {
            //_pendingStop = false;
            //Device.BeginInvokeOnMainThread(OnLayout);
            _visualTree.LayoutCycleRequest += VisualTree_LayoutCycleRequest;
            OnLayout();
        }

        private void VisualTree_LayoutCycleRequest(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(OnLayout);
        }

        public void Stop()
        {
            _visualTree.LayoutCycleRequest += VisualTree_LayoutCycleRequest;
            //_pendingStop = true;
        }

        //private bool _pendingStop = false;

        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;

        private void OnLayout()
        {
            //if (_pendingStop)
            //    return;

            try
            {
                _visualTree.Layout();
            }
            catch (Exception ex)
            {
                UnhandledException?.Invoke(this, new UnhandledExceptionEventArgs(ex, false));
                System.Diagnostics.Trace.WriteLine(ex);
            }

            //Device.BeginInvokeOnMainThread(OnLayout);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _rootComponent;
        }
    }
}
