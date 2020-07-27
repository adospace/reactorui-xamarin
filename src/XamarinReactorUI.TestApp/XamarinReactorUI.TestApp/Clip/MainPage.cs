using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI.Shapes;

namespace XamarinReactorUI.TestApp.Clip
{
    public class MainPageState : IState
    {
        public bool ShowAll { get; set; }
    }

    class MainPage : RxComponent<MainPageState>
    {
        public override VisualNode Render()
        {
            return new RxNavigationPage()
            {
                new RxContentPage()
                {
                    new RxImage()
                        .OnTap(()=>SetState(s=> s.ShowAll = !s.ShowAll))
                        .HCenter()
                        .VCenter()
                        .Source("city.jpg")
                        .Clip(new RxEllipseGeometry()
                                .Center(State.ShowAll ? new Point(10,10) : new Point(220,220))
                                .RadiusX(State.ShowAll ? 500 : 20)
                                .RadiusY(State.ShowAll ? 500 : 20)
                                .WithAnimation())
                }
                .Title("Element Clip")
            };
        }
    }
}
