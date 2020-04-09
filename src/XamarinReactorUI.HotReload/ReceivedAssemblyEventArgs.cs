using System;

namespace XamarinReactorUI
{
    public class ReceivedAssemblyEventArgs : EventArgs
    {
        internal ReceivedAssemblyEventArgs(byte[] assemblyRaw, byte[] assemblySymbolStoreRaw)
        {
            AssemblyRaw = assemblyRaw;
            AssemblySymbolStoreRaw = assemblySymbolStoreRaw;
        }

        public byte[] AssemblyRaw { get; }
        public byte[] AssemblySymbolStoreRaw { get; }
    }
}