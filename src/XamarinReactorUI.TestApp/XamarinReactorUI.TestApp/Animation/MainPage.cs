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
            return new RxContentPage()
            {
                new RxImage()
                    .HCenter()
                    .VCenter()
                    .Source("city.jpg")
                    .OnTap(()=>SetState(s => s.Toggle = !s.Toggle))
                    //.Scale(State.Toggle ? 1.0 : 0.5)
                    //.Opacity(State.Toggle ? 1.0 : 0.2)
                    //.WithAnimation(duration: 2000)
                    .Rotation(State.Toggle ? 180.0 : 0.0)
                    .WithAnimation(duration: 10000)
                    //.When(State.Toggle, _=>_.WithAnimation(easing: Easing.BounceIn, duration: 10000))
                    //.When(!State.Toggle, _=>_.WithAnimation(duration: 1000))

            };
        }
    }
}