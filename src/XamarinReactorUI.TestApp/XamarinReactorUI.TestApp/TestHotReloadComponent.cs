using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp
{
    public class TestHotReloadComponent : RxComponent
    {
        private readonly ContentPage _contentPage;

        public TestHotReloadComponent(ContentPage contentPage)
        {
            _contentPage = contentPage;
        }

        public override VisualNode Render()
        {
            return new RxContentPage(_contentPage)
            {
                new RxStackLayout()
                { 
                    new RxLabel("Hello world")
                }
                .HCenter()
                .VCenter()
            }
            .Title("Xamarin Reactor UI Hot Reload")
            .BackgroundColor(Color.White)
            ;
        }
    }
}
