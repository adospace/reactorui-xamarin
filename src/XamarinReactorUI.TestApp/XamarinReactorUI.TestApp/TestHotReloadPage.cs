using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinReactorUI.TestApp
{
    public class TestHotReloadContext : IValueSet
    { 
        public ContentPage Page { get; set; }
    }

    public class TestHotReloadPage : ContentPage
    {
        private RxHotReloadHostElement _componentHost;

        public TestHotReloadPage()
        {
            _componentHost = new RxHotReloadHostElement(new TestHotReloadComponent() { Context = new TestHotReloadContext { Page = this } });
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