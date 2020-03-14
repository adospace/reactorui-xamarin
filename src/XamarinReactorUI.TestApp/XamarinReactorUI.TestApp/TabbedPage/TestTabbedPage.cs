using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.TestApp.TabbedPage
{
    public class TestTabbedPage : Xamarin.Forms.TabbedPage
    {
        private IRxHostElement _componentHost;

        public TestTabbedPage()
        {
            _componentHost = new RxHotReloadHostElement(new TestTabbedComponent() { Context = new RxContext(c => c["ContainerPage"] = this) });
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
