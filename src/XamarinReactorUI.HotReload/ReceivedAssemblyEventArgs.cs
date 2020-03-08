using System;

namespace XamarinReactorUI
{
    public class ReceivedAssemblyEventArgs : EventArgs
    {
        internal ReceivedAssemblyEventArgs(byte[] assemblyRaw)
        {
            AssemblyRaw = assemblyRaw;
        }

        public byte[] AssemblyRaw { get; }
    }
}