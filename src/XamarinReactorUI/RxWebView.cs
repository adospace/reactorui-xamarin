using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxWebView
    {
        WebViewSource Source { get; set; }
    }

    public class RxWebView<T> : RxView<T>, IRxWebView where T : Xamarin.Forms.WebView, new()
    {
        public RxWebView()
        {
        }

        public RxWebView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public WebViewSource Source { get; set; } = (WebViewSource)WebView.SourceProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Source = Source;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxWebView : RxWebView<Xamarin.Forms.WebView>
    {
        public RxWebView()
        {
        }

        public RxWebView(Action<WebView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxWebViewExtensions
    {
        public static T Source<T>(this T webview, WebViewSource source) where T : IRxWebView
        {
            webview.Source = source;
            return webview;
        }

        public static T Source<T>(this T webview, string source) where T : IRxWebView
        {
            webview.Source = source;
            return webview;
        }

        public static T Source<T>(this T webview, Uri source) where T : IRxWebView
        {
            webview.Source = source;
            return webview;
        }
    }
}