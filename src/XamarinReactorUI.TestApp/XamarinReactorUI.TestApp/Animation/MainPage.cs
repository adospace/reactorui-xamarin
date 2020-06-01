using System.Reflection;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Animation
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
                new RxContentPage("Animation Demo")
                {
                    new RxImage()
                        .HCenter()
                        .VCenter()
                        .Source("city.jpg")
                        .OnTap(()=>SetState(s => s.Toggle = !s.Toggle))
                        .Opacity(State.Toggle ? 1.0 : 0.0)
                        .WithOutAnimation()
                        .Rotation(State.Toggle ? 0.0 : 180.0)
                        .Scale(State.Toggle ? 1.0 : 0.5)
                        .WithAnimation()
                        .Margin(10)
                        
                }
            };
        }
    }
}