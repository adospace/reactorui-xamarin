using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinReactorUI.Internals;

namespace XamarinReactorUI
{
    public interface IRxListView
    {
        bool IsPullToRefreshEnabled { get; set; }
        bool IsRefreshing { get; set; }

        //ICommand RefreshCommand { get; set; }
        //object Header { get; set; }
        //DataTemplate HeaderTemplate { get; set; }
        //object Footer { get; set; }
        //DataTemplate FooterTemplate { get; set; }
        object SelectedItem { get; set; }

        ListViewSelectionMode SelectionMode { get; set; }
        bool HasUnevenRows { get; set; }
        int RowHeight { get; set; }

        //DataTemplate GroupHeaderTemplate { get; set; }
        bool IsGroupingEnabled { get; set; }

        SeparatorVisibility SeparatorVisibility { get; set; }
        Color SeparatorColor { get; set; }
        Color RefreshControlColor { get; set; }
        ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
        ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
        Action RefreshingAction { get; set; }
    }

    public interface IRxListView<I> : IRxListView
    {
        IEnumerable<I> Collection { get; set; }
        Func<I, VisualNode> Template { get; set; }
    }

    public abstract class RxListViewBase<T, I> : RxView<T> where T : ListView, new()
    {
        public RxListViewBase()
        {
        }

        public RxListViewBase(IEnumerable<I> collection)
        {
            Collection = collection;
        }

        public RxListViewBase(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }

        public bool IsPullToRefreshEnabled { get; set; } = (bool)ListView.IsPullToRefreshEnabledProperty.DefaultValue;
        public bool IsRefreshing { get; set; } = (bool)ListView.IsRefreshingProperty.DefaultValue;

        //public ICommand RefreshCommand { get; set; } = (ICommand)ListView.RefreshCommandProperty.DefaultValue;
        //public object Header { get; set; } = (object)ListView.HeaderProperty.DefaultValue;
        //public DataTemplate HeaderTemplate { get; set; } = (DataTemplate)ListView.HeaderTemplateProperty.DefaultValue;
        //public object Footer { get; set; } = (object)ListView.FooterProperty.DefaultValue;
        //public DataTemplate FooterTemplate { get; set; } = (DataTemplate)ListView.FooterTemplateProperty.DefaultValue;
        public object SelectedItem { get; set; } = (object)ListView.SelectedItemProperty.DefaultValue;

        public ListViewSelectionMode SelectionMode { get; set; } = (ListViewSelectionMode)ListView.SelectionModeProperty.DefaultValue;
        public bool HasUnevenRows { get; set; } = (bool)ListView.HasUnevenRowsProperty.DefaultValue;
        public int RowHeight { get; set; } = (int)ListView.RowHeightProperty.DefaultValue;

        //public DataTemplate GroupHeaderTemplate { get; set; } = (DataTemplate)ListView.GroupHeaderTemplateProperty.DefaultValue;
        public bool IsGroupingEnabled { get; set; } = (bool)ListView.IsGroupingEnabledProperty.DefaultValue;

        public SeparatorVisibility SeparatorVisibility { get; set; } = (SeparatorVisibility)ListView.SeparatorVisibilityProperty.DefaultValue;
        public Color SeparatorColor { get; set; } = (Color)ListView.SeparatorColorProperty.DefaultValue;
        public Color RefreshControlColor { get; set; } = (Color)ListView.RefreshControlColorProperty.DefaultValue;
        public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; } = (ScrollBarVisibility)ListView.HorizontalScrollBarVisibilityProperty.DefaultValue;
        public ScrollBarVisibility VerticalScrollBarVisibility { get; set; } = (ScrollBarVisibility)ListView.VerticalScrollBarVisibilityProperty.DefaultValue;
        public Action RefreshingAction { get; set; }

        public IEnumerable<I> Collection { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.IsPullToRefreshEnabled = IsPullToRefreshEnabled;
            NativeControl.IsRefreshing = IsRefreshing;
            NativeControl.SelectedItem = SelectedItem;
            NativeControl.SelectionMode = SelectionMode;
            NativeControl.HasUnevenRows = HasUnevenRows;
            NativeControl.RowHeight = RowHeight;
            NativeControl.IsGroupingEnabled = IsGroupingEnabled;
            NativeControl.SeparatorVisibility = SeparatorVisibility;
            NativeControl.SeparatorColor = SeparatorColor;
            NativeControl.RefreshControlColor = RefreshControlColor;
            NativeControl.HorizontalScrollBarVisibility = HorizontalScrollBarVisibility;
            NativeControl.VerticalScrollBarVisibility = VerticalScrollBarVisibility;

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

    public class RxListView<T, I> : RxListViewBase<T, I>, IRxListView<I> where T : ListView, new()
    {
        public RxListView()
        {
        }

        public RxListView(IEnumerable<I> collection)
        {
            Collection = collection;
        }

        public RxListView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

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

            protected sealed override void OnAddChild(VisualNode widget, Element nativeControl)
            {
                if (nativeControl is View view)
                    _presenter.View = view;
                else
                {
                    throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
                }
            }

            protected sealed override void OnRemoveChild(VisualNode widget, Element nativeControl)
            {
                _presenter.View = null;
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

        private class ItemTemplatePresenter : ViewCell
        {
            private ItemTemplateNode _itemTemplateNode;
            private CustomDataTemplate _template;

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
            public RxListView<T, I> Owner { get; set; }

            public CustomDataTemplate(RxListView<T, I> owner)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
            }
        }

        private CustomDataTemplate _customDataTemplate;

        protected override void OnUpdate()
        {
            if (NativeControl.ItemsSource is ObservableItemsSource<I> existingCollection &&
                existingCollection.ItemsSource == Collection)
            {
                _customDataTemplate.Owner = this;
                existingCollection.NotifyCollectionChanged();
            }
            else
            {
                _customDataTemplate = new CustomDataTemplate(this);
                NativeControl.ItemsSource = ObservableItemsSource<I>.Create(Collection);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }

            base.OnUpdate();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            ((RxListView<T, I>)newNode)._customDataTemplate = _customDataTemplate;

            base.OnMigrated(newNode);
        }
    }

    public interface IRxTextListView<I> : IRxListView
    {
        IEnumerable<I> Collection { get; set; }
        Action<I, RxTextCell> Template { get; set; }
    }

    public class RxTextListView<T, I> : RxListViewBase<T, I>, IRxTextListView<I> where T : ListView, new()
    {
        public RxTextListView()
        {
        }

        public RxTextListView(IEnumerable<I> collection)
        {
            Collection = collection;
        }

        public RxTextListView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Action<I, RxTextCell> Template { get; set; }

        private class ItemTemplatePresenter : TextCell
        {
            private readonly CustomDataTemplate _template;

            public ItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                var item = (I)BindingContext;
                if (item != null)
                {
                    var rxTextCell = new RxTextCell(this);
                    _template.Owner.Template(item, rxTextCell);
                    rxTextCell.Layout();
                }

                base.OnBindingContextChanged();
            }
        }

        private class CustomDataTemplate
        {
            public DataTemplate DataTemplate { get; }
            public RxTextListView<T, I> Owner { get; set; }

            public CustomDataTemplate(RxTextListView<T, I> owner)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
            }
        }

        private CustomDataTemplate _customDataTemplate;

        protected override void OnUpdate()
        {
            if (NativeControl.ItemsSource is ObservableItemsSource<I> existingCollection &&
                existingCollection.ItemsSource == Collection)
            {
                _customDataTemplate.Owner = this;
                existingCollection.NotifyCollectionChanged();
            }
            else
            {
                _customDataTemplate = new CustomDataTemplate(this);
                NativeControl.ItemsSource = ObservableItemsSource<I>.Create(Collection);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }

            base.OnUpdate();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            ((RxTextListView<T, I>)newNode)._customDataTemplate = _customDataTemplate;

            base.OnMigrated(newNode);
        }
    }

    public interface IRxImageListView<I> : IRxListView
    {
        IEnumerable<I> Collection { get; set; }
        Action<I, RxImageCell> Template { get; set; }
    }

    public class RxImageListView<T, I> : RxListViewBase<T, I>, IRxImageListView<I> where T : ListView, new()
    {
        public RxImageListView()
        {
        }

        public RxImageListView(IEnumerable<I> collection)
        {
            Collection = collection;
        }

        public RxImageListView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Action<I, RxImageCell> Template { get; set; }

        private class ItemTemplatePresenter : ImageCell
        {
            private readonly CustomDataTemplate _template;

            public ItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                var item = (I)BindingContext;
                if (item != null)
                {
                    var rxImageCell = new RxImageCell(this);
                    _template.Owner.Template(item, rxImageCell);
                    rxImageCell.Layout();
                }

                base.OnBindingContextChanged();
            }
        }

        private class CustomDataTemplate
        {
            public DataTemplate DataTemplate { get; }
            public RxImageListView<T, I> Owner { get; set; }

            public CustomDataTemplate(RxImageListView<T, I> owner)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
            }
        }

        private CustomDataTemplate _customDataTemplate;

        protected override void OnUpdate()
        {
            if (NativeControl.ItemsSource is ObservableItemsSource<I> existingCollection &&
                existingCollection.ItemsSource == Collection)
            {
                _customDataTemplate.Owner = this;
                existingCollection.NotifyCollectionChanged();
            }
            else
            {
                _customDataTemplate = new CustomDataTemplate(this);
                NativeControl.ItemsSource = ObservableItemsSource<I>.Create(Collection);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }

            base.OnUpdate();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            ((RxImageListView<T, I>)newNode)._customDataTemplate = _customDataTemplate;

            base.OnMigrated(newNode);
        }
    }

    public interface IRxSwitchListView<I> : IRxListView
    {
        IEnumerable<I> Collection { get; set; }
        Action<I, RxSwitchCell> Template { get; set; }
    }

    public class RxSwitchListView<T, I> : RxListViewBase<T, I>, IRxSwitchListView<I> where T : ListView, new()
    {
        public RxSwitchListView()
        {
        }

        public RxSwitchListView(IEnumerable<I> collection)
        {
            Collection = collection;
        }

        public RxSwitchListView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Action<I, RxSwitchCell> Template { get; set; }

        private class ItemTemplatePresenter : SwitchCell
        {
            private readonly CustomDataTemplate _template;

            public ItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                var item = (I)BindingContext;
                if (item != null)
                {
                    var rxSwitchCell = new RxSwitchCell(this);
                    _template.Owner.Template(item, rxSwitchCell);
                    rxSwitchCell.Layout();
                }

                base.OnBindingContextChanged();
            }
        }

        private class CustomDataTemplate
        {
            public DataTemplate DataTemplate { get; }
            public RxSwitchListView<T, I> Owner { get; set; }

            public CustomDataTemplate(RxSwitchListView<T, I> owner)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
            }
        }

        private CustomDataTemplate _customDataTemplate;

        protected override void OnUpdate()
        {
            if (NativeControl.ItemsSource is ObservableItemsSource<I> existingCollection &&
                existingCollection.ItemsSource == Collection)
            {
                _customDataTemplate.Owner = this;
                existingCollection.NotifyCollectionChanged();
            }
            else
            {
                _customDataTemplate = new CustomDataTemplate(this);
                NativeControl.ItemsSource = ObservableItemsSource<I>.Create(Collection);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }

            base.OnUpdate();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            ((RxSwitchListView<T, I>)newNode)._customDataTemplate = _customDataTemplate;

            base.OnMigrated(newNode);
        }
    }

    public interface IRxEntryListView<I> : IRxListView
    {
        IEnumerable<I> Collection { get; set; }
        Action<I, RxEntryCell> Template { get; set; }
    }

    public class RxEntryListView<T, I> : RxListViewBase<T, I>, IRxEntryListView<I> where T : ListView, new()
    {
        public RxEntryListView()
        {
        }

        public RxEntryListView(IEnumerable<I> collection)
        {
            Collection = collection;
        }

        public RxEntryListView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Action<I, RxEntryCell> Template { get; set; }

        private class ItemTemplatePresenter : EntryCell
        {
            private readonly CustomDataTemplate _template;

            public ItemTemplatePresenter(CustomDataTemplate template)
            {
                _template = template;
            }

            protected override void OnBindingContextChanged()
            {
                var item = (I)BindingContext;
                if (item != null)
                {
                    var rxEntryCell = new RxEntryCell(this);
                    _template.Owner.Template(item, rxEntryCell);
                    rxEntryCell.Layout();
                }

                base.OnBindingContextChanged();
            }
        }

        private class CustomDataTemplate
        {
            public DataTemplate DataTemplate { get; }
            public RxEntryListView<T, I> Owner { get; set; }

            public CustomDataTemplate(RxEntryListView<T, I> owner)
            {
                Owner = owner;
                DataTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));
            }
        }

        private CustomDataTemplate _customDataTemplate;

        protected override void OnUpdate()
        {
            if (NativeControl.ItemsSource is ObservableItemsSource<I> existingCollection &&
                existingCollection.ItemsSource == Collection)
            {
                _customDataTemplate.Owner = this;
                existingCollection.NotifyCollectionChanged();
            }
            else
            {
                _customDataTemplate = new CustomDataTemplate(this);
                NativeControl.ItemsSource = ObservableItemsSource<I>.Create(Collection);
                NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
            }

            base.OnUpdate();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            ((RxEntryListView<T, I>)newNode)._customDataTemplate = _customDataTemplate;

            base.OnMigrated(newNode);
        }
    }


    public class RxListView<I> : RxListView<ListView, I>
    {
        public RxListView()
        {
        }

        public RxListView(IEnumerable<I> collection)
            : base(collection)
        {
        }

        public RxListView(Action<ListView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public class RxTextListView<I> : RxTextListView<ListView, I>
    {
        public RxTextListView()
        {
        }

        public RxTextListView(IEnumerable<I> collection)
            : base(collection)
        {
        }

        public RxTextListView(Action<ListView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public class RxImageListView<I> : RxImageListView<ListView, I>
    {
        public RxImageListView()
        {
        }

        public RxImageListView(IEnumerable<I> collection)
            : base(collection)
        {
        }

        public RxImageListView(Action<ListView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public class RxSwitchListView<I> : RxSwitchListView<ListView, I>
    {
        public RxSwitchListView()
        {
        }

        public RxSwitchListView(IEnumerable<I> collection)
            : base(collection)
        {
        }

        public RxSwitchListView(Action<ListView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public class RxEntryListView<I> : RxEntryListView<ListView, I>
    {
        public RxEntryListView()
        {
        }

        public RxEntryListView(IEnumerable<I> collection)
            : base(collection)
        {
        }

        public RxEntryListView(Action<ListView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxListViewBaseExtensions
    {
        public static T IsPullToRefreshEnabled<T>(this T listview, bool isPullToRefreshEnabled) where T : IRxListView
        {
            listview.IsPullToRefreshEnabled = isPullToRefreshEnabled;
            return listview;
        }

        public static T IsRefreshing<T>(this T listview, bool isRefreshing) where T : IRxListView
        {
            listview.IsRefreshing = isRefreshing;
            return listview;
        }

        public static T RenderCollection<T, I>(this T itemsview, IEnumerable<I> collection) where T : IRxListView<I>
        {
            itemsview.Collection = collection;
            return itemsview;
        }

        public static T SelectedItem<T>(this T listview, object selectedItem) where T : IRxListView
        {
            listview.SelectedItem = selectedItem;
            return listview;
        }

        public static T SelectionMode<T>(this T listview, ListViewSelectionMode selectionMode) where T : IRxListView
        {
            listview.SelectionMode = selectionMode;
            return listview;
        }

        public static T HasUnevenRows<T>(this T listview, bool hasUnevenRows) where T : IRxListView
        {
            listview.HasUnevenRows = hasUnevenRows;
            return listview;
        }

        public static T RowHeight<T>(this T listview, int rowHeight) where T : IRxListView
        {
            listview.RowHeight = rowHeight;
            return listview;
        }

        public static T IsGroupingEnabled<T>(this T listview, bool isGroupingEnabled) where T : IRxListView
        {
            listview.IsGroupingEnabled = isGroupingEnabled;
            return listview;
        }

        public static T SeparatorVisibility<T>(this T listview, SeparatorVisibility separatorVisibility) where T : IRxListView
        {
            listview.SeparatorVisibility = separatorVisibility;
            return listview;
        }

        public static T SeparatorColor<T>(this T listview, Color separatorColor) where T : IRxListView
        {
            listview.SeparatorColor = separatorColor;
            return listview;
        }

        public static T RefreshControlColor<T>(this T listview, Color refreshControlColor) where T : IRxListView
        {
            listview.RefreshControlColor = refreshControlColor;
            return listview;
        }

        public static T HorizontalScrollBarVisibility<T>(this T listview, ScrollBarVisibility horizontalScrollBarVisibility) where T : IRxListView
        {
            listview.HorizontalScrollBarVisibility = horizontalScrollBarVisibility;
            return listview;
        }

        public static T VerticalScrollBarVisibility<T>(this T listview, ScrollBarVisibility verticalScrollBarVisibility) where T : IRxListView
        {
            listview.VerticalScrollBarVisibility = verticalScrollBarVisibility;
            return listview;
        }
    }

    public static class RxListViewExtensions
    {
        public static T RenderCollection<T, I>(this T itemsview, IEnumerable<I> collection, Func<I, VisualNode> template) where T : IRxListView<I>
        {
            itemsview.Collection = collection;
            itemsview.Template = template;
            return itemsview;
        }

        public static T WithTemplate<T, I>(this T itemsview, Func<I, VisualNode> template) where T : IRxListView<I>
        {
            itemsview.Template = template;
            return itemsview;
        }
    }

    public static class RxTextListViewExtensions
    {
        public static T RenderCollection<T, I>(this T itemsview, IEnumerable<I> collection, Action<I, RxTextCell> template) where T : IRxTextListView<I>
        {
            itemsview.Collection = collection;
            itemsview.Template = template;
            return itemsview;
        }

        public static T WithTemplate<T, I>(this T itemsview, Action<I, RxTextCell> template) where T : IRxTextListView<I>
        {
            itemsview.Template = template;
            return itemsview;
        }
    }

    public static class RxImageListViewExtensions
    {
        public static T RenderCollection<T, I>(this T itemsview, IEnumerable<I> collection, Action<I, RxImageCell> template) where T : IRxImageListView<I>
        {
            itemsview.Collection = collection;
            itemsview.Template = template;
            return itemsview;
        }

        public static T WithTemplate<T, I>(this T itemsview, Action<I, RxImageCell> template) where T : IRxImageListView<I>
        {
            itemsview.Template = template;
            return itemsview;
        }
    }

    public static class RxSwitchListViewExtensions
    {
        public static T RenderCollection<T, I>(this T itemsview, IEnumerable<I> collection, Action<I, RxSwitchCell> template) where T : IRxSwitchListView<I>
        {
            itemsview.Collection = collection;
            itemsview.Template = template;
            return itemsview;
        }

        public static T WithTemplate<T, I>(this T itemsview, Action<I, RxSwitchCell> template) where T : IRxSwitchListView<I>
        {
            itemsview.Template = template;
            return itemsview;
        }
    }

    public static class RxEntryListViewExtensions
    {
        public static T RenderCollection<T, I>(this T itemsview, IEnumerable<I> collection, Action<I, RxEntryCell> template) where T : IRxEntryListView<I>
        {
            itemsview.Collection = collection;
            itemsview.Template = template;
            return itemsview;
        }

        public static T WithTemplate<T, I>(this T itemsview, Action<I, RxEntryCell> template) where T : IRxEntryListView<I>
        {
            itemsview.Template = template;
            return itemsview;
        }
    }
}