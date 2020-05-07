using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    internal class RxPageHost<T> : VisualNode, IRxHostElement where T : RxComponent, new()
    {
        private RxComponent _component;
        private bool _sleeping;
        public Page ContainerPage { get; private set; }

        protected RxPageHost()
        {
        }

        public static Page CreatePage()
        {
            var host = new RxPageHost<T>();
            host.Run();
            return host.ContainerPage;
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is Page page)
            {
                ContainerPage = page;
                ContainerPage.Appearing += OnComponentPage_Appearing;
                ContainerPage.Disappearing += OnComponentPage_Disappearing;
            }
            else
            {
                throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a page (i.e. RxContentPage, RxShell etc)");
            }
        }

        private void OnComponentPage_Appearing(object sender, EventArgs e)
        {
            _sleeping = false;
            OnLayoutCycleRequested();
        }

        private void OnComponentPage_Disappearing(object sender, EventArgs e)
        {
            _sleeping = true;
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            if (ContainerPage != null)
            {
                ContainerPage.Appearing += OnComponentPage_Appearing;
                ContainerPage.Disappearing += OnComponentPage_Disappearing;
            }

            ContainerPage = null;
        }

        protected virtual RxComponent InitializeComponent(RxComponent component)
        {
            return component;
        }

        public IRxHostElement Run()
        {
            _component = _component ?? InitializeComponent(RxApplication.Instance.ComponentLoader.LoadComponent<T>());

            RxApplication.Instance.ComponentLoader.ComponentAssemblyChanged += OnComponentAssemblyChanged;

            OnLayout();

            if (ContainerPage == null)
            {
                throw new InvalidOperationException($"Component {_component.GetType()} doesn't render a page as root");
            }

            return this;
        }

        private void OnComponentAssemblyChanged(object sender, EventArgs e)
        {
            try
            {
                var newComponent = RxApplication.Instance.ComponentLoader.LoadComponent<T>();
                if (newComponent != null)
                {
                    _component = newComponent;

                    Invalidate();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to hot relead component {typeof(T).FullName}: type not found in received assembly");
                }
            }
            catch (Exception ex)
            {
                RxApplication.Instance.FireUnhandledExpectionEvent(ex);
            }
        }

        public void Stop()
        {
            RxApplication.Instance.ComponentLoader.ComponentAssemblyChanged -= OnComponentAssemblyChanged;
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

        private void OnLayout()
        {
            try
            {
                Layout();
            }
            catch (Exception ex)
            {
                RxApplication.Instance?.FireUnhandledExpectionEvent(ex);
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _component;
        }
    }

    internal class RxPageHost<T, P> : RxPageHost<T> where T : RxComponent, new() where P : class, IProps, new()
    {
        private readonly Action<P> _propsInitializer;

        protected RxPageHost(Action<P> stateInitializer)
        {
            _propsInitializer = stateInitializer;
        }

        public static Page CreatePage(Action<P> stateInitializer) 
        {
            var host = new RxPageHost<T, P>(stateInitializer);
            host.Run();
            return host.ContainerPage;
        }

        protected override RxComponent InitializeComponent(RxComponent component)
        {
            var componentWithProps = (RxComponentWithProps<P>)component;
            _propsInitializer?.Invoke(componentWithProps.Props);
            return component;
        }

    }
}
