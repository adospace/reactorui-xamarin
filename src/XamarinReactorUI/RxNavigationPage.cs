using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxNavigationPage
    {
        Color BarBackgroundColor { get; set; }
        Color BarTextColor { get; set; }
    }

    public class RxNavigationPage<T> : RxPage<T>, IRxNavigationPage, IEnumerable<VisualNode> where T : NavigationPage, new()
    {
        private VisualNode _rootPage = null;

        public RxNavigationPage(VisualNode rootPage = null)
        {
            _rootPage = rootPage;
        }

        public RxNavigationPage(Action<T> componentRefAction, VisualNode rootPage = null)
            : base(componentRefAction)
        {
            _rootPage = rootPage;
        }

        protected override void OnAddChild(VisualNode widget, Element childNativeControl)
        {
            if (childNativeControl is Page page)
            {
                _nativeControl = new NavigationPage(page);
                base.OnMount();
                OnUpdate();
            }
            else
            {
                throw new InvalidOperationException($"Root must be a Page: received {childNativeControl.GetType()}");
            }

            base.OnAddChild(widget, childNativeControl);
        }

        public void Add(VisualNode child)
        {
            if (_rootPage != null)
                throw new InvalidOperationException("Root page already specified");

            _rootPage = child;
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return new List<VisualNode>(new[] { _rootPage }).GetEnumerator();
        }

        public Color BarBackgroundColor { get; set; } = (Color)NavigationPage.BarBackgroundColorProperty.DefaultValue;
        public Color BarTextColor { get; set; } = (Color)NavigationPage.BarTextColorProperty.DefaultValue;

        protected override void OnUpdate()
        {
            if (NativeControl == null)
                return;

            NativeControl.BarBackgroundColor = BarBackgroundColor;
            NativeControl.BarTextColor = BarTextColor;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return _rootPage;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class RxNavigationPage : RxNavigationPage<NavigationPage>
    {
        public RxNavigationPage(VisualNode rootPage = null)
            : base(rootPage)
        {
        }

        public RxNavigationPage(Action<NavigationPage> componentRefAction, VisualNode rootPage = null)
            : base(componentRefAction, rootPage)
        {
        }
    }

    public static class RxNavigationPageExtensions
    {
        public static T BarBackgroundColor<T>(this T navigationpage, Color barBackgroundColor) where T : IRxNavigationPage
        {
            navigationpage.BarBackgroundColor = barBackgroundColor;
            return navigationpage;
        }

        public static T BarTextColor<T>(this T navigationpage, Color barTextColor) where T : IRxNavigationPage
        {
            navigationpage.BarTextColor = barTextColor;
            return navigationpage;
        }
    }
}