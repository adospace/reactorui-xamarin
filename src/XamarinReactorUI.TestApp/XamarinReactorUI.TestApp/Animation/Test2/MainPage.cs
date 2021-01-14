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
                            .HeightRequest(State.Toggle ? 55 : 65)
                            .WidthRequest(State.Toggle ? 100 : 255)
                            .RadiusX(State.Toggle ? 45 : 5)
                            .RadiusY(State.Toggle ? 45 : 5)
                            .Fill(State.Toggle ?  Color.FromHex("52485C") : Color.Olive)
                            .VCenter()
                            .HCenter()
                            .OnTap(()=>SetState(s => s.Toggle = !s.Toggle))
                            .WithAnimation()
                    }
                    .VCenter()
                    .HCenter()
                }
                .BackgroundColor(Color.FromHex("EDEAEE"))
            };
        }
    }
}