using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxDatePicker : IRxView
    {
        string Format { get; set; }
        DateTime Date { get; set; }
        DateTime MinimumDate { get; set; }
        DateTime MaximumDate { get; set; }
        Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        FontAttributes FontAttributes { get; set; }
        Action<DateChangedEventArgs> DateChangedAction { get; set; }
    }

    public class RxDatePicker<T> : RxView<T>, IRxDatePicker where T : DatePicker, new()
    {
        public RxDatePicker()
        {
        }

        public RxDatePicker(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public string Format { get; set; } = (string)DatePicker.FormatProperty.DefaultValue;
        public DateTime Date { get; set; } = (DateTime)DatePicker.DateProperty.DefaultValue;
        public DateTime MinimumDate { get; set; } = (DateTime)DatePicker.MinimumDateProperty.DefaultValue;
        public DateTime MaximumDate { get; set; } = (DateTime)DatePicker.MaximumDateProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)DatePicker.TextColorProperty.DefaultValue;
        public double CharacterSpacing { get; set; } = (double)DatePicker.CharacterSpacingProperty.DefaultValue;
        public string FontFamily { get; set; } = (string)DatePicker.FontFamilyProperty.DefaultValue;
        public double FontSize { get; set; } = (double)DatePicker.FontSizeProperty.DefaultValue;
        public FontAttributes FontAttributes { get; set; } = (FontAttributes)DatePicker.FontAttributesProperty.DefaultValue;
        public Action<DateChangedEventArgs> DateChangedAction { get; set; }

        protected override void OnUpdate()
        {
            if (NativeControl.Format != Format) NativeControl.Format = Format;
            if (NativeControl.Date != Date) NativeControl.Date = Date;
            if (NativeControl.MinimumDate != MinimumDate) NativeControl.MinimumDate = MinimumDate;
            if (NativeControl.TextColor != TextColor) NativeControl.TextColor = TextColor;
            if (NativeControl.CharacterSpacing != CharacterSpacing) NativeControl.CharacterSpacing = CharacterSpacing;
            if (NativeControl.FontFamily != FontFamily) NativeControl.FontFamily = FontFamily;
            if (NativeControl.FontSize != FontSize) NativeControl.FontSize = FontSize;
            if (NativeControl.FontAttributes != FontAttributes) NativeControl.FontAttributes = FontAttributes;
            if (NativeControl.FontAttributes != FontAttributes) NativeControl.FontAttributes = FontAttributes;


            if (DateChangedAction != null)
                NativeControl.DateSelected += NativeControl_DateSelected;

            base.OnUpdate();
        }

        private void NativeControl_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateChangedAction?.Invoke(e);            
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.DateSelected -= NativeControl_DateSelected;

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
            {
                NativeControl.DateSelected -= NativeControl_DateSelected;
            }

            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxDatePicker : RxDatePicker<DatePicker>
    {
        public RxDatePicker()
        {
        }

        public RxDatePicker(Action<DatePicker> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxDatePickerExtensions
    {
        public static T OnDateChanged<T>(this T datepicker, Action<DateChangedEventArgs> action) where T : IRxDatePicker
        {
            datepicker.DateChangedAction = action;
            return datepicker;
        }

        public static T Format<T>(this T datepicker, string format) where T : IRxDatePicker
        {
            datepicker.Format = format;
            return datepicker;
        }

        public static T Date<T>(this T datepicker, DateTime date) where T : IRxDatePicker
        {
            datepicker.Date = date;
            return datepicker;
        }

        public static T MinimumDate<T>(this T datepicker, DateTime minimumDate) where T : IRxDatePicker
        {
            datepicker.MinimumDate = minimumDate;
            return datepicker;
        }

        public static T MaximumDate<T>(this T datepicker, DateTime maximumDate) where T : IRxDatePicker
        {
            datepicker.MaximumDate = maximumDate;
            return datepicker;
        }

        public static T TextColor<T>(this T datepicker, Color textColor) where T : IRxDatePicker
        {
            datepicker.TextColor = textColor;
            return datepicker;
        }

        public static T CharacterSpacing<T>(this T datepicker, double characterSpacing) where T : IRxDatePicker
        {
            datepicker.CharacterSpacing = characterSpacing;
            return datepicker;
        }

        public static T FontFamily<T>(this T datepicker, string fontFamily) where T : IRxDatePicker
        {
            datepicker.FontFamily = fontFamily;
            return datepicker;
        }

        public static T FontSize<T>(this T datepicker, double fontSize) where T : IRxDatePicker
        {
            datepicker.FontSize = fontSize;
            return datepicker;
        }

        public static T FontSize<T>(this T datepicker, NamedSize size) where T : IRxDatePicker
        {
            datepicker.FontSize = Device.GetNamedSize(size, typeof(DatePicker));
            return datepicker;
        }

        public static T FontAttributes<T>(this T datepicker, FontAttributes fontAttributes) where T : IRxDatePicker
        {
            datepicker.FontAttributes = fontAttributes;
            return datepicker;
        }
    }
}