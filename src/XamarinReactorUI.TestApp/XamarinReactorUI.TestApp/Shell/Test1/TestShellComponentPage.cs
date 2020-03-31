using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI.TestApp.HelloWorld;

namespace XamarinReactorUI.TestApp.Shell.Test1
{
    public class TestShellComponentPage : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxShell()
            {
                new RxContentPage()
                {
                    new TestComponentWithState()
                }
            }
            .Title("Xamarin Reactor Shell Test")
            .BackgroundColor(Color.White)
            ;
        }

    }
}
