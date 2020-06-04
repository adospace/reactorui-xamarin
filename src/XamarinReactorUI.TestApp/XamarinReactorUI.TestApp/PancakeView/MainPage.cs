using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace XamarinReactorUI.TestApp.PancakeView
{
    public class MainPage : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxNavigationPage()
            {
                new RxContentPage("PancakeView Demo")
                { 
                    new RxScrollView()
                    {
                        new RxStackLayout()
                        {
                            new RxPancakeView()
                            {
                                new RxLabel("One Pancake!")
                                    .TextColor(Color.White)
                                    .VCenter()
                                    .HCenter()
                            }
                            .CornerRadius(10)
                            .BackgroundColor(Color.Red)
                            .HeightRequest(120)
                            .Margin(10)
                        }
                    }
                }
            };
        }
    }
}
