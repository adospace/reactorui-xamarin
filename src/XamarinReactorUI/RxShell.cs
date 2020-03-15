using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxShell
    {
        FlyoutBehavior FlyoutBehavior { get; set; }

        //DataTemplate MenuItemTemplate { get; set; }
        //DataTemplate ItemTemplate { get; set; }
        //Color BackgroundColor { get; set; }
        //ShellItem CurrentItem { get; set; }
        ImageSource FlyoutBackgroundImage { get; set; }

        Aspect FlyoutBackgroundImageAspect { get; set; }
        Color FlyoutBackgroundColor { get; set; }
        FlyoutHeaderBehavior FlyoutHeaderBehavior { get; set; }
        object FlyoutHeader { get; set; }

        //DataTemplate FlyoutHeaderTemplate { get; set; }
        bool FlyoutIsPresented { get; set; }

        ImageSource FlyoutIcon { get; set; }
        ScrollMode FlyoutVerticalScrollMode { get; set; }
    }

    public sealed class RxShell : RxPage<Xamarin.Forms.Shell>, IRxShell, IEnumerable<VisualNode>
    {
        private readonly Shell _shell;
        private List<VisualNode> _contents = new List<VisualNode>();
        private readonly Dictionary<Element, ShellItem> _elementItemMap = new Dictionary<Element, ShellItem>();

        public RxShell()
        {
        }

        public RxShell(Shell shell)
        {
            _shell = shell;
        }

        public RxShell(Action<Shell> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override void OnMount()
        {
            _nativeControl = _shell;
            base.OnMount();
        }

        public void Add(VisualNode child)
        {
            _contents.Add(child);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        protected override void OnAddChild(VisualNode widget, Element childControl)
        {
            if (childControl is ShellItem item)
                NativeControl.Items.Add(item);
            else if (childControl is Page page)
                NativeControl.Items.Add(new ShellContent() { Content = page });
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            _elementItemMap[childControl] = NativeControl.Items[NativeControl.Items.Count - 1];

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, Element childControl)
        {
            NativeControl.Items.Remove(_elementItemMap[childControl]);

            base.OnRemoveChild(widget, childControl);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _contents;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public FlyoutBehavior FlyoutBehavior { get; set; } = (FlyoutBehavior)Shell.FlyoutBehaviorProperty.DefaultValue;
        public DataTemplate MenuItemTemplate { get; set; } = (DataTemplate)Shell.MenuItemTemplateProperty.DefaultValue;
        public DataTemplate ItemTemplate { get; set; } = (DataTemplate)Shell.ItemTemplateProperty.DefaultValue;

        //public Color BackgroundColor { get; set; } = (Color)Shell.BackgroundColorProperty.DefaultValue;
        //public ShellItem CurrentItem { get; set; } = (ShellItem)Shell.CurrentItemProperty.DefaultValue;
        public ImageSource FlyoutBackgroundImage { get; set; } = (ImageSource)Shell.FlyoutBackgroundImageProperty.DefaultValue;

        public Aspect FlyoutBackgroundImageAspect { get; set; } = (Aspect)Shell.FlyoutBackgroundImageAspectProperty.DefaultValue;
        public Color FlyoutBackgroundColor { get; set; } = (Color)Shell.FlyoutBackgroundColorProperty.DefaultValue;
        public FlyoutHeaderBehavior FlyoutHeaderBehavior { get; set; } = (FlyoutHeaderBehavior)Shell.FlyoutHeaderBehaviorProperty.DefaultValue;
        public object FlyoutHeader { get; set; } = (object)Shell.FlyoutHeaderProperty.DefaultValue;
        public DataTemplate FlyoutHeaderTemplate { get; set; } = (DataTemplate)Shell.FlyoutHeaderTemplateProperty.DefaultValue;
        public bool FlyoutIsPresented { get; set; } = (bool)Shell.FlyoutIsPresentedProperty.DefaultValue;
        public ImageSource FlyoutIcon { get; set; } = (ImageSource)Shell.FlyoutIconProperty.DefaultValue;
        public ScrollMode FlyoutVerticalScrollMode { get; set; } = (ScrollMode)Shell.FlyoutVerticalScrollModeProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.FlyoutBehavior = FlyoutBehavior;
            NativeControl.MenuItemTemplate = MenuItemTemplate;
            NativeControl.ItemTemplate = ItemTemplate;
            //NativeControl.BackgroundColor = BackgroundColor;
            //NativeControl.CurrentItem = CurrentItem;
            NativeControl.FlyoutBackgroundImage = FlyoutBackgroundImage;
            NativeControl.FlyoutBackgroundImageAspect = FlyoutBackgroundImageAspect;
            NativeControl.FlyoutBackgroundColor = FlyoutBackgroundColor;
            NativeControl.FlyoutHeaderBehavior = FlyoutHeaderBehavior;
            NativeControl.FlyoutHeader = FlyoutHeader;
            NativeControl.FlyoutHeaderTemplate = FlyoutHeaderTemplate;
            NativeControl.FlyoutIsPresented = FlyoutIsPresented;
            NativeControl.FlyoutIcon = FlyoutIcon;
            NativeControl.FlyoutVerticalScrollMode = FlyoutVerticalScrollMode;

            base.OnUpdate();
        }
    }

    public static class RxShellExtensions
    {
        public static T FlyoutBehavior<T>(this T shell, FlyoutBehavior flyoutBehavior) where T : IRxShell
        {
            shell.FlyoutBehavior = flyoutBehavior;
            return shell;
        }

        //public static T MenuItemTemplate<T>(this T shell, DataTemplate menuItemTemplate) where T : IRxShell
        //{
        //    shell.MenuItemTemplate = menuItemTemplate;
        //    return shell;
        //}

        //public static T ItemTemplate<T>(this T shell, DataTemplate itemTemplate) where T : IRxShell
        //{
        //    shell.ItemTemplate = itemTemplate;
        //    return shell;
        //}

        //public static T BackgroundColor<T>(this T shell, Color backgroundColor) where T : IRxShell
        //{
        //    shell.BackgroundColor = backgroundColor;
        //    return shell;
        //}

        //public static T CurrentItem<T>(this T shell, ShellItem currentItem) where T : IRxShell
        //{
        //    shell.CurrentItem = currentItem;
        //    return shell;
        //}

        public static T FlyoutBackgroundImage<T>(this T shell, ImageSource flyoutBackgroundImage) where T : IRxShell
        {
            shell.FlyoutBackgroundImage = flyoutBackgroundImage;
            return shell;
        }

        public static T FlyoutBackgroun<T>(this T shell, string file) where T : IRxShell
        {
            shell.FlyoutBackgroundImage = ImageSource.FromFile(file);
            return shell;
        }

        public static T FlyoutBackgroun<T>(this T shell, string fileAndroid, string fileiOS) where T : IRxShell
        {
            shell.FlyoutBackgroundImage = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return shell;
        }

        public static T FlyoutBackgroun<T>(this T shell, string resourceName, Assembly sourceAssembly) where T : IRxShell
        {
            shell.FlyoutBackgroundImage = ImageSource.FromResource(resourceName, sourceAssembly);
            return shell;
        }

        public static T FlyoutBackgroun<T>(this T shell, Uri imageUri) where T : IRxShell
        {
            shell.FlyoutBackgroundImage = ImageSource.FromUri(imageUri);
            return shell;
        }

        public static T FlyoutBackgroun<T>(this T shell, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxShell
        {
            shell.FlyoutBackgroundImage = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return shell;
        }

        public static T FlyoutBackgroun<T>(this T shell, Func<Stream> imageStream) where T : IRxShell
        {
            shell.FlyoutBackgroundImage = ImageSource.FromStream(imageStream);
            return shell;
        }

        public static T FlyoutBackgroundImageAspect<T>(this T shell, Aspect flyoutBackgroundImageAspect) where T : IRxShell
        {
            shell.FlyoutBackgroundImageAspect = flyoutBackgroundImageAspect;
            return shell;
        }

        public static T FlyoutBackgroundColor<T>(this T shell, Color flyoutBackgroundColor) where T : IRxShell
        {
            shell.FlyoutBackgroundColor = flyoutBackgroundColor;
            return shell;
        }

        public static T FlyoutHeaderBehavior<T>(this T shell, FlyoutHeaderBehavior flyoutHeaderBehavior) where T : IRxShell
        {
            shell.FlyoutHeaderBehavior = flyoutHeaderBehavior;
            return shell;
        }

        public static T FlyoutHeader<T>(this T shell, object flyoutHeader) where T : IRxShell
        {
            shell.FlyoutHeader = flyoutHeader;
            return shell;
        }

        //public static T FlyoutHeaderTemplate<T>(this T shell, DataTemplate flyoutHeaderTemplate) where T : IRxShell
        //{
        //    shell.FlyoutHeaderTemplate = flyoutHeaderTemplate;
        //    return shell;
        //}

        public static T FlyoutIsPresented<T>(this T shell, bool flyoutIsPresented) where T : IRxShell
        {
            shell.FlyoutIsPresented = flyoutIsPresented;
            return shell;
        }

        public static T FlyoutIcon<T>(this T shell, ImageSource flyoutIcon) where T : IRxShell
        {
            shell.FlyoutIcon = flyoutIcon;
            return shell;
        }

        public static T FlyoutIcon<T>(this T shell, string file) where T : IRxShell
        {
            shell.FlyoutIcon = ImageSource.FromFile(file);
            return shell;
        }

        public static T FlyoutIcon<T>(this T shell, string fileAndroid, string fileiOS) where T : IRxShell
        {
            shell.FlyoutIcon = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return shell;
        }

        public static T FlyoutIcon<T>(this T shell, string resourceName, Assembly sourceAssembly) where T : IRxShell
        {
            shell.FlyoutIcon = ImageSource.FromResource(resourceName, sourceAssembly);
            return shell;
        }

        public static T FlyoutIcon<T>(this T shell, Uri imageUri) where T : IRxShell
        {
            shell.FlyoutIcon = ImageSource.FromUri(imageUri);
            return shell;
        }

        public static T FlyoutIcon<T>(this T shell, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxShell
        {
            shell.FlyoutIcon = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return shell;
        }

        public static T FlyoutIcon<T>(this T shell, Func<Stream> imageStream) where T : IRxShell
        {
            shell.FlyoutIcon = ImageSource.FromStream(imageStream);
            return shell;
        }

        public static T FlyoutVerticalScrollMode<T>(this T shell, ScrollMode flyoutVerticalScrollMode) where T : IRxShell
        {
            shell.FlyoutVerticalScrollMode = flyoutVerticalScrollMode;
            return shell;
        }
    }
}