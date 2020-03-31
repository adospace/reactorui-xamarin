using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI
{
    internal class LocalComponentLoader : IComponentLoader
    {
        public event EventHandler ComponentAssemblyChanged;

        public RxComponent LoadComponent<T>() where T : RxComponent, new() => new T();
    }
}
