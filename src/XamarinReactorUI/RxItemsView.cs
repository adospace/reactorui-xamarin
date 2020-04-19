using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XamarinReactorUI.Internals;

namespace XamarinReactorUI
{
    public interface IRxItemsView
    {
        ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
        ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
        int RemainingItemsThreshold { get; set; }
        ItemsUpdatingScrollMode ItemsUpdatingScrollMode { get; set; }
        VisualStateGroupList ItemVisualStateGroups { get; set; }
        Action RemainingItemsThresholdReachedAction { get; set; }
    }

    public interface IRxItemsView<I> : IRxItemsView
    {
        IEnumerable<I> Collection { get; set; }
        Func<I, VisualNode> Template { get; set; }
    }

    public abstract class RxItemsView<T, I> : RxView<T>, IRxItemsView<I>/*, IEnumerable<VisualNode>*/ where T : Xamarin.Forms.ItemsView, new()
    {
        public RxItemsView()
        {
        }

        public RxItemsView(IEnumerable<I> collection)
        {
            Collection = collection;
        }

        public RxItemsView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }

        private class ItemTemplateNode : VisualNode
        {
            private ItemTemplatePresenter _presenter = null;

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
                    _presenter.Content = view;
                else
                {
                    throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
                }
            }

            protected sealed override void OnRemoveChild(VisualNode widget, Element nativeControl)
            {
                _presenter.Content = null;
            }

            protected override IEnumerable<VisualNode> RenderChildren()
            {
                yield return Root;
            }
        }

        public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; } = (ScrollBarVisibility)ItemsView.HorizontalScrollBarVisibilityProperty.DefaultValue;
        public ScrollBarVisibility VerticalScrollBarVisibility { get; set; } = (ScrollBarVisibility)ItemsView.VerticalScrollBarVisibilityProperty.DefaultValue;
        public int RemainingItemsThreshold { get; set; } = (int)ItemsView.RemainingItemsThresholdProperty.DefaultValue;
        public ItemsUpdatingScrollMode ItemsUpdatingScrollMode { get; set; } = (ItemsUpdatingScrollMode)ItemsView.ItemsUpdatingScrollModeProperty.DefaultValue;
        public VisualStateGroupList ItemVisualStateGroups { get; set; } = new VisualStateGroupList();
        public Action RemainingItemsThresholdReachedAction { get; set; }

        public IEnumerable<I> Collection { get; set; }
        public Func<I, VisualNode> Template { get; set; }

        private class ItemTemplatePresenter : ContentView
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
            public RxItemsView<T, I> Owner { get; set; }

            public CustomDataTemplate(RxItemsView<T, I> owner)
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

            NativeControl.HorizontalScrollBarVisibility = HorizontalScrollBarVisibility;
            NativeControl.VerticalScrollBarVisibility = VerticalScrollBarVisibility;
            NativeControl.RemainingItemsThreshold = RemainingItemsThreshold;
            NativeControl.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode;

            if (RemainingItemsThresholdReachedAction != null)
                NativeControl.RemainingItemsThresholdReached += NativeControl_RemainingItemsThresholdReached;

            base.OnUpdate();
        }

        private void NativeControl_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            RemainingItemsThresholdReachedAction?.Invoke();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.RemainingItemsThresholdReached -= NativeControl_RemainingItemsThresholdReached;
            }

            ((RxItemsView<T, I>)newNode)._customDataTemplate = _customDataTemplate;

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
            {
                NativeControl.RemainingItemsThresholdReached -= NativeControl_RemainingItemsThresholdReached;
            }

            base.OnUnmount();
        }
    }

    public static class RxItemsViewExtensions
    {
        public static T RenderCollection<T, I>(this T itemsview, IEnumerable<I> collection) where T : IRxItemsView<I>
        {
            itemsview.Collection = collection;
            return itemsview;
        }

        public static T RenderCollection<T, I>(this T itemsview, IEnumerable<I> collection, Func<I, VisualNode> template) where T : IRxItemsView<I>
        {
            itemsview.Collection = collection;
            itemsview.Template = template;
            return itemsview;
        }

        public static T WithTemplate<T, I>(this T itemsview, Func<I, VisualNode> template) where T : IRxItemsView<I>
        {
            itemsview.Template = template;
            return itemsview;
        }

        public static T OnRemainingItemsThresholdReached<T>(this T itemsview, Action action) where T : IRxItemsView
        {
            itemsview.RemainingItemsThresholdReachedAction = action;
            return itemsview;
        }

        public static T ItemVisualState<T>(this T itemsview, string groupName, string stateName, BindableProperty property, object value, string targetName = null) where T : IRxItemsView
        {
            var group = itemsview.ItemVisualStateGroups.FirstOrDefault(_ => _.Name == groupName);

            if (group == null)
            {
                itemsview.ItemVisualStateGroups.Add(group = new VisualStateGroup()
                {
                    Name = groupName
                });
            }

            var state = group.States.FirstOrDefault(_ => _.Name == stateName);
            if (state == null)
            {
                group.States.Add(state = new VisualState { Name = stateName });
            }

            state.Setters.Add(new Setter() { Property = property, Value = value });

            return itemsview;
        }

        public static T HorizontalScrollBarVisibility<T>(this T itemsview, ScrollBarVisibility horizontalScrollBarVisibility) where T : IRxItemsView
        {
            itemsview.HorizontalScrollBarVisibility = horizontalScrollBarVisibility;
            return itemsview;
        }

        public static T VerticalScrollBarVisibility<T>(this T itemsview, ScrollBarVisibility verticalScrollBarVisibility) where T : IRxItemsView
        {
            itemsview.VerticalScrollBarVisibility = verticalScrollBarVisibility;
            return itemsview;
        }

        public static T RemainingItemsThreshold<T>(this T itemsview, int remainingItemsThreshold) where T : IRxItemsView
        {
            itemsview.RemainingItemsThreshold = remainingItemsThreshold;
            return itemsview;
        }

        public static T ItemsUpdatingScrollMode<T>(this T itemsview, ItemsUpdatingScrollMode itemsUpdatingScrollMode) where T : IRxItemsView
        {
            itemsview.ItemsUpdatingScrollMode = itemsUpdatingScrollMode;
            return itemsview;
        }
    }
}