using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Navigation
{
    public class ChildPageComponent : RxComponent
    {

        public override VisualNode Render()
        {
            return new RxContentPage()
            { 
                new RxButton("Back")
                    .VCenter()
                    .HCenter()
                    .OnClick(GoBack)
            
            }
            .Title("Child Page");
        }

        private async void GoBack()
        {
            await Navigation.PopAsync();
        }
    }
}
