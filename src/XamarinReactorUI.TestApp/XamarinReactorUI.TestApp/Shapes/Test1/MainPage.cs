using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI.Shapes;

namespace XamarinReactorUI.TestApp.Shapes.Test1
{
    public class MainPage : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxNavigationPage
            {
                new RxContentPage("Reactangle shape")
                {
                    new RxRectangle()
                        .HeightRequest(100)
                        .WidthRequest(300)
                        .VCenter()
                        .StrokeThickness(2)
                        .RadiusX(20)
                        .RadiusY(20)
                        .Stroke(Color.Blue)
                        .Fill(Color.Red)
                }
            };
        }

    }
}
