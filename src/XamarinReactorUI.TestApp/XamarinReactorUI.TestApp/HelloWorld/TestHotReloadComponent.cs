using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.HelloWorld
{
    public class TestHotReloadComponent : RxComponent
    {
        public TestHotReloadComponent()
        {
        }

        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                new TestComponentWithState()
            }
            .Title("Xamarin Reactor UI Hot Reload")
            .BackgroundColor(Color.White)
            ;
        }
    }
}
