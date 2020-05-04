using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using XamarinReactorUI.Internals;

namespace XamarinReactorUI
{
    public interface IRxSearchHandler
    {
        Keyboard Keyboard { get; set; }
        TextAlignment HorizontalTextAlignment { get; set; }
        TextAlignment VerticalTextAlignment { get; set; }
        Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        Color CancelButtonColor { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        FontAttributes FontAttributes { get; set; }
        string Placeholder { get; set; }
        Color PlaceholderColor { get; set; }
        Color BackgroundColor { get; set; }
        string ClearIconHelpText { get; set; }
        string ClearIconName { get; set; }
        ImageSource ClearIcon { get; set; }

        //object ClearPlaceholderCommandParameter { get; set; }
        //ICommand ClearPlaceholderCommand { get; set; }
        bool ClearPlaceholderEnabled { get; set; }

        string ClearPlaceholderHelpText { get; set; }
        ImageSource ClearPlaceholderIcon { get; set; }
        string ClearPlaceholderName { get; set; }

        //object CommandParameter { get; set; }
        //ICommand Command { get; set; }
        string DisplayMemberName { get; set; }

        bool IsSearchEnabled { get; set; }

        //IEnumerable ItemsSource { get; set; }
        //DataTemplate ItemTemplate { get; set; }
        string QueryIconHelpText { get; set; }

        string QueryIconName { get; set; }
        ImageSource QueryIcon { get; set; }
        string Query { get; set; }
        SearchBoxVisibility SearchBoxVisibility { get; set; }
        bool ShowsResults { get; set; }

        Action<string> SearchAction { get; set; }
        Action<string> QueryChangedAction { get; set; }
    }

    public interface IRxSearchHandler<I> : IRxSearchHandler
    {
        IEnumerable<I> Results { get; set; }
        Func<I, VisualNode> Template { get; set; }
    }

    public class RxSearchHandler<T, I> : VisualNode<T>, IRxSearchHandler<I> where T : Xamarin.Forms.SearchHandler, new()
    {
        public RxSearchHandler()
        {
        }

        public RxSearchHandler(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Keyboard Keyboard { get; set; } = (Keyboard)SearchHandler.KeyboardProperty.DefaultValue;
        public TextAlignment HorizontalTextAlignment { get; set; } = (TextAlignment)SearchHandler.HorizontalTextAlignmentProperty.DefaultValue;
        public TextAlignment VerticalTextAlignment { get; set; } = (TextAlignment)SearchHandler.VerticalTextAlignmentProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)SearchHandler.TextColorProperty.DefaultValue;
        public double CharacterSpacing { get; set; } = (double)SearchHandler.CharacterSpacingProperty.DefaultValue;
        public Color CancelButtonColor { get; set; } = (Color)SearchHandler.CancelButtonColorProperty.DefaultValue;
        public string FontFamily { get; set; } = (string)SearchHandler.FontFamilyProperty.DefaultValue;
        public double FontSize { get; set; } = (double)SearchHandler.FontSizeProperty.DefaultValue;
        public FontAttributes FontAttributes { get; set; } = (FontAttributes)SearchHandler.FontAttributesProperty.DefaultValue;
        public string Placeholder { get; set; } = (string)SearchHandler.PlaceholderProperty.DefaultValue;
        public Color PlaceholderColor { get; set; } = (Color)SearchHandler.PlaceholderColorProperty.DefaultValue;
        public Color BackgroundColor { get; set; } = (Color)SearchHandler.BackgroundColorProperty.DefaultValue;
        public string ClearIconHelpText { get; set; } = (string)SearchHandler.ClearIconHelpTextProperty.DefaultValue;
        public string ClearIconName { get; set; } = (string)SearchHandler.ClearIconNameProperty.DefaultValue;
        public ImageSource ClearIcon { get; set; } = (ImageSource)SearchHandler.ClearIconProperty.DefaultValue;

        //public object ClearPlaceholderCommandParameter { get; set; } = (object)SearchHandler.ClearPlaceholderCommandParameterProperty.DefaultValue;
        //public ICommand ClearPlaceholderCommand { get; set; } = (ICommand)SearchHandler.ClearPlaceholderCommandProperty.DefaultValue;
        public bool ClearPlaceholderEnabled { get; set; } = (bool)SearchHandler.ClearPlaceholderEnabledProperty.DefaultValue;

        public string ClearPlaceholderHelpText { get; set; } = (string)SearchHandler.ClearPlaceholderHelpTextProperty.DefaultValue;
        public ImageSource ClearPlaceholderIcon { get; set; } = (ImageSource)SearchHandler.ClearPlaceholderIconProperty.DefaultValue;
        public string ClearPlaceholderName { get; set; } = (string)SearchHandler.ClearPlaceholderNameProperty.DefaultValue;

        //public object CommandParameter { get; set; } = (object)SearchHandler.CommandParameterProperty.DefaultValue;
        //public ICommand Command { get; set; } = (ICommand)SearchHandler.CommandProperty.DefaultValue;
        public string DisplayMemberName { get; set; } = (string)SearchHandler.DisplayMemberNameProperty.DefaultValue;

        public bool IsSearchEnabled { get; set; } = (bool)SearchHandler.IsSearchEnabledProperty.DefaultValue;

        //public IEnumerable ItemsSource { get; set; } = (IEnumerable)SearchHandler.ItemsSourceProperty.DefaultValue;
        //public DataTemplate ItemTemplate { get; set; } = (DataTemplate)SearchHandler.ItemTemplateProperty.DefaultValue;
        public string QueryIconHelpText { get; set; } = (string)SearchHandler.QueryIconHelpTextProperty.DefaultValue;

        public string QueryIconName { get; set; } = (string)SearchHandler.QueryIconNameProperty.DefaultValue;
        public ImageSource QueryIcon { get; set; } = (ImageSource)SearchHandler.QueryIconProperty.DefaultValue;
        public string Query { get; set; } = (string)SearchHandler.QueryProperty.DefaultValue;
        public SearchBoxVisibility SearchBoxVisibility { get; set; } = (SearchBoxVisibility)SearchHandler.SearchBoxVisibilityProperty.DefaultValue;
        public bool ShowsResults { get; set; } = (bool)SearchHandler.ShowsResultsProperty.DefaultValue;
        public Action<string> SearchAction { get; set; }
        public Action<string> QueryChangedAction { get; set; }

        public IEnumerable<I> Results { get; set; }
        public Func<I, VisualNode> Template { get; set; }

        private class ItemTemplateNode : VisualNode
        {
            private readonly ItemTemplatePresenter _presenter = null;

            public ItemTemplateNode(VisualNode root, ItemTemplatePresenter presenter)
            {
                Root = root;
                _presenter = presenter;
            }

            private VisualNode _root;

            public VisualNode Root
            {
                get => _root;
                set
                {
                    if (_root != value)
                    {
                        _root = value;
                        Invalidate();
                    }
                }
            }

            protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
            {
                if (nativeControl is View view)
                    _presenter.Content = view;
                else
                {
                    throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
                }
            }

            protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
            {
            }

            protected override IEnumerable<VisualNode> RenderChildren()
            {
                yield return Root;
            }

            protected internal override void OnLayoutCycleRequested()
            {
                Layout();
                base.OnLayoutCycleRequested();
            }
        }

        private class ItemTemplatePresenter : ContentView
        {
            private ItemTemplateNode _itemTemplateNode;
            private readonly CustomDataTemplate _template;

            public ItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                var item = (I)BindingContext;
                VisualNode newRoot = null;
                if (item != null)
                {
                    newRoot = _template.Owner.Template(item);
                    _itemTemplateNode = new ItemTemplateNode(newRoot, this);
                    _itemTemplateNode.Layout();
                }

                base.OnBindingContextChanged();
            }
        }

        private class CustomDataTemplate
        {
            public DataTemplate DataTemplate { get; }
            public RxSearchHandler<T, I> Owner { get; set; }

            public CustomDataTemplate(RxSearchHandler<T, I> owner)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
            }
        }

        private CustomDataTemplate _customDataTemplate;        

        protected override void OnUpdate()
        {
            if (NativeControl.ItemsSource is ObservableItemsSource<I> existingCollection &&
                existingCollection.ItemsSource == Results)
            {
                _customDataTemplate.Owner = this;
                existingCollection.NotifyCollectionChanged();
            }
            else if (Results != null)
            {
                _customDataTemplate = new CustomDataTemplate(this);
                NativeControl.ItemsSource = ObservableItemsSource<I>.Create(Results);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }
            else
            {
                NativeControl.ItemsSource = null;
                NativeControl.ItemTemplate = null;
            }

            NativeControl.Keyboard = Keyboard;
            NativeControl.HorizontalTextAlignment = HorizontalTextAlignment;
            NativeControl.VerticalTextAlignment = VerticalTextAlignment;
            NativeControl.TextColor = TextColor;
            NativeControl.CharacterSpacing = CharacterSpacing;
            NativeControl.CancelButtonColor = CancelButtonColor;
            NativeControl.FontFamily = FontFamily;
            NativeControl.FontSize = FontSize;
            NativeControl.FontAttributes = FontAttributes;
            NativeControl.Placeholder = Placeholder;
            NativeControl.PlaceholderColor = PlaceholderColor;
            NativeControl.BackgroundColor = BackgroundColor;
            NativeControl.ClearIconHelpText = ClearIconHelpText;
            NativeControl.ClearIconName = ClearIconName;
            NativeControl.ClearIcon = ClearIcon;
            //NativeControl.ClearPlaceholderCommandParameter = ClearPlaceholderCommandParameter;
            //NativeControl.ClearPlaceholderCommand = ClearPlaceholderCommand;
            NativeControl.ClearPlaceholderEnabled = ClearPlaceholderEnabled;
            NativeControl.ClearPlaceholderHelpText = ClearPlaceholderHelpText;
            NativeControl.ClearPlaceholderIcon = ClearPlaceholderIcon;
            NativeControl.ClearPlaceholderName = ClearPlaceholderName;
            //NativeControl.CommandParameter = CommandParameter;
            //NativeControl.Command = Command;
            NativeControl.DisplayMemberName = DisplayMemberName;
            NativeControl.IsSearchEnabled = IsSearchEnabled;
            //NativeControl.ItemsSource = ItemsSource;
            //NativeControl.ItemTemplate = ItemTemplate;
            NativeControl.QueryIconHelpText = QueryIconHelpText;
            NativeControl.QueryIconName = QueryIconName;
            NativeControl.QueryIcon = QueryIcon;
            NativeControl.Query = Query;
            NativeControl.SearchBoxVisibility = SearchBoxVisibility;
            NativeControl.ShowsResults = ShowsResults;

            NativeControl.Command = new ActionCommand(() => SearchAction?.Invoke(Query));

            if (QueryChangedAction != null)
            {
                NativeControl.PropertyChanged += NativeControl_PropertyChanged;
            }

            base.OnUpdate();
        }

        private void NativeControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Query" && NativeControl != null)
            {
                QueryChangedAction?.Invoke(NativeControl.Query);
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;

            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxSearchHandler<I> : RxSearchHandler<Xamarin.Forms.SearchHandler, I>
    {
        public RxSearchHandler()
        {
        }

        public RxSearchHandler(Action<SearchHandler> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxSearchHandlerExtensions
    {
        public static T OnSearch<T>(this T searchHandler, Action<string> action) where T : IRxSearchHandler
        {
            searchHandler.SearchAction = action;
            return searchHandler;
        }

        public static T OnQueryChanged<T>(this T searchHandler, Action<string> action) where T : IRxSearchHandler
        {
            searchHandler.QueryChangedAction = action;
            return searchHandler;
        }

        public static T RenderResults<T, I>(this T searchHandler, IEnumerable<I> results) where T : IRxSearchHandler<I>
        {
            searchHandler.Results = results;
            return searchHandler;
        }

        public static T RenderResults<T, I>(this T searchHandler, IEnumerable<I> results, Func<I, VisualNode> template) where T : IRxSearchHandler<I>
        {
            searchHandler.Results = results;
            searchHandler.Template = template;
            return searchHandler;
        }

        public static T Keyboard<T>(this T searchhandler, Keyboard keyboard) where T : IRxSearchHandler
        {
            searchhandler.Keyboard = keyboard;
            return searchhandler;
        }

        public static T HorizontalTextAlignment<T>(this T searchhandler, TextAlignment horizontalTextAlignment) where T : IRxSearchHandler
        {
            searchhandler.HorizontalTextAlignment = horizontalTextAlignment;
            return searchhandler;
        }

        public static T VerticalTextAlignment<T>(this T searchhandler, TextAlignment verticalTextAlignment) where T : IRxSearchHandler
        {
            searchhandler.VerticalTextAlignment = verticalTextAlignment;
            return searchhandler;
        }

        public static T TextColor<T>(this T searchhandler, Color textColor) where T : IRxSearchHandler
        {
            searchhandler.TextColor = textColor;
            return searchhandler;
        }

        public static T CharacterSpacing<T>(this T searchhandler, double characterSpacing) where T : IRxSearchHandler
        {
            searchhandler.CharacterSpacing = characterSpacing;
            return searchhandler;
        }

        public static T CancelButtonColor<T>(this T searchhandler, Color cancelButtonColor) where T : IRxSearchHandler
        {
            searchhandler.CancelButtonColor = cancelButtonColor;
            return searchhandler;
        }

        public static T FontFamily<T>(this T searchhandler, string fontFamily) where T : IRxSearchHandler
        {
            searchhandler.FontFamily = fontFamily;
            return searchhandler;
        }

        public static T FontSize<T>(this T searchhandler, double fontSize) where T : IRxSearchHandler
        {
            searchhandler.FontSize = fontSize;
            return searchhandler;
        }

        public static T FontSize<T>(this T searchhandler, NamedSize size) where T : IRxSearchHandler
        {
            searchhandler.FontSize = Device.GetNamedSize(size, typeof(SearchHandler));
            return searchhandler;
        }

        public static T FontAttributes<T>(this T searchhandler, FontAttributes fontAttributes) where T : IRxSearchHandler
        {
            searchhandler.FontAttributes = fontAttributes;
            return searchhandler;
        }

        public static T Placeholder<T>(this T searchhandler, string placeholder) where T : IRxSearchHandler
        {
            searchhandler.Placeholder = placeholder;
            return searchhandler;
        }

        public static T PlaceholderColor<T>(this T searchhandler, Color placeholderColor) where T : IRxSearchHandler
        {
            searchhandler.PlaceholderColor = placeholderColor;
            return searchhandler;
        }

        public static T BackgroundColor<T>(this T searchhandler, Color backgroundColor) where T : IRxSearchHandler
        {
            searchhandler.BackgroundColor = backgroundColor;
            return searchhandler;
        }

        public static T ClearIconHelpText<T>(this T searchhandler, string clearIconHelpText) where T : IRxSearchHandler
        {
            searchhandler.ClearIconHelpText = clearIconHelpText;
            return searchhandler;
        }

        public static T ClearIconName<T>(this T searchhandler, string clearIconName) where T : IRxSearchHandler
        {
            searchhandler.ClearIconName = clearIconName;
            return searchhandler;
        }

        public static T ClearIcon<T>(this T searchhandler, ImageSource clearIcon) where T : IRxSearchHandler
        {
            searchhandler.ClearIcon = clearIcon;
            return searchhandler;
        }

        public static T ClearIcon<T>(this T searchhandler, string file) where T : IRxSearchHandler
        {
            searchhandler.ClearIcon = ImageSource.FromFile(file);
            return searchhandler;
        }

        public static T ClearIcon<T>(this T searchhandler, string fileAndroid, string fileiOS) where T : IRxSearchHandler
        {
            searchhandler.ClearIcon = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return searchhandler;
        }

        public static T ClearIcon<T>(this T searchhandler, string resourceName, Assembly sourceAssembly) where T : IRxSearchHandler
        {
            searchhandler.ClearIcon = ImageSource.FromResource(resourceName, sourceAssembly);
            return searchhandler;
        }

        public static T ClearIcon<T>(this T searchhandler, Uri imageUri) where T : IRxSearchHandler
        {
            searchhandler.ClearIcon = ImageSource.FromUri(imageUri);
            return searchhandler;
        }

        public static T ClearIcon<T>(this T searchhandler, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxSearchHandler
        {
            searchhandler.ClearIcon = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return searchhandler;
        }

        public static T ClearIcon<T>(this T searchhandler, Func<Stream> imageStream) where T : IRxSearchHandler
        {
            searchhandler.ClearIcon = ImageSource.FromStream(imageStream);
            return searchhandler;
        }

        //public static T ClearPlaceholderCommandParameter<T>(this T searchhandler, object clearPlaceholderCommandParameter) where T : IRxSearchHandler
        //{
        //    searchhandler.ClearPlaceholderCommandParameter = clearPlaceholderCommandParameter;
        //    return searchhandler;
        //}

        //public static T ClearPlaceholderCommand<T>(this T searchhandler, ICommand clearPlaceholderCommand) where T : IRxSearchHandler
        //{
        //    searchhandler.ClearPlaceholderCommand = clearPlaceholderCommand;
        //    return searchhandler;
        //}

        public static T ClearPlaceholderEnabled<T>(this T searchhandler, bool clearPlaceholderEnabled) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderEnabled = clearPlaceholderEnabled;
            return searchhandler;
        }

        public static T ClearPlaceholderHelpText<T>(this T searchhandler, string clearPlaceholderHelpText) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderHelpText = clearPlaceholderHelpText;
            return searchhandler;
        }

        public static T ClearPlaceholderIcon<T>(this T searchhandler, ImageSource clearPlaceholderIcon) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderIcon = clearPlaceholderIcon;
            return searchhandler;
        }

        public static T ClearPlaceholderIcon<T>(this T searchhandler, string file) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderIcon = ImageSource.FromFile(file);
            return searchhandler;
        }

        public static T ClearPlaceholderIcon<T>(this T searchhandler, string fileAndroid, string fileiOS) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderIcon = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return searchhandler;
        }

        public static T ClearPlaceholderIcon<T>(this T searchhandler, string resourceName, Assembly sourceAssembly) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderIcon = ImageSource.FromResource(resourceName, sourceAssembly);
            return searchhandler;
        }

        public static T ClearPlaceholderIcon<T>(this T searchhandler, Uri imageUri) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderIcon = ImageSource.FromUri(imageUri);
            return searchhandler;
        }

        public static T ClearPlaceholderIcon<T>(this T searchhandler, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderIcon = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return searchhandler;
        }

        public static T ClearPlaceholderIcon<T>(this T searchhandler, Func<Stream> imageStream) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderIcon = ImageSource.FromStream(imageStream);
            return searchhandler;
        }

        public static T ClearPlaceholderName<T>(this T searchhandler, string clearPlaceholderName) where T : IRxSearchHandler
        {
            searchhandler.ClearPlaceholderName = clearPlaceholderName;
            return searchhandler;
        }

        //public static T CommandParameter<T>(this T searchhandler, object commandParameter) where T : IRxSearchHandler
        //{
        //    searchhandler.CommandParameter = commandParameter;
        //    return searchhandler;
        //}

        //public static T Command<T>(this T searchhandler, ICommand command) where T : IRxSearchHandler
        //{
        //    searchhandler.Command = command;
        //    return searchhandler;
        //}

        public static T DisplayMemberName<T>(this T searchhandler, string displayMemberName) where T : IRxSearchHandler
        {
            searchhandler.DisplayMemberName = displayMemberName;
            return searchhandler;
        }

        public static T IsSearchEnabled<T>(this T searchhandler, bool isSearchEnabled) where T : IRxSearchHandler
        {
            searchhandler.IsSearchEnabled = isSearchEnabled;
            return searchhandler;
        }

        //public static T ItemsSource<T>(this T searchhandler, IEnumerable itemsSource) where T : IRxSearchHandler
        //{
        //    searchhandler.ItemsSource = itemsSource;
        //    return searchhandler;
        //}

        //public static T ItemTemplate<T>(this T searchhandler, DataTemplate itemTemplate) where T : IRxSearchHandler
        //{
        //    searchhandler.ItemTemplate = itemTemplate;
        //    return searchhandler;
        //}

        public static T QueryIconHelpText<T>(this T searchhandler, string queryIconHelpText) where T : IRxSearchHandler
        {
            searchhandler.QueryIconHelpText = queryIconHelpText;
            return searchhandler;
        }

        public static T QueryIconName<T>(this T searchhandler, string queryIconName) where T : IRxSearchHandler
        {
            searchhandler.QueryIconName = queryIconName;
            return searchhandler;
        }

        public static T QueryIcon<T>(this T searchhandler, ImageSource queryIcon) where T : IRxSearchHandler
        {
            searchhandler.QueryIcon = queryIcon;
            return searchhandler;
        }

        public static T QueryIcon<T>(this T searchhandler, string file) where T : IRxSearchHandler
        {
            searchhandler.QueryIcon = ImageSource.FromFile(file);
            return searchhandler;
        }

        public static T QueryIcon<T>(this T searchhandler, string fileAndroid, string fileiOS) where T : IRxSearchHandler
        {
            searchhandler.QueryIcon = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return searchhandler;
        }

        public static T QueryIcon<T>(this T searchhandler, string resourceName, Assembly sourceAssembly) where T : IRxSearchHandler
        {
            searchhandler.QueryIcon = ImageSource.FromResource(resourceName, sourceAssembly);
            return searchhandler;
        }

        public static T QueryIcon<T>(this T searchhandler, Uri imageUri) where T : IRxSearchHandler
        {
            searchhandler.QueryIcon = ImageSource.FromUri(imageUri);
            return searchhandler;
        }

        public static T QueryIcon<T>(this T searchhandler, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxSearchHandler
        {
            searchhandler.QueryIcon = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return searchhandler;
        }

        public static T QueryIcon<T>(this T searchhandler, Func<Stream> imageStream) where T : IRxSearchHandler
        {
            searchhandler.QueryIcon = ImageSource.FromStream(imageStream);
            return searchhandler;
        }

        public static T Query<T>(this T searchhandler, string query) where T : IRxSearchHandler
        {
            searchhandler.Query = query;
            return searchhandler;
        }

        public static T SearchBoxVisibility<T>(this T searchhandler, SearchBoxVisibility searchBoxVisibility) where T : IRxSearchHandler
        {
            searchhandler.SearchBoxVisibility = searchBoxVisibility;
            return searchhandler;
        }

        public static T ShowsResults<T>(this T searchhandler, bool showsResults) where T : IRxSearchHandler
        {
            searchhandler.ShowsResults = showsResults;
            return searchhandler;
        }
    }
}