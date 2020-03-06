using Xamarin.Forms;

namespace XamarinReactorUI.TestApp
{
    internal class TestHelloWorld : RxComponent
    {
        public override VisualNode Render()
            => new RxStackLayout()
            {
                new RxLabel($"Welcome to Xamarin.Forms + ReactorUI!")
                    .FontSize(NamedSize.Title)
                    .HCenterAndExpand()
                    .VCenterAndExpand()
            };
    }
}