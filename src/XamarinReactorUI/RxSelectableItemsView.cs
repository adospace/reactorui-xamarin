
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxSelectableItemsView
    {
        SelectionMode SelectionMode { get; set; }
        object SelectedItem { get; set; }
        IEnumerable<object> SelectedItems { get; set; }
        Action<VisualNode> SelectedAction { get; set; }
        Action<IReadOnlyList<VisualNode>, IReadOnlyList<VisualNode>> SelectionChangedAction { get; set; }
    }

    public abstract class RxSelectableItemsView<T> : RxItemsView<T>, IRxSelectableItemsView where T : Xamarin.Forms.SelectableItemsView, new()
    {
        public RxSelectableItemsView()
        {

        }

        public RxSelectableItemsView(params VisualNode[] children)
            : base(children)
        {
        }

        public RxSelectableItemsView(IEnumerable<VisualNode> children)
            : base(children)
        {
        }

        public RxSelectableItemsView(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public SelectionMode SelectionMode { get; set; } = (SelectionMode)SelectableItemsView.SelectionModeProperty.DefaultValue;
        public object SelectedItem { get; set; } = (object)SelectableItemsView.SelectedItemProperty.DefaultValue;
        public IEnumerable<object> SelectedItems { get; set; } = (IEnumerable<object>)SelectableItemsView.SelectedItemsProperty.DefaultValue;
        public Action<VisualNode> SelectedAction { get; set; }
        public Action<IReadOnlyList<VisualNode>, IReadOnlyList<VisualNode>> SelectionChangedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.SelectionMode = SelectionMode;
            NativeControl.SelectedItem = SelectedItem;
            NativeControl.SelectedItems = SelectedItems?.ToList();

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
                Invalidate();
            }
        }

        protected override void OnMigrated()
        {
            NativeControl.SelectionChanged -= NativeControl_SelectionChanged;
            NativeControl.SelectionChangedCommand = null;

            base.OnMigrated();
        }

    }

    public static class RxSelectableItemsViewExtensions
    {
        public static T OnSelected<T>(this T collectionView, Action<VisualNode> action) where T : IRxSelectableItemsView
        {
            collectionView.SelectedAction = action;
            return collectionView;
        }

        public static T OnSelectionChanged<T>(this T collectionView, Action<IReadOnlyList<VisualNode>, IReadOnlyList<VisualNode>> action) where T : IRxSelectableItemsView
        {
            collectionView.SelectionChangedAction = action;
            return collectionView;
        }

        public static T SelectionMode<T>(this T selectableitemsview, SelectionMode selectionMode) where T : IRxSelectableItemsView
        {
            selectableitemsview.SelectionMode = selectionMode;
            return selectableitemsview;
        }

        public static T SelectedItem<T>(this T selectableitemsview, object selectedItem) where T : IRxSelectableItemsView
        {
            selectableitemsview.SelectedItem = selectedItem;
            return selectableitemsview;
        }
        
        public static T SelectedItems<T>(this T selectableitemsview, IEnumerable<object> selectedItems) where T : IRxSelectableItemsView
        {
            selectableitemsview.SelectedItems = selectedItems;
            return selectableitemsview;
        }

    }

}