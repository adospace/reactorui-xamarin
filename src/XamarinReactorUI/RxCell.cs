using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxCell
    {
        Action TappedAction { get; set; }
        bool IsEnabled { get; set; }
    }

    public abstract class RxCell<T> : RxElement<T>, IRxCell where T : Cell, new()
    {
        public RxCell()
        {
        }

        public RxCell(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public bool IsEnabled { get; set; } = (bool)Cell.IsEnabledProperty.DefaultValue;
        public Action TappedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.IsEnabled = IsEnabled;
            if (TappedAction != null)
                NativeControl.Tapped += NativeControl_Tapped;

            base.OnUpdate();
        }

        private void NativeControl_Tapped(object sender, EventArgs e)
        {
            TappedAction?.Invoke();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.Tapped -= NativeControl_Tapped;
            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
                NativeControl.Tapped -= NativeControl_Tapped;
            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxCellExtensions
    {
        public static T OnTapped<T>(this T textcell, Action action) where T : IRxCell
        {
            textcell.TappedAction = action;
            return textcell;
        }

        public static T IsEnabled<T>(this T cell, bool isEnabled) where T : IRxCell
        {
            cell.IsEnabled = isEnabled;
            return cell;
        }
    }
}