using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxCheckBox
    {
        bool IsChecked { get; set; }
        Color Color { get; set; }
        Action<object, CheckedChangedEventArgs> CheckedChangedAction { get; set; }

    }

    public class RxCheckBox : RxView<Xamarin.Forms.CheckBox>, IRxCheckBox
    {
        public RxCheckBox()
        {
        }

        public RxCheckBox(Action<CheckBox> componentRefAction)
            : base(componentRefAction)
        {
        }

        public bool IsChecked { get; set; } = (bool)CheckBox.IsCheckedProperty.DefaultValue;
        public Color Color { get; set; } = (Color)CheckBox.ColorProperty.DefaultValue;
        public Action<object, CheckedChangedEventArgs> CheckedChangedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.IsChecked = IsChecked;
            NativeControl.Color = Color;

            if (CheckedChangedAction != null)
                NativeControl.CheckedChanged += NativeControl_CheckedChanged;

            base.OnUpdate();
        }

        private void NativeControl_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckedChangedAction?.Invoke(sender, e);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.CheckedChanged -= NativeControl_CheckedChanged;

            base.OnMigrated(newNode);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxCheckBoxExtensions
    {
        public static T OnChecked<T>(this T checkbox, Action<object, CheckedChangedEventArgs> action) where T : IRxCheckBox
        {
            checkbox.CheckedChangedAction = action;
            return checkbox;
        }

        public static T IsChecked<T>(this T checkbox, bool isChecked) where T : IRxCheckBox
        {
            checkbox.IsChecked = isChecked;
            return checkbox;
        }

        public static T Color<T>(this T checkbox, Color color) where T : IRxCheckBox
        {
            checkbox.Color = color;
            return checkbox;
        }
    }
}