
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxToolbarItem
    {
        bool IsDestructive { get; set; }
        ImageSource IconImageSource { get; set; }
        string Text { get; set; }
        ToolbarItemOrder Order { get; set; }
        int Priority { get; set; }
        Action ClickedAction { get; set; }
    }

    public class RxToolbarItem : RxBaseMenuItem<ToolbarItem>, IRxToolbarItem
    {
        public RxToolbarItem()
        {

        }

        public RxToolbarItem(string text)
        {
            Text = text;
        }

        public RxToolbarItem(Action<ToolbarItem> componentRefAction)
            : base(componentRefAction)
        {

        }

        public bool IsDestructive { get; set; } = (bool)MenuItem.IsDestructiveProperty.DefaultValue;
        public ImageSource IconImageSource { get; set; } = (ImageSource)MenuItem.IconImageSourceProperty.DefaultValue;
        public string Text { get; set; } = (string)MenuItem.TextProperty.DefaultValue;
        public ToolbarItemOrder Order { get; set; }
        public int Priority { get; set; }
        public Action ClickedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.IsDestructive = IsDestructive;
            NativeControl.IconImageSource = IconImageSource;
            NativeControl.Text = Text;
            NativeControl.Priority = Priority;
            NativeControl.Order = Order;

            if (ClickedAction != null)
                NativeControl.Clicked += NativeControl_Clicked;

            base.OnUpdate();
        }

        private void NativeControl_Clicked(object sender, EventArgs e)
        {
            ClickedAction?.Invoke();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            NativeControl.Clicked -= NativeControl_Clicked;
            base.OnMigrated(newNode);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxToolbarItemExtensions
    {
        public static T OnClicked<T>(this T menuitem, Action action) where T : IRxToolbarItem
        {
            menuitem.ClickedAction = action;
            return menuitem;
        }

        public static T Order<T>(this T menuitem, ToolbarItemOrder order) where T : IRxToolbarItem
        {
            menuitem.Order = order;
            return menuitem;
        }

        public static T Priority<T>(this T menuitem, int priority) where T : IRxToolbarItem
        {
            menuitem.Priority = priority;
            return menuitem;
        }

        public static T IsDestructive<T>(this T menuitem, bool isDestructive) where T : IRxToolbarItem
        {
            menuitem.IsDestructive = isDestructive;
            return menuitem;
        }

        public static T IconImageSource<T>(this T menuitem, ImageSource iconImageSource) where T : IRxToolbarItem
        {
            menuitem.IconImageSource = iconImageSource;
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, string file) where T : IRxToolbarItem
        {
            menuitem.IconImageSource = ImageSource.FromFile(file);
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, string fileAndroid, string fileiOS) where T : IRxToolbarItem
        {
            menuitem.IconImageSource = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, string resourceName, Assembly sourceAssembly) where T : IRxToolbarItem
        {
            menuitem.IconImageSource = ImageSource.FromResource(resourceName, sourceAssembly);
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, Uri imageUri) where T : IRxToolbarItem
        {
            menuitem.IconImageSource = ImageSource.FromUri(imageUri);
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxToolbarItem
        {
            menuitem.IconImageSource = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return menuitem;
        }

        public static T IconImage<T>(this T menuitem, Func<Stream> imageStream) where T : IRxToolbarItem
        {
            menuitem.IconImageSource = ImageSource.FromStream(imageStream);
            return menuitem;
        }

        public static T Text<T>(this T menuitem, string text) where T : IRxToolbarItem
        {
            menuitem.Text = text;
            return menuitem;
        }
    }

}
