using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.HelloWorld
{
    internal class TestComponent : RxComponent
    {
        int _counter;

        void IncreaseCounter()
        {
            _counter += 10;

            Invalidate();
        }

        public override VisualNode Render() 
            => new RxStackLayout()
            {
                new RxLabel($"Button clicked {_counter} times!")
                    .HorizontalOptions(LayoutOptions.Center)
                    ,
                new RxButton("Click here!")
                    .HorizontalOptions(LayoutOptions.Center)
                    .OnClick(IncreaseCounter)
            }
            .HorizontalOptions(LayoutOptions.CenterAndExpand)
            .VerticalOptions(LayoutOptions.CenterAndExpand);
    }
}