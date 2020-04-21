using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public class SizeChangedEventArgs : EventArgs
    {
        public SizeChangedEventArgs(Size size)
        {
            Size = size;
        }

        public Size Size { get; set; }
    }
}