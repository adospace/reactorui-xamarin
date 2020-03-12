
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTabBar
    {
    }

    public sealed class RxTabBar : RxShellItem<Xamarin.Forms.TabBar>, IRxTabBar
    {
    }

    public static class RxTabBarExtensions
    {
    }

}
