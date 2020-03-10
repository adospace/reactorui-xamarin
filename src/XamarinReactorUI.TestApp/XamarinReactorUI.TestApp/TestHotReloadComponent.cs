using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp
{
    public class TestHotReloadState : IValueSet
    {
        public int Counter { get; set; }
    }

    public class TestHotReloadComponent : RxComponent<TestHotReloadState>
    {
        public TestHotReloadComponent()
        {
        }

        public override VisualNode Render()
        {
            return new RxContentPage(GetContext<TestHotReloadContext>().Page)
            {
                new RxStackLayout()
                { 
                    new RxLabel($"Clicked {State.Counter} times!")
                        .HCenter(),
                    new RxButton("Click here!")
                        .OnClick(IncrementCounter)
                        .HCenter()
                        .VEnd()
                }
                .HFillAndExpand()
                .HCenterAndExpand()
                .Margin(10)
            }
            .Title("Xamarin Reactor UI Hot Reload")
            .BackgroundColor(Color.White)
            ;
        }

        private void IncrementCounter()
        {
            SetState(s => s.Counter++);
        }
    }
}
