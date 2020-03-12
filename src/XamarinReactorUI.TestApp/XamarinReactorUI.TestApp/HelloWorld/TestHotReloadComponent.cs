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
            var page = Context.Get<Xamarin.Forms.ContentPage>("Page");

            return new RxContentPage(page)
            {
                new TestComponentWithState()
            }
            .Title("Xamarin Reactor UI Hot Reload")
            .BackgroundColor(Color.White)
            ;
        }
    }
}
