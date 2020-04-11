using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinReactorUI.HotReload;

namespace XamarinReactorUI.TestApp
{
    public partial class App : Application
    {
        //private readonly IRxHostElement _mainPageHost;
        private readonly RxApplication _rxApp;

        public App()
        {
            InitializeComponent();

            //TEST 1
            //MainPage = new TestHotReloadPage();

            //TEST 2
            //var mainPage = new ContentPage();

            //_mainPageHost = mainPage.Host(new TestCollectionViewComponent());

            //MainPage = mainPage;

            //TEST 3
            //_rxApp = RxApplication.Create<Shell.Test1.TestShellComponentPage>(this).WithHotReload();

            //TEST 4
            //_rxApp = RxApplication.Create<Shell.Test2.TestShellComponentPage>(this).WithHotReload();

            //TEST 5
            //MainPage = new TabbedPage.TestTabbedPage();

            //TEST 6
            //MainPage = new Layout.TestPage();

            //TEST 7
            //MainPage = new Grid.GridPage();

            //TEST 8
            //_rxApp = new RxApplication(this, new Grid.GridPageComponent());

            //TEST 9
            //_rxApp = RxApplication.Create<Navigation.MainPageComponent>(this).WithHotReload();

            //TEST 10
            //_rxApp = RxApplication.Create<ElementRef.MainPage>(this).WithHotReload();

            //TEST 11
            //_rxApp = RxApplication.Create<Busy.BusyPageComponent>(this).WithHotReload();

            //TEST 12
            _rxApp = RxApplication.Create<ComponentWithChildren.PageComponent>(this).WithHotReload();
        }

        protected override void OnStart()
        {
            _rxApp.Run();
        }

        protected override void OnSleep()
        {
            _rxApp.Stop();
        }

        protected override void OnResume()
        {
            _rxApp.Run();
        }
    }
}
