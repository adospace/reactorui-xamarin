using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTabbedPage : IRxMultiPage
    {
        Color BarBackgroundColor { get; set; }
        Color BarTextColor { get; set; }
        Color UnselectedTabColor { get; set; }
        Color SelectedTabColor { get; set; }
    }

    public class RxTabbedPage<T> : RxMultiPage<T, Page>, IRxTabbedPage where T : TabbedPage, new()
    {
        public RxTabbedPage()
        {
        }

        public RxTabbedPage(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Color BarBackgroundColor { get; set; } = (Color)TabbedPage.BarBackgroundColorProperty.DefaultValue;
        public Color BarTextColor { get; set; } = (Color)TabbedPage.BarTextColorProperty.DefaultValue;
        public Color UnselectedTabColor { get; set; } = (Color)TabbedPage.UnselectedTabColorProperty.DefaultValue;
        public Color SelectedTabColor { get; set; } = (Color)TabbedPage.SelectedTabColorProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.BarBackgroundColor = BarBackgroundColor;
            NativeControl.BarTextColor = BarTextColor;
            NativeControl.UnselectedTabColor = UnselectedTabColor;
            NativeControl.SelectedTabColor = SelectedTabColor;

            base.OnUpdate();
        }
    }

    public class RxTabbedPage : RxTabbedPage<TabbedPage>
    {
        public RxTabbedPage()
        {
        }

        public RxTabbedPage(Action<TabbedPage> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxTabbedPageExtensions
    {
        public static T BarBackgroundColor<T>(this T tabbedpage, Color barBackgroundColor) where T : IRxTabbedPage
        {
            tabbedpage.BarBackgroundColor = barBackgroundColor;
            return tabbedpage;
        }

        public static T BarTextColor<T>(this T tabbedpage, Color barTextColor) where T : IRxTabbedPage
        {
            tabbedpage.BarTextColor = barTextColor;
            return tabbedpage;
        }

        public static T UnselectedTabColor<T>(this T tabbedpage, Color unselectedTabColor) where T : IRxTabbedPage
        {
            tabbedpage.UnselectedTabColor = unselectedTabColor;
            return tabbedpage;
        }

        public static T SelectedTabColor<T>(this T tabbedpage, Color selectedTabColor) where T : IRxTabbedPage
        {
            tabbedpage.SelectedTabColor = selectedTabColor;
            return tabbedpage;
        }
    }
}