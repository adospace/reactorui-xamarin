using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxStructuredItemsView : IRxItemsView
    {
        //Object Header { get; set; }
        //DataTemplate HeaderTemplate { get; set; }
        //Object Footer { get; set; }
        //DataTemplate FooterTemplate { get; set; }
        IItemsLayout ItemsLayout { get; set; }

        ItemSizingStrategy ItemSizingStrategy { get; set; }
    }

    public abstract class RxStructuredItemsView<T, I> : RxItemsView<T, I>, IRxStructuredItemsView where T : StructuredItemsView, new()
    {
        public RxStructuredItemsView()
        {
        }

        public RxStructuredItemsView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public IItemsLayout ItemsLayout { get; set; } = (IItemsLayout)StructuredItemsView.ItemsLayoutProperty.DefaultValue;

        public VisualNode Header { get; set; }
        public VisualNode Footer { get; set; }

        public ItemSizingStrategy ItemSizingStrategy { get; set; } = (ItemSizingStrategy)StructuredItemsView.ItemSizingStrategyProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.ItemsLayout = ItemsLayout;
            NativeControl.ItemSizingStrategy = ItemSizingStrategy;

            base.OnUpdate();
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
        {
            if (widget == Header)
                NativeControl.Header = childNativeControl;
            else
                NativeControl.Footer = childNativeControl;

            base.OnAddChild(widget, childNativeControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
            if (widget == Header)
                NativeControl.Header = null;
            else
                NativeControl.Footer = null;

            base.OnRemoveChild(widget, childNativeControl);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Header;
            yield return Footer;
        }
    }

    public static class RxStructuredItemsViewExtensions
    {
        //public static T Header<T>(this T structureditemsview, Object header) where T : IRxStructuredItemsView
        //{
        //    structureditemsview.Header = header;
        //    return structureditemsview;
        //}
        //public static T HeaderTemplate<T>(this T structureditemsview, DataTemplate headerTemplate) where T : IRxStructuredItemsView
        //{
        //    structureditemsview.HeaderTemplate = headerTemplate;
        //    return structureditemsview;
        //}
        //public static T Footer<T>(this T structureditemsview, Object footer) where T : IRxStructuredItemsView
        //{
        //    structureditemsview.Footer = footer;
        //    return structureditemsview;
        //}
        //public static T FooterTemplate<T>(this T structureditemsview, DataTemplate footerTemplate) where T : IRxStructuredItemsView
        //{
        //    structureditemsview.FooterTemplate = footerTemplate;
        //    return structureditemsview;
        //}
        public static T ItemsLayout<T>(this T structureditemsview, IItemsLayout itemsLayout) where T : IRxStructuredItemsView
        {
            structureditemsview.ItemsLayout = itemsLayout;
            return structureditemsview;
        }

        public static T ItemSizingStrategy<T>(this T structureditemsview, ItemSizingStrategy itemSizingStrategy) where T : IRxStructuredItemsView
        {
            structureditemsview.ItemSizingStrategy = itemSizingStrategy;
            return structureditemsview;
        }
    }
}