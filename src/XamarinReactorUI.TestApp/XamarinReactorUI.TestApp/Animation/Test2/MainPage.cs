using System.Reflection;
using Xamarin.Forms;
using XamarinReactorUI.Shapes;

namespace XamarinReactorUI.TestApp.Animation.Test2
{
    public class MainPageState : IState
    {
        public bool Toggle { get; set; }
    }

    public class MainPage : RxComponent<MainPageState>
    {
        public override VisualNode Render()
        {
            return new RxNavigationPage()
            {
                new RxContentPage("Button Animation Demo 2")
                {
                    new RxStackLayout()
                    {
                        new RxRectangle()
                            .HeightRequest(65)
                            .WidthRequest(250)
                            .RadiusX(45)
                            .RadiusY(45)
                            .Fill(Color.FromHex("52485C"))
                            .VCenter()
                            .HCenter(),
                       //new RxRectangle()
                       //     .HeightRequest(55)
                       //     .WidthRequest(100)
                       //     .RadiusX(45)
                       //     .RadiusY(45)
                       //     .Fill(Color.FromHex("52485C"))
                       //     .VCenter()
                       //     .HCenter()
                    }
                    .VCenter()
                    .HCenter()
                }
                .BackgroundColor(Color.FromHex("EDEAEE"))
            };
        }
    }
}