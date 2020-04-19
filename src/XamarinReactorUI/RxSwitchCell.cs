using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxSwitchCell
    {
        bool On { get; set; }
        string Text { get; set; }
        Color OnColor { get; set; }
        Action OnChangedAction { get; set; }
    }

    public class RxSwitchCell<T> : RxCell<T>, IRxSwitchCell where T : SwitchCell, new()
    {
        public RxSwitchCell()
        {
        }

        public RxSwitchCell(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public bool On { get; set; } = (bool)SwitchCell.OnProperty.DefaultValue;
        public string Text { get; set; } = (string)SwitchCell.TextProperty.DefaultValue;
        public Color OnColor { get; set; } = (Color)SwitchCell.OnColorProperty.DefaultValue;
        public Action OnChangedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.On = On;
            NativeControl.Text = Text;
            NativeControl.OnColor = OnColor;

            if (OnChangedAction != null)
                NativeControl.OnChanged += NativeControl_OnChanged;

            base.OnUpdate();
        }

        private void NativeControl_OnChanged(object sender, ToggledEventArgs e)
        {
            OnChangedAction?.Invoke();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.OnChanged -= NativeControl_OnChanged;
            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
                NativeControl.OnChanged -= NativeControl_OnChanged;
            base.OnUnmount();
        }
    }

    public class RxSwitchCell : RxSwitchCell<Xamarin.Forms.SwitchCell>
    {
        internal RxSwitchCell(SwitchCell switchCell)
        {
            _nativeControl = switchCell;
        }
    }

    public static class RxSwitchCellExtensions
    {
        public static T OnChanged<T>(this T switchcell, Action action) where T : IRxSwitchCell
        {
            switchcell.OnChangedAction = action;
            return switchcell;
        }

        public static T On<T>(this T switchcell, bool on) where T : IRxSwitchCell
        {
            switchcell.On = on;
            return switchcell;
        }

        public static T Text<T>(this T switchcell, string text) where T : IRxSwitchCell
        {
            switchcell.Text = text;
            return switchcell;
        }

        public static T OnColor<T>(this T switchcell, Color onColor) where T : IRxSwitchCell
        {
            switchcell.OnColor = onColor;
            return switchcell;
        }
    }
}