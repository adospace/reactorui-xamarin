using System;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Navigation
{
    public class ChildPageComponentState : IState
    {
        public int Value { get; set; }
    }

    public class ChildPageComponentProps : IProps
    {
        public int InitialValue { get; set; }

        public Action<int> OnValueSet { get; set; }
    }

    public class ChildPageComponent : RxComponent<ChildPageComponentState, ChildPageComponentProps>
    {
        protected override void OnMounted()
        {
            State.Value = Props.InitialValue;

            base.OnMounted();
        }

        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                new RxStackLayout()
                {
                    new RxEntry(State.Value.ToString())
                        .OnAfterTextChanged(_=>SetState(s => s.Value = int.Parse(_)))
                        .Keyboard(Keyboard.Numeric),
                    new RxButton("Back")
                        .OnClick(GoBack)
                }
                .VCenter()
                .HCenter()
            }
            .Title("Child Page");
        }

        private async void GoBack()
        {
            Props.OnValueSet(State.Value);

            await Navigation.PopAsync();
        }
    }
}