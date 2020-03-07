
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxItemsView
    {
        ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
        ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
        int RemainingItemsThreshold { get; set; }
        ItemsUpdatingScrollMode ItemsUpdatingScrollMode { get; set; }
        VisualStateGroupList ItemVisualStateGroups { get; set; }
    }

    public abstract class RxItemsView<T> : RxView<T>, IRxItemsView, IEnumerable<VisualNode> where T : Xamarin.Forms.ItemsView, new()
    {
        private readonly List<VisualNode> _internalChildren = new List<VisualNode>();
        private readonly List<VisualNode> _layoutChildren = new List<VisualNode>();
        
        public RxItemsView()
        {

        }

        public RxItemsView(params VisualNode[] children)
        {
            if (children is null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            _internalChildren = new List<VisualNode>(children);
        }

        public RxItemsView(IEnumerable<VisualNode> children)
        {
            if (children is null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            _internalChildren = new List<VisualNode>(children);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(VisualNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            _internalChildren.Add(node);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _layoutChildren;
        }

        private void AddLayoutChild(ItemTemplateNode childNode)
        {
            _layoutChildren.Add(childNode);
            Invalidate();
        }

        private class ItemTemplateNode : VisualNode
        {
            private readonly ItemTemplatePresenter _itemTemplatePreseter;

            public ItemTemplateNode(ItemTemplatePresenter itemTemplatePreseter)
            {
                _itemTemplatePreseter = itemTemplatePreseter;
            }

            protected sealed override void OnAddChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
            {
                if (nativeControl is View view)
                    _itemTemplatePreseter.Content = view;
                else
                {
                    throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
                }
            }

            protected sealed override void OnRemoveChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
            {
                _itemTemplatePreseter.Content = null;
            }

            protected override void OnUnmount()
            {
                _itemTemplatePreseter.Content = null;
                base.OnUnmount();
            }

            protected override IEnumerable<VisualNode> RenderChildren()
            {
                yield return _itemTemplatePreseter.PresentedView;
            }
        }

        private class ItemTemplatePresenter : ContentPresenter
        {
            private readonly RxItemsView<T> _collectionView;
            private ItemTemplateNode _itemTemplateNode;

            public ItemTemplatePresenter(RxItemsView<T> collectionView)
            {
                _collectionView = collectionView;
                VisualStateManager.SetVisualStateGroups(this, collectionView.ItemVisualStateGroups);
            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();

                if (_itemTemplateNode == null && PresentedView != null)
                {
                    _itemTemplateNode = new ItemTemplateNode(this);
                    _collectionView.AddLayoutChild(_itemTemplateNode);
                }
            }

            public VisualNode PresentedView => (VisualNode)BindingContext;
        }

        public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; } = (ScrollBarVisibility)ItemsView.HorizontalScrollBarVisibilityProperty.DefaultValue;
        public ScrollBarVisibility VerticalScrollBarVisibility { get; set; } = (ScrollBarVisibility)ItemsView.VerticalScrollBarVisibilityProperty.DefaultValue;
        public int RemainingItemsThreshold { get; set; } = (int)ItemsView.RemainingItemsThresholdProperty.DefaultValue;
        public ItemsUpdatingScrollMode ItemsUpdatingScrollMode { get; set; } = (ItemsUpdatingScrollMode)ItemsView.ItemsUpdatingScrollModeProperty.DefaultValue;
        public VisualStateGroupList ItemVisualStateGroups { get; set; } = new VisualStateGroupList();

        protected override void OnUpdate()
        {
            NativeControl.ItemsSource = _internalChildren;
            NativeControl.ItemTemplate = new DataTemplate(() => new ItemTemplatePresenter(this));

            NativeControl.HorizontalScrollBarVisibility = HorizontalScrollBarVisibility;
            NativeControl.VerticalScrollBarVisibility = VerticalScrollBarVisibility;
            NativeControl.RemainingItemsThreshold = RemainingItemsThreshold;
            NativeControl.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode;

            base.OnUpdate();
        }

    }

    public static class RxItemsViewExtensions
    {
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
