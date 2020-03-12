
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxStructuredItemsView
    {
        //Object Header { get; set; }
        //DataTemplate HeaderTemplate { get; set; }
        //Object Footer { get; set; }
        //DataTemplate FooterTemplate { get; set; }
        IItemsLayout ItemsLayout { get; set; }
        ItemSizingStrategy ItemSizingStrategy { get; set; }
    }

    public sealed class RxStructuredItemsView : RxView<Xamarin.Forms.StructuredItemsView>, IRxStructuredItemsView
    {
        public RxStructuredItemsView()
        {

        }

        //public Object Header { get; set; } = (Object)StructuredItemsView.HeaderProperty.DefaultValue;
        //public DataTemplate HeaderTemplate { get; set; } = (DataTemplate)StructuredItemsView.HeaderTemplateProperty.DefaultValue;
        //public Object Footer { get; set; } = (Object)StructuredItemsView.FooterProperty.DefaultValue;
        //public DataTemplate FooterTemplate { get; set; } = (DataTemplate)StructuredItemsView.FooterTemplateProperty.DefaultValue;
        public IItemsLayout ItemsLayout { get; set; } = (IItemsLayout)StructuredItemsView.ItemsLayoutProperty.DefaultValue;
        public ItemSizingStrategy ItemSizingStrategy { get; set; } = (ItemSizingStrategy)StructuredItemsView.ItemSizingStrategyProperty.DefaultValue;

        protected override void OnUpdate()
        {
            //NativeControl.Header = Header;
            //NativeControl.HeaderTemplate = HeaderTemplate;
            //NativeControl.Footer = Footer;
            //NativeControl.FooterTemplate = FooterTemplate;
            NativeControl.ItemsLayout = ItemsLayout;
            NativeControl.ItemSizingStrategy = ItemSizingStrategy;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
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
