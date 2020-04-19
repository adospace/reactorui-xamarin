using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI;

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

        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                new RxStackLayout()
                {
                    new RxButton("SWITCH SMALL/LARGE LIST")
                        .OnClick(this.OnShowHideLargeList),
                        
                        new RxCollectionView<Monkey>()
                            .RenderCollection(
                                _largePersonListVisible ? _allMonkeys : _allMonkeys.Take(4), 
                                RenderMonkey)
                        .VFillAndExpand()
                }
                .HFillAndExpand()
                .VFillAndExpand()
            };
        }

        private VisualNode RenderMonkey(Monkey monkey)
        {
            return new RxStackLayout()
            {
                new RxImage()
                    .Source(new Uri(monkey.ImageUrl))
                    .Margin(4),
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
            .WithHorizontalOrientation();
        }
    }
}
