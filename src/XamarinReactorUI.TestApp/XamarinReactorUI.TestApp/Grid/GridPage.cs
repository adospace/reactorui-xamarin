using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Grid
{
    public class GridPage : ContentPage
    {
        private RxHotReloadHostElement _componentHost;

        public GridPage()
        {
            var context = new RxContext
            {
                ["Page"] = this
            };
            _componentHost = new RxHotReloadHostElement(new GridPageComponent() { Context = context });
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
