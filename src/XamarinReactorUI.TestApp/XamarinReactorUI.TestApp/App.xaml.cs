using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinReactorUI.TestApp
{
    public partial class App : Application
    {
        private RxPageContentContainer _mainPageHost;

        public App()
        {
            InitializeComponent();

            var mainPage = new ContentPage();
            MainPage = mainPage;

            _mainPageHost = mainPage
                .Host(new TestCollectionViewComponent())
                .Run();

            
        }

        protected override void OnStart()
        {
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
