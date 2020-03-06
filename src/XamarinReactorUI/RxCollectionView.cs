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


    public class RxCollectionView : RxSelectableItemsView<CollectionView>, IRxCollectionView, IEnumerable<VisualNode>
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

    }


    public static class RxCollectionViewExtensions
    {
    }

}
