using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxEntryCell : IRxCell
    {
        string Text { get; set; }
        string Label { get; set; }
        string Placeholder { get; set; }
        Color LabelColor { get; set; }
        Keyboard Keyboard { get; set; }
        TextAlignment HorizontalTextAlignment { get; set; }
        TextAlignment VerticalTextAlignment { get; set; }
        Action<string> CompletedAction { get; set; }
    }

    public class RxEntryCell<T> : RxCell<T>, IRxEntryCell where T : EntryCell, new()
    {
        public RxEntryCell()
        {
        }

        public RxEntryCell(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public string Text { get; set; } = (string)EntryCell.TextProperty.DefaultValue;
        public string Label { get; set; } = (string)EntryCell.LabelProperty.DefaultValue;
        public string Placeholder { get; set; } = (string)EntryCell.PlaceholderProperty.DefaultValue;
        public Color LabelColor { get; set; } = (Color)EntryCell.LabelColorProperty.DefaultValue;
        public Keyboard Keyboard { get; set; } = (Keyboard)EntryCell.KeyboardProperty.DefaultValue;
        public TextAlignment HorizontalTextAlignment { get; set; } = (TextAlignment)EntryCell.HorizontalTextAlignmentProperty.DefaultValue;
        public TextAlignment VerticalTextAlignment { get; set; } = (TextAlignment)EntryCell.VerticalTextAlignmentProperty.DefaultValue;
        public Action<string> CompletedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.Text = Text;
            NativeControl.Label = Label;
            NativeControl.Placeholder = Placeholder;
            NativeControl.LabelColor = LabelColor;
            NativeControl.Keyboard = Keyboard;
            NativeControl.HorizontalTextAlignment = HorizontalTextAlignment;
            NativeControl.VerticalTextAlignment = VerticalTextAlignment;

            if (CompletedAction != null)
                NativeControl.Completed += NativeControl_Completed;

            base.OnUpdate();
        }

        private void NativeControl_Completed(object sender, EventArgs e)
        {
            if (NativeControl != null)
                CompletedAction?.Invoke(NativeControl.Text);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.Tapped -= NativeControl_Completed;
            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
                NativeControl.Tapped -= NativeControl_Completed;
            base.OnUnmount();
        }
    }

    public class RxEntryCell : RxEntryCell<EntryCell>
    {
        internal RxEntryCell(EntryCell entry)
        {
            _nativeControl = entry;
        }

        protected internal override void OnLayoutCycleRequested()
        {
            Layout();
            base.OnLayoutCycleRequested();
        }
    }

    public static class RxEntryCellExtensions
    {
        public static T OnCompleted<T>(this T entryCell, Action<string> action) where T : IRxEntryCell
        {
            entryCell.CompletedAction = action;
            return entryCell;
        }

        public static T Text<T>(this T entrycell, string text) where T : IRxEntryCell
        {
            entrycell.Text = text;
            return entrycell;
        }

        public static T Label<T>(this T entrycell, string label) where T : IRxEntryCell
        {
            entrycell.Label = label;
            return entrycell;
        }

        public static T Placeholder<T>(this T entrycell, string placeholder) where T : IRxEntryCell
        {
            entrycell.Placeholder = placeholder;
            return entrycell;
        }

        public static T LabelColor<T>(this T entrycell, Color labelColor) where T : IRxEntryCell
        {
            entrycell.LabelColor = labelColor;
            return entrycell;
        }

        public static T Keyboard<T>(this T entrycell, Keyboard keyboard) where T : IRxEntryCell
        {
            entrycell.Keyboard = keyboard;
            return entrycell;
        }

        public static T HorizontalTextAlignment<T>(this T entrycell, TextAlignment horizontalTextAlignment) where T : IRxEntryCell
        {
            entrycell.HorizontalTextAlignment = horizontalTextAlignment;
            return entrycell;
        }

        public static T VerticalTextAlignment<T>(this T entrycell, TextAlignment verticalTextAlignment) where T : IRxEntryCell
        {
            entrycell.VerticalTextAlignment = verticalTextAlignment;
            return entrycell;
        }
    }
}