using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxPage : IRxVisualElement
    {
        ImageSource BackgroundImageSource { get; set; }
        bool IsBusy { get; set; }
        Thickness Padding { get; set; }
        string Title { get; set; }
        ImageSource IconImageSource { get; set; }
        Action BackButtonAction { get; set; }
        bool IsBackButtonEnabled { get; set; }
        //Action<SizeChangedEventArgs> SizeChangedAction { get; set; }
    }

    public abstract class RxPage<T> : RxVisualElement<T>, IRxPage where T : Page, new()
    {
        public RxPage()
        {
        }

        public RxPage(string title)
        {
            Title = title;
        }

        public RxPage(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public ImageSource BackgroundImageSource { get; set; } = (ImageSource)Page.BackgroundImageSourceProperty.DefaultValue;
        public bool IsBusy { get; set; } = (bool)Page.IsBusyProperty.DefaultValue;
        public Thickness Padding { get; set; } = (Thickness)Page.PaddingProperty.DefaultValue;
        public string Title { get; set; } = (string)Page.TitleProperty.DefaultValue;
        public ImageSource IconImageSource { get; set; } = (ImageSource)Page.IconImageSourceProperty.DefaultValue;
        //public Action<SizeChangedEventArgs> SizeChangedAction { get; set; }

        public Action BackButtonAction { get; set; }
        public bool IsBackButtonEnabled { get; set; } = true;

        private Command _backButtonCommand;
        public Command BackButtonCommand
        {
            get
            {
                if (BackButtonAction != null)
                {
                    _backButtonCommand = _backButtonCommand ?? new Command(BackButtonAction);
                }

                return _backButtonCommand;
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
        {
            if (childNativeControl is SearchHandler handler)
            {
                Shell.SetSearchHandler(NativeControl, handler);
            }

            base.OnAddChild(widget, childNativeControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
            if (childNativeControl is SearchHandler _)
            {
                Shell.SetSearchHandler(NativeControl, null);
            }

            base.OnRemoveChild(widget, childNativeControl);
        }

        protected override void OnUpdate()
        {
            if (NativeControl.BackgroundImageSource != BackgroundImageSource) NativeControl.BackgroundImageSource = BackgroundImageSource;
            if (NativeControl.IsBusy != IsBusy) NativeControl.IsBusy = IsBusy;
            if (NativeControl.Padding != Padding) NativeControl.Padding = Padding;
            if (NativeControl.Title != Title) NativeControl.Title = Title;
            if (NativeControl.IconImageSource != IconImageSource) NativeControl.IconImageSource = IconImageSource;


            //if (SizeChangedAction != null)
            //    NativeControl.SizeChanged += NativeControl_SizeChanged;

            NativeControl.SetValue(Shell.BackButtonBehaviorProperty, new BackButtonBehavior()
            { 
                IsEnabled = IsBackButtonEnabled,
                Command = BackButtonCommand
            });

            base.OnUpdate();
        }

        //private void NativeControl_SizeChanged(object sender, EventArgs e)
        //{
        //    var page = (T)sender;
        //    SizeChangedAction?.Invoke(new SizeChangedEventArgs(new Size(page.Width, page.Height)));
        //}

        protected override void OnUnmount()
        {
            //if (NativeControl != null)
            //    NativeControl.SizeChanged -= NativeControl_SizeChanged;

            base.OnUnmount();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            //if (NativeControl != null)
            //    NativeControl.SizeChanged -= NativeControl_SizeChanged;
            
            base.OnMigrated(newNode);
        }
    }

    public static class RxPageExtensions
    {
        //public static T OnSizeChanged<T>(this T page, Action<SizeChangedEventArgs> action) where T : IRxPage
        //{
        //    page.SizeChangedAction = action;
        //    return page;
        //}

        public static T OnBackButtonClicked<T>(this T page, Action action) where T : IRxPage
        {
            page.BackButtonAction = action;
            return page;
        }

        public static T IsBackButtonEnabled<T>(this T page, bool enabled) where T : IRxPage
        {
            page.IsBackButtonEnabled = enabled;
            return page;
        }

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

        public static T Padding<T>(this T page, double left, double top, double right, double bottom) where T : IRxPage
        {
            page.Padding = new Thickness(left, top, right, bottom);
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