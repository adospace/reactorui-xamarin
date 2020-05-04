
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxSelectableItemsView : IRxItemsView
    {
        SelectionMode SelectionMode { get; set; }
        object SelectedItem { get; set; }
        IEnumerable<object> SelectedItems { get; set; }
    }

    public interface IRxSelectableItemsView<I> : IRxSelectableItemsView
    {
        Action<I> SelectedAction { get; set; }
        Action<IReadOnlyList<I>, IReadOnlyList<I>> SelectionChangedAction { get; set; }
    }

    public abstract class RxSelectableItemsView<T, I> : RxStructuredItemsView<T, I>, IRxSelectableItemsView<I> where T : SelectableItemsView, new()
    {
        public RxSelectableItemsView()
        {

        }

        public RxSelectableItemsView(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public SelectionMode SelectionMode { get; set; } = (SelectionMode)SelectableItemsView.SelectionModeProperty.DefaultValue;
        public object SelectedItem { get; set; } = (object)SelectableItemsView.SelectedItemProperty.DefaultValue;
        public IEnumerable<object> SelectedItems { get; set; } = (IEnumerable<object>)SelectableItemsView.SelectedItemsProperty.DefaultValue;
        public Action<I> SelectedAction { get; set; }
        public Action<IReadOnlyList<I>, IReadOnlyList<I>> SelectionChangedAction { get; set; }

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
                SelectedAction?.Invoke(e.CurrentSelection.Count > 0 ? (I)e.CurrentSelection[0] : default);
                SelectionChangedAction?.Invoke(e.CurrentSelection.Cast<I>().ToList(), e.PreviousSelection.Cast<I>().ToList());
                Invalidate();
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.SelectionChanged -= NativeControl_SelectionChanged;
                NativeControl.SelectionChangedCommand = null;
            }

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
            {
                NativeControl.SelectionChanged -= NativeControl_SelectionChanged;
                NativeControl.SelectionChangedCommand = null;
            }

            base.OnUnmount();
        }
    }

    public static class RxSelectableItemsViewExtensions
    {
        public static T OnSelected<T, I>(this T collectionView, Action<I> action) where T : IRxSelectableItemsView<I>
        {
            collectionView.SelectedAction = action;
            return collectionView;
        }

        public static T OnSelectionChanged<T, I>(this T collectionView, Action<IReadOnlyList<I>, IReadOnlyList<I>> action) where T : IRxSelectableItemsView<I>
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