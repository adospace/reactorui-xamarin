using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxCollectionView : IRxSelectableItemsView
    {
    }

    public class RxCollectionView<T, I> : RxSelectableItemsView<T, I>, IRxCollectionView/*, IEnumerable<VisualNode>*/ where T : CollectionView, new()
    {
        public RxCollectionView()
        { }

        //public RxCollectionView(params VisualNode[] children)
        //    : base(children)
        //{
        //}

        //public RxCollectionView(IEnumerable<VisualNode> children)
        //    : base(children)
        //{
        //}

        public RxCollectionView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override void OnUpdate()
        {

            base.OnUpdate();
        }
    }

    public class RxCollectionView<I> : RxCollectionView<CollectionView, I>
    {
        public RxCollectionView()
        { }

        //public RxCollectionView(params VisualNode[] children)
        //    : base(children)
        //{
        //}

        //public RxCollectionView(IEnumerable<VisualNode> children)
        //    : base(children)
        //{
        //}

        public RxCollectionView(Action<CollectionView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxCollectionViewExtensions
    {
    }
}