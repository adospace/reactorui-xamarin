using System;
using System.Collections.Generic;
using System.Text;
using XamarinReactorUI.SharedTransitions;

namespace XamarinReactorUI.TestApp.SharedTransitions
{
    internal class ChildPage : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                new RxImage()
                    .Source("city.jpg")
                    .HeightRequest(200)
                    .VStart()
                    .HFillAndExpand()
                    .TransitionName("city")

            }
            .Title("Details");
        }
    }
}
