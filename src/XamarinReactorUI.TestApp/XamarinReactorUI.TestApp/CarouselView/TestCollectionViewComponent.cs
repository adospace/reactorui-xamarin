using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI;
using XamarinReactorUI.TestApp.CollectionView;

namespace XamarinReactorUI.TestApp.CarouselView
{
    public class TestCarouselViewComponent : RxComponent
    {
        private readonly IEnumerable<Monkey> _allMonkeys = Monkey.GetList();

        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                new RxCarouselView<Monkey>()
                    .RenderCollection(_allMonkeys, RenderMonkey)
            };
        }

        private VisualNode RenderMonkey(Monkey monkey)
        {
            return new RxFrame()
            {
                new RxStackLayout()
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
                    .HCenter()
                }
                .Padding(10)
            }
            .HasShadow(true)
            .BorderColor(Color.Gray)
            .Margin(10);
        }
    }
}
