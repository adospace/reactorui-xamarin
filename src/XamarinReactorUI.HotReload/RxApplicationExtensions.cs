using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.HotReload
{
    public static class RxApplicationExtensions
    {
        public static RxApplication WithHotReload(this RxApplication application)
        {
            application.ComponentLoader = RemoteComponentLoader.Instance;
            return application;
        }
    }
}
