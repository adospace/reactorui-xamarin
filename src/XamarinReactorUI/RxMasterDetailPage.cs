using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxMasterDetailPage
    {
        bool IsGestureEnabled { get; set; }
        bool IsPresented { get; set; }
        MasterBehavior MasterBehavior { get; set; }
    }

    public class RxMasterDetailPage<T> : RxPage<T>, IRxMasterDetailPage where T : Xamarin.Forms.MasterDetailPage, new()
    {
        public RxMasterDetailPage()
        {
        }

        public RxMasterDetailPage(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public bool IsGestureEnabled { get; set; } = (bool)MasterDetailPage.IsGestureEnabledProperty.DefaultValue;
        public bool IsPresented { get; set; } = (bool)MasterDetailPage.IsPresentedProperty.DefaultValue;
        public MasterBehavior MasterBehavior { get; set; } = (MasterBehavior)MasterDetailPage.MasterBehaviorProperty.DefaultValue;

        public VisualNode Master { get; set; }
        public VisualNode Detail { get; set; }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is Page page)
            {
                if (widget == Master)
                    NativeControl.Master = page;
                else
                    NativeControl.Detail = page;
            }
            //else
            //{
            //    throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            //}

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
            if (NativeControl.Master == childNativeControl)
                NativeControl.Master = null;
            else
                NativeControl.Detail = null;

            base.OnRemoveChild(widget, childNativeControl);
        }

        protected override void OnUpdate()
        {
            NativeControl.IsGestureEnabled = IsGestureEnabled;
            NativeControl.IsPresented = IsPresented;
            NativeControl.MasterBehavior = MasterBehavior;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Master;
            yield return Detail;
        }

    }

    public class RxMasterDetailPage : RxMasterDetailPage<MasterDetailPage>
    {
        public RxMasterDetailPage()
        {
        }

        public RxMasterDetailPage(Action<MasterDetailPage> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxMasterDetailPageExtensions
    {
        public static T IsGestureEnabled<T>(this T masterdetailpage, bool isGestureEnabled) where T : IRxMasterDetailPage
        {
            masterdetailpage.IsGestureEnabled = isGestureEnabled;
            return masterdetailpage;
        }

        public static T IsPresented<T>(this T masterdetailpage, bool isPresented) where T : IRxMasterDetailPage
        {
            masterdetailpage.IsPresented = isPresented;
            return masterdetailpage;
        }

        public static T MasterBehavior<T>(this T masterdetailpage, MasterBehavior masterBehavior) where T : IRxMasterDetailPage
        {
            masterdetailpage.MasterBehavior = masterBehavior;
            return masterdetailpage;
        }
    }
}