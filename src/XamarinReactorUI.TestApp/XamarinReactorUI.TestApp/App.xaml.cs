﻿using System;
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
        private Xamarin.Forms.Shapes.Rectangle cnt;

        public App()
        {
            Device.SetFlags(new[] { "Shapes_Experimental" });

            InitializeComponent();

            //TEST 1
            //MainPage = new TestHotReloadPage();

            //TEST 2
            //_rxApp = RxApplication.Create<CollectionView.TestCollectionViewComponent>(this).WithHotReload();

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
            //_rxApp = RxApplication.Create<ComponentWithChildren.PageComponent>(this).WithHotReload();

            //TEST 13
            //_rxApp = RxApplication.Create<Theming.MainPage>(this).WithHotReload();

            //TEST 14
            //_rxApp = RxApplication.Create<ListView.MainPage>(this).WithHotReload();

            //TEST 15
            //_rxApp = RxApplication.Create<Canvas.MainPage>(this).WithHotReload();

            //TEST 16
            //_rxApp = RxApplication.Create<CarouselView.TestCarouselViewComponent>(this).WithHotReload();

            //TEST 16
            //_rxApp = RxApplication.Create<TableView.MainPage>(this)
            //    .WithHotReload();

            //TEST 17
            //_rxApp = RxApplication.Create<Shell.Test3.ShellWithSearch>(this)
            //    .WithHotReload();

            //TEST 18
            //_rxApp = RxApplication.Create<Animation.MainPage>(this)
            //    .WithHotReload();

            //TEST 19
            //_rxApp = RxApplication.Create<WebView.MainPage>(this)
            //    .WithHotReload();

            //TEST 20
            //_rxApp = RxApplication.Create<PancakeView.MainPage>(this)
            //    .WithHotReload();

            //TEST 21
            //_rxApp = RxApplication.Create<SharedTransitions.MainPage>(this)
            //    .WithHotReload();

            //TEST 21
            //_rxApp = RxApplication.Create<Clip.MainPage>(this)
            //    .WithHotReload();

            ////TEST 22
            //_rxApp = RxApplication.Create<Shell.Test4.Page1>(this)
            //    .WithHotReload();

            //TEST 23
            //_rxApp = RxApplication.Create<Shapes.Test1.MainPage>(this)
            //    .WithHotReload();

            //TEST 23
            _rxApp = RxApplication.Create<Animation.Test2.MainPage>(this)
                .WithHotReload();

            _rxApp.Run();

            //MainPage = new ContentPage();
            //var sp = new StackLayout();
            //cnt = new Xamarin.Forms.Shapes.Rectangle();
            //sp.Children.Add(cnt);
            //cnt.HeightRequest = 100;
            //cnt.WidthRequest = 50;
            //cnt.VerticalOptions = LayoutOptions.Center;
            //cnt.HorizontalOptions = LayoutOptions.Center;
            //cnt.Fill = new SolidColorBrush(Color.Red);
            ////cnt.Clip = null;

            //var btn = new Button() { Text = "click" };
            //btn.Clicked += Btn_Clicked;
            //sp.Children.Add(btn);

            //((ContentPage)MainPage).Content = sp;
        }

        //private void Btn_Clicked(object sender, EventArgs e)
        //{
        //    //((ContentPage)MainPage).Content = null;
        //    cnt = new Xamarin.Forms.Shapes.Rectangle();
        //    ((ContentPage)MainPage).Content = cnt;
        //    cnt.HeightRequest = 100;
        //    cnt.WidthRequest = 50;
        //    cnt.VerticalOptions = LayoutOptions.Center;
        //    cnt.HorizontalOptions = LayoutOptions.Center;
        //    cnt.Fill = new SolidColorBrush(Color.Red);
        //    //cnt.Clip = null;

        //}

        protected override void OnStart()
        {
            //_rxApp.Run();
        }

        protected override void OnSleep()
        {
            //_rxApp.Stop();
        }

        protected override void OnResume()
        {
            //_rxApp.Run();
        }
    }
}
