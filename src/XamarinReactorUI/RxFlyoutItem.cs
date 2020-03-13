
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxFlyoutItem
    {
    }

    public sealed class RxFlyoutItem : RxShellItem<FlyoutItem>, IRxFlyoutItem
    {
        public RxFlyoutItem()
        {

        }

        public RxFlyoutItem(Action<FlyoutItem> componentRefAction)
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

    public static class RxFlyoutItemExtensions
    {
    }

}
