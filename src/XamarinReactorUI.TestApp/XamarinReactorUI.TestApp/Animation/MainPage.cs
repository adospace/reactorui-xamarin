using System;
using System.Collections.Generic;
using System.Text;

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
                new RxButton("Click me!")
                    .HCenter()
                    .VCenter()
                    .OnClick(()=>SetState(s => s.Toggle = !s.Toggle))
                    .Scale(State.Toggle ? 2.0 : 1.0)
                    .WithAnimation()
            };
        }
    }
}
