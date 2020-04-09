using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Navigation
{ 
    public class MainPageComponentState : IState
    {
        public int Value { get; set; }
    }

    public class MainPageComponent : RxComponent<MainPageComponentState>
    {
        public override VisualNode Render()
        {
            return new RxNavigationPage()
            {
                new RxContentPage()
                {
                    new RxStackLayout()
                    {
                        new RxLabel($"Value: {State.Value}")
                            .FontSize(NamedSize.Large),
                        new RxButton("Move To Page")
                            .OnClick(OpenChildPage)
                    }
                    .VCenter()
                    .HCenter()
                }
                .Title("Main Page")
            };
        }

        private async void OpenChildPage()
        {
            await Navigation.PushAsync<ChildPageComponent, ChildPageComponentProps>(_=>
            {
                _.InitialValue = State.Value;
                _.OnValueSet = this.OnValueSetFromChilPage;
            });
        }

        private void OnValueSetFromChilPage(int newValue)
        {
            SetState(s => s.Value = newValue);
        }
    }
}
