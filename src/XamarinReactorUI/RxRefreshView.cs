using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxRefreshView : IRxContentView
    {
        bool IsRefreshing { get; set; }
        Color RefreshColor { get; set; }
        Action RefreshingAction { get; set; }
    }

    public class RxRefreshView<T> : RxContentView<T>, IRxRefreshView where T : RefreshView, new()
    {
        public RxRefreshView()
        {
        }

        public RxRefreshView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public bool IsRefreshing { get; set; } = (bool)RefreshView.IsRefreshingProperty.DefaultValue;
        public Color RefreshColor { get; set; } = (Color)RefreshView.RefreshColorProperty.DefaultValue;
        public Action RefreshingAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.IsRefreshing = IsRefreshing;
            NativeControl.RefreshColor = RefreshColor;

            if (RefreshingAction != null)
                NativeControl.Refreshing += NativeControl_Refreshing;

            base.OnUpdate();
        }

        private void NativeControl_Refreshing(object sender, EventArgs e)
        {
            RefreshingAction?.Invoke();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.Refreshing -= NativeControl_Refreshing;

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
                NativeControl.Refreshing -= NativeControl_Refreshing;

            base.OnUnmount();
        }
    }

    public class RxRefreshView : RxRefreshView<RefreshView>
    {
        public RxRefreshView()
        {
        }

        public RxRefreshView(Action<RefreshView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxRefreshViewExtensions
    {
        public static T OnRefresh<T>(this T refreshview, Action refreshAction) where T : IRxRefreshView
        {
            refreshview.RefreshingAction = refreshAction;
            return refreshview;
        }

        public static T IsRefreshing<T>(this T refreshview, bool isRefreshing) where T : IRxRefreshView
        {
            refreshview.IsRefreshing = isRefreshing;
            return refreshview;
        }

        public static T RefreshColor<T>(this T refreshview, Color refreshColor) where T : IRxRefreshView
        {
            refreshview.RefreshColor = refreshColor;
            return refreshview;
        }
    }
}