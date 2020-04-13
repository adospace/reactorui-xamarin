using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxCollectionView
    {
    }

    public class RxCollectionView<T> : RxSelectableItemsView<T>, IRxCollectionView, IEnumerable<VisualNode> where T : CollectionView, new()
    {
        public RxCollectionView()
        { }

        public RxCollectionView(params VisualNode[] children)
            : base(children)
        {
        }

        public RxCollectionView(IEnumerable<VisualNode> children)
            : base(children)
        {
        }

        public RxCollectionView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public class RxCollectionView : RxCollectionView<CollectionView>
    {
        public RxCollectionView()
        { }

        public RxCollectionView(params VisualNode[] children)
            : base(children)
        {
        }

        public RxCollectionView(IEnumerable<VisualNode> children)
            : base(children)
        {
        }

        public RxCollectionView(Action<CollectionView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxCollectionViewExtensions
    {
    }
}