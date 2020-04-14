using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Theming
{
    public static class Theme
    {
        public static RxTheme Light()
            => new RxTheme()
                .StyleFor<RxLabel>(_ =>
                {
                    _.TextColor(Color.Black);
                    _.Margin(0, 10);
                })
                .StyleFor<RxEntry>(_ =>
                {
                    _.TextColor(Color.Black);
                    _.Margin(0, 10);
                    _.BackgroundColor(Color.White);
                })
                .StyleFor<RxButton>(_ =>
                {
                    _.TextColor(Color.Black);
                    _.Margin(10);
                    _.BackgroundColor(Color.DarkGray);
                })
                .StyleFor<RxNavigationPage>(_ =>
                {
                    _.BarBackgroundColor((Color)NavigationPage.BarBackgroundColorProperty.DefaultValue);
                    _.BarTextColor((Color)NavigationPage.BarTextColorProperty.DefaultValue);
                })
                .StyleFor<RxContentPage>(_ =>
                {
                    _.BackgroundColor((Color)ContentPage.BackgroundColorProperty.DefaultValue);
                    _.Padding(10);
                });

        public static RxTheme Dark()
            => new RxTheme()
                .StyleFor<RxLabel>(_ =>
                {
                    _.TextColor(Color.White);
                    _.Margin(0, 10);
                })
                .StyleFor<RxEntry>(_ =>
                {
                    _.TextColor(Color.Black);
                    _.Margin(0, 10);
                    _.BackgroundColor(Color.LightGray);
                })
                .StyleFor<RxButton>(_ =>
                {
                    _.TextColor(Color.Black);
                    _.Margin(10);
                    _.BackgroundColor(Color.LightGray);
                })
                .StyleFor<RxNavigationPage>(_ =>
                {
                    _.BarBackgroundColor(Color.DarkGray);
                    _.BarTextColor(Color.White);
                })
                .StyleFor<RxContentPage>(_ =>
                {
                    _.BackgroundColor(Color.Black);
                    _.Padding(10);
                });
    }
}
