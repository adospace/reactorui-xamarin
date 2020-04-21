using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTabBar : IRxShellItem
    {
    }

    public class RxTabBar<T> : RxShellItem<T>, IRxTabBar where T : TabBar, new()
    {
        public RxTabBar()
        {
        }

        public RxTabBar(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public class RxTabBar : RxTabBar<TabBar>
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