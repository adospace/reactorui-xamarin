using System;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTextCell
    {
        string Text { get; set; }
        string Detail { get; set; }
        Color TextColor { get; set; }
        Color DetailColor { get; set; }
    }

    public class RxTextCell<T> : RxCell<T>, IRxTextCell where T : TextCell, new()
    {
        public RxTextCell()
        {
        }

        public RxTextCell(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public string Text { get; set; } = (string)TextCell.TextProperty.DefaultValue;
        public string Detail { get; set; } = (string)TextCell.DetailProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)TextCell.TextColorProperty.DefaultValue;
        public Color DetailColor { get; set; } = (Color)TextCell.DetailColorProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Text = Text;
            NativeControl.Detail = Detail;
            NativeControl.TextColor = TextColor;
            NativeControl.DetailColor = DetailColor;
            base.OnUpdate();
        }
    }

    public class RxTextCell : RxTextCell<TextCell>
    {
        internal RxTextCell(TextCell textCell)
        {
            _nativeControl = textCell;
        }

        protected internal override void OnLayoutCycleRequested()
        {
            Layout();
            base.OnLayoutCycleRequested();
        }
    }

    public static class RxTextCellExtensions
    {
        public static T Text<T>(this T textcell, string text) where T : IRxTextCell
        {
            textcell.Text = text;
            return textcell;
        }

        public static T Detail<T>(this T textcell, string detail) where T : IRxTextCell
        {
            textcell.Detail = detail;
            return textcell;
        }

        public static T TextColor<T>(this T textcell, Color textColor) where T : IRxTextCell
        {
            textcell.TextColor = textColor;
            return textcell;
        }

        public static T DetailColor<T>(this T textcell, Color detailColor) where T : IRxTextCell
        {
            textcell.DetailColor = detailColor;
            return textcell;
        }
    }
}