using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public abstract class RxApplication : VisualNode, IRxHostElement
    { 
        public static RxApplication Instance { get; private set; }
        protected readonly Application _application;

        internal IComponentLoader ComponentLoader { get; set; } = new LocalComponentLoader();

        protected RxApplication(Application application)
        {
            //if (Instance != null)
            //{
            //    throw new InvalidOperationException("Only one instance of RxApplication is permitted");
            //}

            Instance = this;

            _application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public Action<UnhandledExceptionEventArgs> UnhandledException { get; set; }

        internal void FireUnhandledExpectionEvent(Exception ex)
        {
            UnhandledException?.Invoke(new UnhandledExceptionEventArgs(ex, false));
            System.Diagnostics.Debug.WriteLine(ex);
        }

        public abstract void Run();

        public abstract void Stop();

        public static RxApplication Create<T>(Application application) where T : RxComponent, new() 
            => new RxApplication<T>(application);

        public RxApplication WithContext(string key, object value)
        {
            Context[key] = value;
            return this;
        }

        public RxApplication OnUnhandledException(Action<UnhandledExceptionEventArgs> action)
        {
            UnhandledException = action;
            return this;
        }

        public INavigation Navigation =>  _application.MainPage?.Navigation;

        public RxContext Context { get; } = new RxContext();

        public Page ContainerPage => _application.MainPage;

    }

    public class RxApplication<T> : RxApplication where T : RxComponent, new()
    {
        private RxComponent _rootComponent;
        private bool _sleeping;

        internal RxApplication(Application application)
            :base(application)
        {
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
            //MainPage can't be set to null!
            //_application.MainPage = null;
        }

        public override void Run()
        {
            _rootComponent = _rootComponent ?? ComponentLoader.LoadComponent<T>();
            ComponentLoader.ComponentAssemblyChanged += OnComponentAssemblyChanged;
            _sleeping = false;
            OnLayoutCycleRequested();
            ComponentLoader.Run();
        }

        private void OnComponentAssemblyChanged(object sender, EventArgs e)
        {
            try
            {
                var newComponent = ComponentLoader.LoadComponent<T>();
                if (newComponent != null)
                {
                    _rootComponent = newComponent;
                    Invalidate();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to hot relead component {typeof(T).FullName}: type not found in received assembly");
                }
            }
            catch (Exception ex)
            {
                FireUnhandledExpectionEvent(ex);
            }
        }

        public override void Stop()
        {
            ComponentLoader.ComponentAssemblyChanged -= OnComponentAssemblyChanged;
            _sleeping = true;
            ComponentLoader.Stop();
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
                FireUnhandledExpectionEvent(ex);
            }
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _rootComponent;
        }

    }
}

