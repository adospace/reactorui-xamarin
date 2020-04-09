using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.TestApp.HelloWorld
{
    public class TestComponentState : IValueSet
    {
        public int Counter { get; set; }
    }

    public class TestComponentWithState : RxComponent<TestComponentState>
    {
        public TestComponentWithState()
        {
        }

        public override VisualNode Render()
        {
            return new RxStackLayout()
            {
                new RxLabel($"Tapped {State.Counter} times!")
                    .HCenter(),
                new RxButton("Click here")
                    .OnClick(IncrementCounter)
                    .HCenter()
                    .VEnd()
            }
            .HFillAndExpand()
            .VCenterAndExpand()
            .Margin(10);
        }

        private void IncrementCounter()
        {
            System.Diagnostics.Debug.WriteLine("IncrementCounter");
            SetState(s => s.Counter++);
        }
    }

}
