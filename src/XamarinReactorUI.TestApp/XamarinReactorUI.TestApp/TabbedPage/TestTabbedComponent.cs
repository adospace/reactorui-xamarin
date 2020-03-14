namespace XamarinReactorUI.TestApp.TabbedPage
{
    internal class TestTabbedComponent : RxComponent
    {
        public TestTabbedComponent()
        {
        }

        public override VisualNode Render()
        {
            return new RxTabbedPage(Context.Get<Xamarin.Forms.TabbedPage>("ContainerPage"))
            {
                new RxContentPage()
                {
                    new RxStackLayout()
                    {
                        new RxLabel("Page1")
                            .VCenterAndExpand()
                            .HCenter(),
                        new RxLabel("Under Page1")
                            .VCenterAndExpand()
                            .HCenter(),

                    }
                }
                .Title("Page1"),
                new RxContentPage()
                {
                    new RxStackLayout()
                    {
                        new RxLabel("Page2")
                            .VCenterAndExpand()
                            .HCenter()
                    }
                }
                .Title("Page2"),
                new RxContentPage()
                {
                    new RxStackLayout()
                    {
                        new RxLabel("Page3")
                            .VCenterAndExpand()
                            .HCenter()
                    }
                }
                .Title("Page3"),
            };
        }
    }
}