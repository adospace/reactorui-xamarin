using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.Grid
{
    internal class GridPageComponent : RxComponent
    {
        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                new RxGrid("* *", "*")
                {
                    new RxLabel("Row 1")
                        .GridRow(0),
                    new RxLabel("Row 2")
                        .GridRow(1),
                }
            };
        }
    }

}