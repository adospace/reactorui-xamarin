using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Theming
{
    public enum ThemeColor
    { 
        Dark,

        Light
    }

    public class MainPageState : IState
    {
        public ThemeColor ThemeColor { get; set; } = ThemeColor.Light;
    }

    public class MainPage : RxComponent<MainPageState>
    {
        public override VisualNode Render()
        {
            return new RxNavigationPage()
            {
                new RxContentPage()
                {
                    new RxStackLayout()
                    {
                        new RxLabel("Label"),
                        new RxEntry(),
                        new RxButton("Toggle Theme")
                            .OnClick(()=> SetState(s => s.ThemeColor = (s.ThemeColor == ThemeColor.Dark ? ThemeColor.Light : ThemeColor.Dark)))
                    }
                }
                .Title("Theming")
            }
            .UseTheme(State.ThemeColor == ThemeColor.Dark ? Theme.Dark() : Theme.Light());
        }
    }


}
