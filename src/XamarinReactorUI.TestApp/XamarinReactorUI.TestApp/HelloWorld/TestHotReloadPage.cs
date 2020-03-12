using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.HelloWorld
{
    public class TestHotReloadContext : RxContext
    {
        public TestHotReloadContext(ContentPage page)
        {
            this["Page"] = page;
        }
    }

    public class TestHotReloadPage : ContentPage
    {
        private RxHotReloadHostElement _componentHost;

        public TestHotReloadPage()
        {
            _componentHost = new RxHotReloadHostElement(new TestHotReloadComponent() { Context = new TestHotReloadContext(this) });
        }

        protected override void OnAppearing()
        {
            _componentHost.Run();

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _componentHost.Stop();

            base.OnDisappearing();
        }
    }
}