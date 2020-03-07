using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinReactorUI.TestApp
{
    public partial class App : Application
    {
        private readonly RxPageContentContainer _mainPageHost;

        public App()
        {
            InitializeComponent();

            var mainPage = new ContentPage();

            _mainPageHost = mainPage.Host(new TestCollectionViewComponent());

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            _mainPageHost.Run();
        }

        protected override void OnSleep()
        {
            _mainPageHost.Stop();
        }

        protected override void OnResume()
        {
            _mainPageHost.Run();
        }
    }
}
