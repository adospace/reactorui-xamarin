using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.TestApp.Navigation
{
    public class ChildPageComponent : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxContentPage()
            { 
                new RxButton("Back")
                    .OnClick(GoBack)
            
            }
            .Title("Child Page");
        }

        private async void GoBack()
        {
            await Navigation().PopModalAsync();
        }
    }
}
