using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.TestApp.WebView
{
    public class MainPage : RxComponent
    {
        public override VisualNode Render()
        => new RxNavigationPage()
        {
            new RxContentPage("WebView")
            {
                new RxWebView()
                    .Source("https://dotnet.microsoft.com/apps/xamarin")
            }
        };
    }
}
