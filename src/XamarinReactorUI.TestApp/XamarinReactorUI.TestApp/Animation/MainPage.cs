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
                new RxFrame()
                { 
                    new RxImage()
                        .HCenter()
                        .VCenter()
                        .Source("city.jpg")
                }
                .OnTap(()=>SetState(s => s.Toggle = !s.Toggle))
                .HasShadow(true)
                .Scale(State.Toggle ? 1.0 : 0.5)
                .Opacity(State.Toggle ? 1.0 : 0.0)
                .WithAnimation()
                .Margin(10)
            };
        }
    }
}