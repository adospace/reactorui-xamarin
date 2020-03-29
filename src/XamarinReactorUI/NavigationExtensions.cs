using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public static class NavigationExtensions
    {
        public static Task PushAsync<T>(this INavigation navigation) where T : RxComponent, new() 
            => navigation.PushAsync(RxPageHost.CreatePage<T>());

        public static Task PushAsync<T>(this INavigation navigation, bool animated) where T : RxComponent, new() 
            => navigation.PushAsync(RxPageHost.CreatePage<T>(), animated);

        public static Task PushModalAsync<T>(this INavigation navigation) where T : RxComponent, new()
            => navigation.PushModalAsync(RxPageHost.CreatePage<T>());

        public static Task PushModalAsync<T>(this INavigation navigation, bool animated) where T : RxComponent, new()
            => navigation.PushModalAsync(RxPageHost.CreatePage<T>(), animated);
    }
}
