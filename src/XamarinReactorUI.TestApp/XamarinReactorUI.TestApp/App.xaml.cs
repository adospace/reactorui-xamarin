using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinReactorUI.TestApp
{
    public partial class App : Application
    {
        private readonly IRxHostElement _mainPageHost;

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
            //var mainPage = new Xamarin.Forms.Shell();

            //_mainPageHost = new RxHotReloadHostElement(new Shell.Test1.TestShellComponentPage() { Context = new Shell.Test1.TestShellContext() { Page = mainPage } });

            //MainPage = mainPage;

            //TEST 4
            //var mainPage = new Xamarin.Forms.Shell();

            //_mainPageHost = new RxHotReloadHostElement(new Shell.Test2.TestShellComponentPage() { Context = new Shell.Test2.TestShellContext(mainPage)  });

            //MainPage = mainPage;

            //TEST 5
            //MainPage = new TabbedPage.TestTabbedPage();

            //TEST 6
            //MainPage = new Layout.TestPage();

            //TEST 7
            MainPage = new Grid.GridPage();

        }

        protected override void OnStart()
        {
            _mainPageHost?.Run();
        }

        protected override void OnSleep()
        {
            _mainPageHost?.Stop();
        }

        protected override void OnResume()
        {
            _mainPageHost?.Run();
        }
    }
}
