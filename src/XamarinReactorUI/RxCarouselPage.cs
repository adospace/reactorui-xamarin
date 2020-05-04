using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxCarouselPage
    {
    }

    public class RxCarouselPage<T> : RxMultiPage<T, ContentPage>, IRxCarouselPage where T : Xamarin.Forms.CarouselPage, new()
    {
        public RxCarouselPage()
        {
        }

        public RxCarouselPage(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public class RxCarouselPage : RxCarouselPage<Xamarin.Forms.CarouselPage>
    {
        public RxCarouselPage()
        {
        }

        public RxCarouselPage(Action<CarouselPage> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxCarouselPageExtensions
    {
    }
}