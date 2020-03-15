using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Layout
{
    public class NumericsFormState : IValueSet
    {
        public int Field3 { get; set; }
        public int Field4 { get; set; }
        public int Field5 { get; set; }
    }

    public class NumericsForm : RxComponent<NumericsFormState>
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
                new RxLabel("Field 3"),
                new RxEntry(State.Field3.ToString())
                    .OnTextChanged((s,e) => SetState(_=>_.Field3 = int.Parse(e.NewTextValue)))
                    .Keyboard(Keyboard.Numeric),
                new RxLabel("Field 4"),
                new RxEntry(State.Field4.ToString())
                    .OnTextChanged((s,e) => SetState(_=>_.Field4 = int.Parse(e.NewTextValue)))
                    .Keyboard(Keyboard.Numeric),
                new RxLabel("Field 5"),
                new RxEntry(State.Field5.ToString())
                    .OnTextChanged((s,e) => SetState(_=>_.Field5 = int.Parse(e.NewTextValue)))
                    .Keyboard(Keyboard.Numeric),
            };
        }
    }
}
