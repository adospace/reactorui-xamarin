using System;

namespace XamarinReactorUI
{
    public interface IRxHostElement
    {
        void Run();

        void Stop();

        //event EventHandler<UnhandledExceptionEventArgs> UnhandledException;
    }
}
