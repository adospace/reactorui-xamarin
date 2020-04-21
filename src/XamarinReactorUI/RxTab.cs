using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTab : IRxShellSection
    {
    }

    public class RxTab<T> : RxShellSection<T>, IRxTab where T : Tab, new()
    {
        public RxTab()
        {

        }

        public RxTab(string title)
            : base(title)
        {

        }

        public RxTab(Action<Tab> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public class RxTab : RxTab<Tab>
    {
        public RxTab()
        {

        }

        public RxTab(string title)
            : base(title)
        {

        }

        public RxTab(Action<Tab> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxTabExtensions
    {
    }

}
