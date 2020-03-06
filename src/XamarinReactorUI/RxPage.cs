
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxPage
    {
        ImageSource BackgroundImageSource { get; set; }
        bool IsBusy { get; set; }
        Thickness Padding { get; set; }
        string Title { get; set; }
        ImageSource IconImageSource { get; set; }
    }

    public abstract class RxPage<T> : RxVisualElement<T>, IRxPage where T : Xamarin.Forms.Page, new()
    {
        public RxPage()
        {

        }

        public ImageSource BackgroundImageSource { get; set; } = (ImageSource)Page.BackgroundImageSourceProperty.DefaultValue;
        public bool IsBusy { get; set; } = (bool)Page.IsBusyProperty.DefaultValue;
        public Thickness Padding { get; set; } = (Thickness)Page.PaddingProperty.DefaultValue;
        public string Title { get; set; } = (string)Page.TitleProperty.DefaultValue;
        public ImageSource IconImageSource { get; set; } = (ImageSource)Page.IconImageSourceProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.BackgroundImageSource = BackgroundImageSource;
            NativeControl.IsBusy = IsBusy;
            NativeControl.Padding = Padding;
            NativeControl.Title = Title;
            NativeControl.IconImageSource = IconImageSource;

            base.OnUpdate();
        }
    }

    public static class RxPageExtensions
    {
        public static T BackgroundImageSource<T>(this T page, ImageSource backgroundImageSource) where T : IRxPage
        {
            page.BackgroundImageSource = backgroundImageSource;
            return page;
        }


        public static T BackgroundImage<T>(this T page, string file) where T : IRxPage
        {
            page.BackgroundImageSource = ImageSource.FromFile(file);
            return page;
        }
        public static T BackgroundImage<T>(this T page, string fileAndroid, string fileiOS) where T : IRxPage
        {
            page.BackgroundImageSource = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return page;
        }
        public static T BackgroundImage<T>(this T page, string resourceName, Assembly sourceAssembly) where T : IRxPage
        {
            page.BackgroundImageSource = ImageSource.FromResource(resourceName, sourceAssembly);
            return page;
        }
        public static T BackgroundImage<T>(this T page, Uri imageUri) where T : IRxPage
        {
            page.BackgroundImageSource = ImageSource.FromUri(imageUri);
            return page;
        }
        public static T BackgroundImage<T>(this T page, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxPage
        {
            page.BackgroundImageSource = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return page;
        }
        public static T BackgroundImage<T>(this T page, Func<Stream> imageStream) where T : IRxPage
        {
            page.BackgroundImageSource = ImageSource.FromStream(imageStream);
            return page;
        }


        public static T IsBusy<T>(this T page, bool isBusy) where T : IRxPage
        {
            page.IsBusy = isBusy;
            return page;
        }



        public static T Padding<T>(this T page, Thickness padding) where T : IRxPage
        {
            page.Padding = padding;
            return page;
        }
        public static T Padding<T>(this T page, double leftRight, double topBottom) where T : IRxPage
        {
            page.Padding = new Thickness(leftRight, topBottom);
            return page;
        }
        public static T Padding<T>(this T page, double uniformSize) where T : IRxPage
        {
            page.Padding = new Thickness(uniformSize);
            return page;
        }



        public static T Title<T>(this T page, string title) where T : IRxPage
        {
            page.Title = title;
            return page;
        }



        public static T IconImageSource<T>(this T page, ImageSource iconImageSource) where T : IRxPage
        {
            page.IconImageSource = iconImageSource;
            return page;
        }


        public static T IconImage<T>(this T page, string file) where T : IRxPage
        {
            page.IconImageSource = ImageSource.FromFile(file);
            return page;
        }
        public static T IconImage<T>(this T page, string fileAndroid, string fileiOS) where T : IRxPage
        {
            page.IconImageSource = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return page;
        }
        public static T IconImage<T>(this T page, string resourceName, Assembly sourceAssembly) where T : IRxPage
        {
            page.IconImageSource = ImageSource.FromResource(resourceName, sourceAssembly);
            return page;
        }
        public static T IconImage<T>(this T page, Uri imageUri) where T : IRxPage
        {
            page.IconImageSource = ImageSource.FromUri(imageUri);
            return page;
        }
        public static T IconImage<T>(this T page, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxPage
        {
            page.IconImageSource = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return page;
        }
        public static T IconImage<T>(this T page, Func<Stream> imageStream) where T : IRxPage
        {
            page.IconImageSource = ImageSource.FromStream(imageStream);
            return page;
        }


    }


}
