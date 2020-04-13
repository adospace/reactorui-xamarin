using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxHostElement
    {
        void Run();

        void Stop();

        Page ContainerPage { get; }
    }
}
