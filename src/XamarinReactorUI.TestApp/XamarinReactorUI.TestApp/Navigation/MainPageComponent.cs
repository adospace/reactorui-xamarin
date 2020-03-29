using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Navigation
{
    public class MainPageComponent : RxComponent
    {
        private NavigationPage _pageRef;

        public override VisualNode Render()
        {
            return new RxNavigationPage(pageRef => _pageRef = pageRef)
            {
                new RxContentPage()
                {
                    new RxButton("Move To Child Page")
                        .VCenter()
                        .HCenter()
                        .OnClick(OpenChildPage)
                }
                .Title("Main Page")
            };
        }

        private async void OpenChildPage()
        {
            await Navigation().PushAsync<ChildPageComponent>();
        }
    }
}
