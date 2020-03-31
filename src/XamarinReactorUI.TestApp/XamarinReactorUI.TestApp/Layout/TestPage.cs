//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xamarin.Forms;

//namespace XamarinReactorUI.TestApp.Layout
//{
//    public class TestPage : ContentPage
//    {
//        private RxHotReloadHostElement _componentHost;

//        public TestPage()
//        {
//            var context = new RxContext
//            {
//                ["Page"] = this
//            };
//            _componentHost = new RxHotReloadHostElement(new TestPageComponent() { Context = context });
//        }

//        protected override void OnAppearing()
//        {
//            _componentHost.Run();

//            base.OnAppearing();
//        }

//        protected override void OnDisappearing()
//        {
//            _componentHost.Stop();

//            base.OnDisappearing();
//        }
//    }
//}
