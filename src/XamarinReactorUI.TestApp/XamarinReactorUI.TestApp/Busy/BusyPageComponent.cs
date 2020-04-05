using System.Threading.Tasks;

namespace XamarinReactorUI.TestApp.Busy
{
    public class BusyPageState : IValueSet
    {
        public bool IsBusy { get; set; }
    }

    public class BusyPageComponent : RxComponent<BusyPageState>
    {
        protected override void OnMounted()
        {
            SetState(_ => _.IsBusy = true);

            //OnMounted is called on UI Thread, move the slow code to a background thread
            Task.Run(async () =>
            {
                //Simulate lenghty work
                await Task.Delay(3000);

                SetState(_ => _.IsBusy = false);
            });

            base.OnMounted();
        }

        public override VisualNode Render()
        {
            return new RxContentPage()
            {
                State.IsBusy ?
                new BusyComponent()
                    .Message("Loading")
                    .IsBusy(true)
                :
                RenderPage()
            };
        }

        private VisualNode RenderPage()
            => new RxLabel("Done!")
                    .VCenter()
                    .HCenter();
    }
}