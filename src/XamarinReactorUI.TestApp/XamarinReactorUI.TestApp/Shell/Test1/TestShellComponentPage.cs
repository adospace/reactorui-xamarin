using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI.TestApp.HelloWorld;

namespace XamarinReactorUI.TestApp.Shell.Test1
{
    public class TestShellContext : RxContext
    {
        public TestShellContext(Xamarin.Forms.Shell page)
        {
            this["Page"] = page;
        }
    }

    public class TestShellComponentPage : RxComponent
    {
        public override VisualNode Render()
        {
            var page = Context.Get<Xamarin.Forms.Shell>("Page");

            return new RxShell(page)
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
