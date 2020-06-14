using System;
using System.Collections.Generic;
using System.Text;
using XamarinReactorUI.SharedTransitions;

namespace XamarinReactorUI.TestApp.SharedTransitions
{
    public class MainPage : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxSharedTransitionNavigationPage()
            {
                new RxContentPage()
                {
                    new RxStackLayout()
                    {
                        new RxImage()
                            .Source("city.jpg")
                            .HeightRequest(100)
                            .WidthRequest(100)
                            .TransitionName("city"),
                        new RxButton()
                            .Text("Navigate")
                            .OnClick(OnNavigateToChildPage)
                    }
                    .VCenter()
                    .HCenter()
                }
                .Title("Master")
            };
        }

        private async void OnNavigateToChildPage()
        {
            await Navigation.PushAsync<ChildPage>();
        }
    }
}
