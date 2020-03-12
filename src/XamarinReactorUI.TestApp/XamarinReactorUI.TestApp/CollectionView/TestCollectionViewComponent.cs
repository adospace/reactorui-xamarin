using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.CollectionView
{
    public class TestCollectionViewComponent : RxComponent
    {
        private IEnumerable<Monkey> _allMonkeys = Monkey.GetList();

        bool _largePersonListVisible = false;
        private void OnShowHideLargeList()
        {
            _largePersonListVisible = !_largePersonListVisible;
            Invalidate();
        }

        private IEnumerable<VisualNode> RenderList(IEnumerable<Monkey> persons)
            => persons.Select(monkey => new RxStackLayout()
                {
                    new RxImage()
                        .Source(new Uri(monkey.ImageUrl))
                        
                        .Margin(4)
                    ,
                    new RxStackLayout()
                    {
                        new RxLabel(monkey.Name)
                            .FontSize(NamedSize.Default)
                            .Margin(5),
                        new RxLabel(monkey.Location)
                            .FontSize(NamedSize.Caption)
                            .Margin(5)
                    }
                }
                .Padding(10)
                .WithHorizontalOrientation()
                );

        public override VisualNode Render()
        {
            return 
                new RxStackLayout()
                {
                    new RxButton("SWITCH SMALL/LARGE LIST")
                        .OnClick(OnShowHideLargeList),
                        _largePersonListVisible ? 
                        new RxCollectionView(RenderList(_allMonkeys))
                        .VFillAndExpand()
                        :
                        new RxCollectionView(RenderList(_allMonkeys.Take(4)))
                        .VFillAndExpand()
                }
                .HFillAndExpand()
                .VFillAndExpand();
        }

    }
}
