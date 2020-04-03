namespace XamarinReactorUI.TestApp.Busy
{
    public class BusyComponent : RxComponent
    {
        private string _message;
        private bool _isBusy;

        public BusyComponent Message(string message)
        {
            _message = message;
            return this;
        }

        public BusyComponent IsBusy(bool isBusy)
        {
            _isBusy = isBusy;
            return this;
        }

        public override VisualNode Render()
        {
            if (!_isBusy)
                return null;

            return new RxStackLayout()
            {
                new RxActivityIndicator()
                    .IsRunning(true),
                new RxLabel()
                    .Text(_message)
            }
            .VCenter()
            .HCenter();
        }
    }
}