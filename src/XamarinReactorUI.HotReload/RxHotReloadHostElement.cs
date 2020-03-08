using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public class RxHotReloadHostElement : RxElement
    {
        private readonly HotReloadServer _server;
        private RxComponent _rootComponent;
        private readonly object[] _args;
        private bool _pendingStop = false;
        private byte[] _latestAssemblyRaw;

        public RxHotReloadHostElement(RxComponent rootComponent, params object[] args)
            : this(rootComponent, 45821, args)
        {
        }

        public RxHotReloadHostElement(RxComponent rootComponent, int serverPort, params object[] args)
        {
            _rootComponent = rootComponent ?? throw new ArgumentNullException(nameof(rootComponent));
            _args = args;
            _server = new HotReloadServer(serverPort);
            _server.ReceivedAssembly += ReceivedAssemblyFromHost;
        }

        protected sealed override void OnAddChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {
            
        }

        protected sealed override void OnRemoveChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {
        }

        public RxHotReloadHostElement Run()
        {
            _pendingStop = false;
            Device.BeginInvokeOnMainThread(OnLayout);

            _server.Run();

            return this;
        }

        
        private void ReceivedAssemblyFromHost(object sender, ReceivedAssemblyEventArgs e)
        {
            lock (this)
            {
                _latestAssemblyRaw = e.AssemblyRaw;
            }            
        }

        public void Stop()
        {
            _server.Stop();
            _pendingStop = true;
        }

        private void OnLayout()
        {
            if (_pendingStop)
                return;

            if (_latestAssemblyRaw != null)
            {
                LoadLatestAssembly();
            }

            new VisualTree(this).Layout();
            Device.BeginInvokeOnMainThread(OnLayout);
        }

        private void LoadLatestAssembly()
        {
            byte[] latestAssemblyRaw;
            lock (this)
            {
                latestAssemblyRaw = _latestAssemblyRaw;
                _latestAssemblyRaw = null;
            }

            var assembly = Assembly.Load(latestAssemblyRaw);
            var type = assembly.GetType(_rootComponent.GetType().FullName);
            _rootComponent = (RxComponent)Activator.CreateInstance(type, _args);
            Invalidate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _rootComponent;
        }
    }
}
