using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxSwitch
    {
        bool IsToggled { get; set; }
        Color OnColor { get; set; }
        Color ThumbColor { get; set; }
        Action<object, ToggledEventArgs> ToggledChangedAction { get; set; }
    }

    public class RxSwitch : RxView<Switch>, IRxSwitch
    {
        public RxSwitch()
        {
        }

        public RxSwitch(Action<Switch> componentRefAction)
            : base(componentRefAction)
        {
        }

        public bool IsToggled { get; set; } = (bool)Switch.IsToggledProperty.DefaultValue;
        public Color OnColor { get; set; } = (Color)Switch.OnColorProperty.DefaultValue;
        public Color ThumbColor { get; set; } = (Color)Switch.ThumbColorProperty.DefaultValue;
        public Action<object, ToggledEventArgs> ToggledChangedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.IsToggled = IsToggled;
            NativeControl.OnColor = OnColor;
            NativeControl.ThumbColor = ThumbColor;

            if (ToggledChangedAction != null)
                NativeControl.Toggled += NativeControl_Toggled;
            

            base.OnUpdate();
        }

        private void NativeControl_Toggled(object sender, ToggledEventArgs e)
        {
            ToggledChangedAction?.Invoke(sender, e);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.Toggled -= NativeControl_Toggled;

            base.OnMigrated(newNode);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxSwitchExtensions
    {
        public static T OnToggled<T>(this T checkbox, Action<object, ToggledEventArgs> action) where T : IRxSwitch
        {
            checkbox.ToggledChangedAction = action;
            return checkbox;
        }

        public static T IsToggled<T>(this T s, bool isToggled) where T : IRxSwitch
        {
            s.IsToggled = isToggled;
            return s;
        }

        public static T OnColor<T>(this T s, Color onColor) where T : IRxSwitch
        {
            s.OnColor = onColor;
            return s;
        }

        public static T ThumbColor<T>(this T s, Color thumbColor) where T : IRxSwitch
        {
            s.ThumbColor = thumbColor;
            return s;
        }
    }
}