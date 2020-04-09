using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Layout
{
    public class TestPageState : IState
    { 
        public bool ToggleLayout { get; set; }
    }

    public class TestPageComponent : RxComponent<TestPageState>
    {
        public override VisualNode Render()
        {
            return new RxContentPage(Context.Get<ContentPage>("Page"))
            {
                new RxStackLayout()
                {
                    new RxButton("Toggle Layout")
                        .OnClick(()=> SetState(s => s.ToggleLayout = !s.ToggleLayout)),
                    State.ToggleLayout ?
                    StringsForm()
                    :
                    NumericsForm()
                }
            };
        }

        private RxComponent StringsForm() => new StringsForm();
        private RxComponent NumericsForm() => new NumericsForm();
    }
}