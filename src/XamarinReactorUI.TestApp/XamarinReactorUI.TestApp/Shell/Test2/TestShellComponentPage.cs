using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinReactorUI.TestApp.HelloWorld;

namespace XamarinReactorUI.TestApp.Shell.Test2
{
    public class TestShellComponentPage : RxComponent
    {
        public override VisualNode Render()
        {
            //var shell = Context.Get<Xamarin.Forms.Shell>("Page");

            return new RxShell()
            {
                new RxTabBar()
                {
                    new RxTab("Tab1")
                    {
                        new RxShellContent()
                        {
                            new RxContentPage()
                            {
                                new TestComponentWithState()
                            }
                        }
                    },
                    new RxTab("Tab2")
                    {
                        new RxShellContent()
                        {
                            new RxContentPage()
                            {
                                new TestComponentWithState()
                            }
                        }
                    }
                }
            }
            .Title("Xamarin Reactor Shell Test")
            .BackgroundColor(Color.White)
            ;
        }

    }
}
