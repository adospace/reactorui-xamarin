using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinReactorUI.TestApp
{
    public class TestHotReloadPage : ContentPage
    {
        private RxHotReloadHostElement _componentHost;

        public TestHotReloadPage()
        {
            _componentHost = new RxHotReloadHostElement(new TestHotReloadComponent(this), this);
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