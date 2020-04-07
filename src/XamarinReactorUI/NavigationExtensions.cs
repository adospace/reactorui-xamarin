using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public static class NavigationExtensions
    {
        public static async Task<Page> PushAsync<T>(this INavigation navigation, Action<RxComponent> componentInitializer = null) where T : RxComponent, new()
        {
            var page = RxPageHost<T>.CreatePage(componentInitializer);
            await navigation.PushAsync(page);
            return page;
        }

        public static async Task<Page> PushAsync<T>(this INavigation navigation, bool animated, Action<RxComponent> componentInitializer = null) where T : RxComponent, new()
        {
            var page = RxPageHost<T>.CreatePage(componentInitializer);
            await navigation.PushAsync(page, animated);
            return page;
        }

        public static async Task<Page> PushModalAsync<T>(this INavigation navigation, Action<RxComponent> componentInitializer = null) where T : RxComponent, new()
        {
            var page = RxPageHost<T>.CreatePage(componentInitializer);
            await navigation.PushModalAsync(page);
            return page;
        }

        public static async Task<Page> PushModalAsync<T>(this INavigation navigation, bool animated, Action<RxComponent> componentInitializer = null) where T : RxComponent, new()
        {
            var page = RxPageHost<T>.CreatePage(componentInitializer);
            await navigation.PushModalAsync(page, animated);
            return page;
        }
    }
}
