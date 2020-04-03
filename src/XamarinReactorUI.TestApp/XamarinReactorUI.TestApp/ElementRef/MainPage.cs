using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.ElementRef
{
    public class MainPage : RxComponent
    {
        private Page _pageRef;

        public override VisualNode Render()
        {
            return new RxContentPage(pageRef => _pageRef = pageRef)
            {
                new RxButton("Click here!")
                    .OnClick(OnButtonClicked)
                    .VCenter()
                    .HCenter()
            };
        }

        private async void OnButtonClicked()
            => await _pageRef.DisplayAlert("Sample Page", "Hello!", "Close");
    }
}