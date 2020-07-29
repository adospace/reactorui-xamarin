using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Shell.Test4
{
    class Page1 : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxShell()
            {
                new RxContentPage()
                {
                    new RxStackLayout()
                    {
                        new RxLabel("Page 1"),
                        new RxButton("Go Next")
                            .OnClick(async ()=> await Navigation.PushAsync<Page2>())
                    }
                    .WithVerticalOrientation()
                    .HCenter()
                    .VCenter()
                }
            }
            .Title("Xamarin Reactor Shell Nav Test")
            .FlyoutIsPresented(false)
            .FlyoutBehavior(FlyoutBehavior.Disabled)
            ;
        }

    }
}
