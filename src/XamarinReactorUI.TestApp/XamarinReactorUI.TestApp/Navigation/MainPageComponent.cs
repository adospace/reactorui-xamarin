using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Navigation
{
    public class MainPageComponent : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxNavigationPage()
            {
                new RxContentPage()
                {
                    new RxButton("Move To Page")
                        .VCenter()
                        .HCenter()
                        .OnClick(OpenChildPage)
                }
                .Title("Main Page")
            };
        }

        private async void OpenChildPage()
        {
            await Navigation.PushAsync<ChildPageComponent>();
        }
    }
}
