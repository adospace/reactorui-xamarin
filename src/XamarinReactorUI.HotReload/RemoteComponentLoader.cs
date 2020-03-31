using System;
using System.Reflection;

namespace XamarinReactorUI.HotReload
{
    internal class RemoteComponentLoader : IComponentLoader
    {
        private readonly HotReloadServer _server;
        private Assembly _latestAssembly;

        public static IComponentLoader Instance { get; set; } = new RemoteComponentLoader();

        public RemoteComponentLoader(int serverPort = 45820)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Only one instance of RxApplication is permitted");
            }

            Instance = this;

            _server = new HotReloadServer(serverPort);
            _server.ReceivedAssembly += ReceivedAssemblyFromHost;
        }


        private void ReceivedAssemblyFromHost(object sender, ReceivedAssemblyEventArgs e)
        {
            _latestAssembly = Assembly.Load(e.AssemblyRaw);
            ComponentAssemblyChanged?.Invoke(this, EventArgs.Empty);
        }


        //private void LoadLatestAssembly()
        //{
        //    byte[] latestAssemblyRaw;
        //    lock (this)
        //    {
        //        latestAssemblyRaw = _latestAssemblyRaw;
        //        _latestAssemblyRaw = null;
        //    }

        //    try
        //    {
        //        var assembly = Assembly.Load(latestAssemblyRaw);
        //        var type = assembly.GetType(_rootComponent.GetType().FullName);
        //        var context = _rootComponent.Context;
        //        _rootComponent = (RxComponent)Activator.CreateInstance(type);

        //        _rootComponent.Context = context;
        //        Invalidate();
        //    }
        //    catch (Exception ex)
        //    {
        //        UnhandledException?.Invoke(this, new UnhandledExceptionEventArgs(ex, false));
        //    }
        //}


        public event EventHandler ComponentAssemblyChanged;

        public RxComponent LoadComponent<T>() where T : RxComponent, new()
        {
            if (_latestAssembly == null)
                return new T();

            var type = _latestAssembly.GetType(typeof(T).FullName);
            
            return(RxComponent)Activator.CreateInstance(type);
        }
    }
}