using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public static class NavigationExtensions
    {
        public static async Task<Page> PushAsync<T>(this INavigation navigation) where T : RxComponent, new()
        {
            var page = RxPageHost<T>.CreatePage();
            await navigation.PushAsync(page);
            return page;
        }

        public static async Task<Page> PushAsync<T, P>(this INavigation navigation, Action<P> propsInitializer) where T : RxComponent, new() where P : class, IProps, new()
        {
            var page = RxPageHost<T, P>.CreatePage(propsInitializer);
            await navigation.PushAsync(page);
            return page;
        }

        public static async Task<Page> PushAsync<T, P>(this INavigation navigation, P props) where T : RxComponent, new() where P : class, IProps, new()
        {
            var page = RxPageHost<T, P>.CreatePage();
            await navigation.PushAsync(page);
            return page;
        }

        public static async Task<Page> PushAsync<T>(this INavigation navigation, bool animated) where T : RxComponent, new()
        {
            var page = RxPageHost<T>.CreatePage();
            await navigation.PushAsync(page, animated);
            return page;
        }

        public static async Task<Page> PushAsync<T, P>(this INavigation navigation, bool animated, Action<P> propsInitializer) where T : RxComponent, new() where P : class, IProps, new()
        {
            var page = RxPageHost<T, P>.CreatePage(propsInitializer);
            await navigation.PushAsync(page, animated);
            return page;
        }

        public static async Task<Page> PushModalAsync<T>(this INavigation navigation) where T : RxComponent, new()
        {
            var page = RxPageHost<T>.CreatePage();
            await navigation.PushModalAsync(page);
            return page;
        }

        public static async Task<Page> PushModalAsync<T, P>(this INavigation navigation, Action<P> propsInitializer) where T : RxComponent, new() where P : class, IProps, new()
        {
            var page = RxPageHost<T, P>.CreatePage(propsInitializer);
            await navigation.PushModalAsync(page);
            return page;
        }

        public static async Task<Page> PushModalAsync<T>(this INavigation navigation, bool animated) where T : RxComponent, new()
        {
            var page = RxPageHost<T>.CreatePage();
            await navigation.PushModalAsync(page, animated);
            return page;
        }

        public static async Task<Page> PushModalAsync<T, P>(this INavigation navigation, bool animated, Action<P> propsInitializer) where T : RxComponent, new() where P : class, IProps, new()
        {
            var page = RxPageHost<T, P>.CreatePage(propsInitializer);
            await navigation.PushModalAsync(page, animated);
            return page;
        }

    }
}
