using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxHostElement
    {
        IRxHostElement Run();

        void Stop();

        Page ContainerPage { get; }
    }
}
