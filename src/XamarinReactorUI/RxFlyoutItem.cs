
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxFlyoutItem : IRxShellItem
    {
    }

    public class RxFlyoutItem<T> : RxShellItem<T>, IRxFlyoutItem where T : FlyoutItem, new()
    {
        public RxFlyoutItem()
        {

        }

        public RxFlyoutItem(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        protected override void OnUpdate()
        {

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxFlyoutItem : RxFlyoutItem<FlyoutItem>
    {
        public RxFlyoutItem()
        {

        }

        public RxFlyoutItem(Action<FlyoutItem> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static class RxFlyoutItemExtensions
    {
    }

}
