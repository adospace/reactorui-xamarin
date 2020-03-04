using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxCollectionView
    {
        object SelectedItem { get; set; }

        Action<VisualNode> SelectedAction { get; set; }
        
        Action<IReadOnlyList<VisualNode>, IReadOnlyList<VisualNode>> SelectionChangedAction { get; set; }

        SelectionMode SelectionMode { get; set; }
    }


    public class RxCollectionView : RxView<CollectionView>, IRxCollectionView, IEnumerable<VisualNode>
    {
        private readonly List<VisualNode> _internalChildren = new List<VisualNode>();
        private readonly List<VisualNode> _layoutChildren = new List<VisualNode>();

        public object SelectedItem { get; set; }
        
        public Action<VisualNode> SelectedAction { get; set; }
        
        public Action<IReadOnlyList<VisualNode>, IReadOnlyList<VisualNode>> SelectionChangedAction { get; set; }


        private readonly NullableField<SelectionMode> _selectionMode = new NullableField<SelectionMode>();
        public SelectionMode SelectionMode { get => _selectionMode.GetValueOrDefault(); set => _selectionMode.Value = value; }

        public RxCollectionView(params VisualNode[] children)
        {
            if (children is null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            _internalChildren = new List<VisualNode>(children);
        }

        public RxCollectionView(IEnumerable<VisualNode> children)
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

            protected sealed override void OnAddChild(RxElement widget, Xamarin.Forms.View nativeControl)
            {
                _itemTemplatePreseter.Content = nativeControl;
            }

            protected sealed override void OnRemoveChild(RxElement widget, Xamarin.Forms.View nativeControl)
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

        private class ItemTemplatePresenter : ContentView
        {
            private readonly RxCollectionView _collectionView;
            private ItemTemplateNode _itemTemplateNode;

            public ItemTemplatePresenter(RxCollectionView collectionView)
            {
                _collectionView = collectionView;
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

        protected override void OnUpdate()
        {
            NativeControl.ItemsSource = _internalChildren;
            NativeControl.ItemTemplate = new DataTemplate(() =>
            {
                return new ItemTemplatePresenter(this);
            });

            if (_selectionMode.HasValue) NativeControl.SelectionMode = _selectionMode.Value;

            NativeControl.SelectedItem = SelectedItem;            

            if (SelectedAction != null || SelectionChangedAction != null)
                NativeControl.SelectionChanged += NativeControl_SelectionChanged;

            base.OnUpdate();
        }

        private void NativeControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedAction != null || SelectionChangedAction != null)
            {
                SelectedAction?.Invoke(e.CurrentSelection.Count > 0 ? (VisualNode)e.CurrentSelection[0] : null);
                SelectionChangedAction?.Invoke(e.CurrentSelection.Cast<VisualNode>().ToList(), e.PreviousSelection.Cast<VisualNode>().ToList());
            }
        }

        protected override void OnMigrated()
        {
            NativeControl.SelectionChanged -= NativeControl_SelectionChanged;

            base.OnMigrated();
        }
    }


    public static class RxCollectionViewExtensions
    {
        public static T OnSelected<T>(this T collectionView, Action<VisualNode> action) where T : IRxCollectionView
        {
            collectionView.SelectedAction = action;
            return collectionView;
        }

        public static T OnSelectionChanged<T>(this T collectionView, Action<IReadOnlyList<VisualNode>, IReadOnlyList<VisualNode>> action) where T : IRxCollectionView
        {
            collectionView.SelectionChangedAction = action;
            return collectionView;
        }

        public static T SelectionMode<T>(this T collectionView, SelectionMode selectionMode) where T : IRxCollectionView
        {
            collectionView.SelectionMode = selectionMode;
            return collectionView;
        }
    }

}
