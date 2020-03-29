using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public class RxApplication : VisualNode, IRxHostElement
    {
        private readonly Application _application;
        private readonly RxComponent _rootComponent;
        private bool _sleeping;

        public RxApplication(Application application, RxComponent rootComponent)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _rootComponent = rootComponent;
        }

        protected sealed override void OnAddChild(VisualNode widget, Element nativeControl)
        {
            if (nativeControl is Page page)
                _application.MainPage = page;
            else
            {
                throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a page (i.e. RxContentPage, RxShell etc)");    
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, Element nativeControl)
        {
            _application.MainPage = null;
        }

        public void Run()
        {
            _sleeping = false;
            OnLayoutCycleRequested();
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
            yield return _rootComponent;
        }

        public override INavigation Navigation()
        {
            return _application.MainPage?.Navigation;
        }
    }
}

