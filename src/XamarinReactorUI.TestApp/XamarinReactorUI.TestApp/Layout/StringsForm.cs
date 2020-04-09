using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.TestApp.Layout
{

    public class StringsFormState : IState
    {
        public string Field1 { get; set; }
        public string Field2 { get; set; }
    }

    public class StringsForm : RxComponent<StringsFormState>
    {
        protected override void OnMounted()
        {
            System.Diagnostics.Trace.WriteLine($"{GetType()} Mounted");
            base.OnMounted();
        }

        protected override void OnWillUnmount()
        {
            System.Diagnostics.Trace.WriteLine($"{GetType()} Will unmount");
            base.OnWillUnmount();
        }

        public override VisualNode Render()
        {
            return new RxStackLayout()
            {
                new RxLabel("Field 1"),
                new RxEntry(State.Field1)
                    .OnTextChanged((s,e) => State.Field1 = e.NewTextValue),
                new RxLabel("Field 2"),
                new RxEntry(State.Field2)
                    .OnTextChanged((s,e) => State.Field2 = e.NewTextValue),
            };
        }
    }

}
