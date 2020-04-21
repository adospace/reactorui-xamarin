
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxBaseShellItem : IRxNavigableElement
    {
        ImageSource FlyoutIcon { get; set; }
        ImageSource Icon { get; set; }
        bool IsEnabled { get; set; }
        string Title { get; set; }
        int TabIndex { get; set; }
        bool IsTabStop { get; set; }
        string Route { get; set; }
    }

    public abstract class RxBaseShellItem<T> : RxNavigableElement<T>, IRxBaseShellItem where T : Xamarin.Forms.BaseShellItem, new()
    {
        public RxBaseShellItem()
        {

        }

        public RxBaseShellItem(string title)
        {
            Title = title;
        }

        public RxBaseShellItem(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public ImageSource FlyoutIcon { get; set; } = (ImageSource)BaseShellItem.FlyoutIconProperty.DefaultValue;
        public ImageSource Icon { get; set; } = (ImageSource)BaseShellItem.IconProperty.DefaultValue;
        public bool IsEnabled { get; set; } = (bool)BaseShellItem.IsEnabledProperty.DefaultValue;
        public string Title { get; set; } = (string)BaseShellItem.TitleProperty.DefaultValue;
        public int TabIndex { get; set; } = (int)BaseShellItem.TabIndexProperty.DefaultValue;
        public bool IsTabStop { get; set; } = (bool)BaseShellItem.IsTabStopProperty.DefaultValue;
        public string Route { get; set; }


        protected override void OnUpdate()
        {
            NativeControl.FlyoutIcon = FlyoutIcon;
            NativeControl.Icon = Icon;
            NativeControl.IsEnabled = IsEnabled;
            NativeControl.Title = Title;
            NativeControl.TabIndex = TabIndex;
            NativeControl.IsTabStop = IsTabStop;
            if (Route != null)
                NativeControl.Route = Route;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxBaseShellItemExtensions
    {
        public static T FlyoutIcon<T>(this T baseshellitem, ImageSource flyoutIcon) where T : IRxBaseShellItem
        {
            baseshellitem.FlyoutIcon = flyoutIcon;
            return baseshellitem;
        }


        public static T Flyo<T>(this T baseshellitem, string file) where T : IRxBaseShellItem
        {
            baseshellitem.FlyoutIcon = ImageSource.FromFile(file);
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, string fileAndroid, string fileiOS) where T : IRxBaseShellItem
        {
            baseshellitem.FlyoutIcon = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, string resourceName, Assembly sourceAssembly) where T : IRxBaseShellItem
        {
            baseshellitem.FlyoutIcon = ImageSource.FromResource(resourceName, sourceAssembly);
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, Uri imageUri) where T : IRxBaseShellItem
        {
            baseshellitem.FlyoutIcon = ImageSource.FromUri(imageUri);
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxBaseShellItem
        {
            baseshellitem.FlyoutIcon = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, Func<Stream> imageStream) where T : IRxBaseShellItem
        {
            baseshellitem.FlyoutIcon = ImageSource.FromStream(imageStream);
            return baseshellitem;
        }


        public static T Icon<T>(this T baseshellitem, ImageSource icon) where T : IRxBaseShellItem
        {
            baseshellitem.Icon = icon;
            return baseshellitem;
        }


        public static T Icon<T>(this T baseshellitem, string file) where T : IRxBaseShellItem
        {
            baseshellitem.Icon = ImageSource.FromFile(file);
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, string fileAndroid, string fileiOS) where T : IRxBaseShellItem
        {
            baseshellitem.Icon = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, string resourceName, Assembly sourceAssembly) where T : IRxBaseShellItem
        {
            baseshellitem.Icon = ImageSource.FromResource(resourceName, sourceAssembly);
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, Uri imageUri) where T : IRxBaseShellItem
        {
            baseshellitem.Icon = ImageSource.FromUri(imageUri);
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxBaseShellItem
        {
            baseshellitem.Icon = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, Func<Stream> imageStream) where T : IRxBaseShellItem
        {
            baseshellitem.Icon = ImageSource.FromStream(imageStream);
            return baseshellitem;
        }


        public static T IsEnabled<T>(this T baseshellitem, bool isEnabled) where T : IRxBaseShellItem
        {
            baseshellitem.IsEnabled = isEnabled;
            return baseshellitem;
        }



        public static T Title<T>(this T baseshellitem, string title) where T : IRxBaseShellItem
        {
            baseshellitem.Title = title;
            return baseshellitem;
        }



        public static T TabIndex<T>(this T baseshellitem, int tabIndex) where T : IRxBaseShellItem
        {
            baseshellitem.TabIndex = tabIndex;
            return baseshellitem;
        }



        public static T IsTabStop<T>(this T baseshellitem, bool isTabStop) where T : IRxBaseShellItem
        {
            baseshellitem.IsTabStop = isTabStop;
            return baseshellitem;
        }



    }

}
