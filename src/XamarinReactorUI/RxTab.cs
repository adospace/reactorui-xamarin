using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTab
    {
    }

    public sealed class RxTab : RxShellSection<Xamarin.Forms.Tab>, IRxTab
    {
        public RxTab()
        {

        }

        public RxTab(string title)
            : base(title)
        {

        }
    }

    public static class RxTabExtensions
    {
    }

}
