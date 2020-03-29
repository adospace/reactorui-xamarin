using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    internal class RxPageHost : VisualNode, IRxHostElement
    {
        private readonly RxComponent _component;
        private bool _sleeping;
        private Page _componentPage;

        private RxPageHost(RxComponent component)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public static Page CreatePage<T>() where T : RxComponent, new()
        {
            var host = new RxPageHost(new T());
            host.Run();
            return host._componentPage;
        }

        protected sealed override void OnAddChild(VisualNode widget, Element nativeControl)
        {
            if (nativeControl is Page page)
            {
                _componentPage = page;
                _componentPage.Appearing += OnComponentPage_Appearing;
                _componentPage.Disappearing += OnComponentPage_Disappearing;
            }
            else
            {
                throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a page (i.e. RxContentPage, RxShell etc)");
            }
        }

        private void OnComponentPage_Disappearing(object sender, EventArgs e)
        {
            _sleeping = false;
            OnLayoutCycleRequested();
        }

        private void OnComponentPage_Appearing(object sender, EventArgs e)
        {
            _sleeping = true;
        }

        protected sealed override void OnRemoveChild(VisualNode widget, Element nativeControl)
        {
            if (_componentPage != null)
            {
                _componentPage.Appearing += OnComponentPage_Appearing;
                _componentPage.Disappearing += OnComponentPage_Disappearing;
            }

            _componentPage = null;
        }

        public void Run()
        {
            OnLayout();

            if (_componentPage == null)
            {
                throw new InvalidOperationException($"Component {_component.GetType()} doesn't render a page as root");
            }
        }

        public void Stop()
        {
            _sleeping = true;
        }

        protected internal override void OnLayoutCycleRequested()
        {
            if (!_sleeping)
            {
                Device.BeginInvokeOnMainThread(OnLayout);
            }

            base.OnLayoutCycleRequested();
        }

        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;

        private void OnLayout()
        {
            try
            {
                Layout();
            }
            catch (Exception ex)
            {
                UnhandledException?.Invoke(this, new UnhandledExceptionEventArgs(ex, false));
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _component;
        }
    }
}
