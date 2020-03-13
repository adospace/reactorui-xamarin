using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTabBar
    {
    }

    public sealed class RxTabBar : RxShellItem<TabBar>, IRxTabBar
    {
        public RxTabBar()
        {
        }

        public RxTabBar(Action<TabBar> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxTabBarExtensions
    {
    }
}