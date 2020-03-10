using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public class RxHotReloadHostElement : RxElement, IRxHostElement
    {
        private readonly HotReloadServer _server;
        private RxComponent _rootComponent;
        private bool _pendingStop = false;
        private byte[] _latestAssemblyRaw;

        public RxHotReloadHostElement(RxComponent rootComponent)
            : this(rootComponent, 45821)
        {
        }

        public RxHotReloadHostElement(RxComponent rootComponent, int serverPort, params object[] args)
        {
            _rootComponent = rootComponent ?? throw new ArgumentNullException(nameof(rootComponent));
            _server = new HotReloadServer(serverPort);
            _server.ReceivedAssembly += ReceivedAssemblyFromHost;
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

            _server.Run();
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
            var context = _rootComponent.Context;
            _rootComponent = (RxComponent)Activator.CreateInstance(type);
            _rootComponent.Context = context;
            Invalidate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _rootComponent;
        }
    }
}
