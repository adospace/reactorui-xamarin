using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxShellGroupItem
    {
        FlyoutDisplayOptions FlyoutDisplayOptions { get; set; }
    }

    public abstract class RxShellGroupItem<T> : RxBaseShellItem<T>, IRxShellGroupItem where T : Xamarin.Forms.ShellGroupItem, new()
    {
        public RxShellGroupItem()
        {
        }

        public RxShellGroupItem(string title)
            : base(title)
        {
        }

        public RxShellGroupItem(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public FlyoutDisplayOptions FlyoutDisplayOptions { get; set; } = (FlyoutDisplayOptions)ShellGroupItem.FlyoutDisplayOptionsProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.FlyoutDisplayOptions = FlyoutDisplayOptions;

            base.OnUpdate();
        }
    }

    public static class RxShellGroupItemExtensions
    {
        public static T FlyoutDisplayOptions<T>(this T shellgroupitem, FlyoutDisplayOptions flyoutDisplayOptions) where T : IRxShellGroupItem
        {
            shellgroupitem.FlyoutDisplayOptions = flyoutDisplayOptions;
            return shellgroupitem;
        }
    }
}