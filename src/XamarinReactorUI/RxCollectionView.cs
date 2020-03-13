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

    }


    public sealed class RxCollectionView : RxSelectableItemsView<CollectionView>, IRxCollectionView, IEnumerable<VisualNode>
    {
        public RxCollectionView()
        { }

        public RxCollectionView(params VisualNode[] children)
            :base(children)
        {
        }

        public RxCollectionView(IEnumerable<VisualNode> children)
            :base(children)
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
