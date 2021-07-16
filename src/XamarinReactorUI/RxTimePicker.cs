using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTimePicker : IRxView
    {
        string Format { get; set; }
        Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        TimeSpan Time { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        FontAttributes FontAttributes { get; set; }
        Action<TimeSpan> TimeChangedAction { get; set; }
    }

    public class RxTimePicker<T> : RxView<T>, IRxTimePicker where T : TimePicker, new()
    {
        public RxTimePicker()
        {
        }

        public RxTimePicker(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public string Format { get; set; } = (string)TimePicker.FormatProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)TimePicker.TextColorProperty.DefaultValue;
        public double CharacterSpacing { get; set; } = (double)TimePicker.CharacterSpacingProperty.DefaultValue;
        public TimeSpan Time { get; set; } = (TimeSpan)TimePicker.TimeProperty.DefaultValue;
        public string FontFamily { get; set; } = (string)TimePicker.FontFamilyProperty.DefaultValue;
        public double FontSize { get; set; } = (double)TimePicker.FontSizeProperty.DefaultValue;
        public FontAttributes FontAttributes { get; set; } = (FontAttributes)TimePicker.FontAttributesProperty.DefaultValue;
        public Action<TimeSpan> TimeChangedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.Format = Format;
            NativeControl.TextColor = TextColor;
            NativeControl.CharacterSpacing = CharacterSpacing;
            NativeControl.Time = Time;
            NativeControl.FontFamily = FontFamily;
            NativeControl.FontSize = FontSize;
            NativeControl.FontAttributes = FontAttributes;


            if (TimeChangedAction != null)
                NativeControl.PropertyChanged += NativeControl_PropertyChanged;

            base.OnUpdate();
        }

        private void NativeControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
            {
                TimeChangedAction?.Invoke(NativeControl.Time);
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
            }

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
            {
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
            }

            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxTimePicker : RxTimePicker<TimePicker>
    {
        public RxTimePicker()
        {
        }

        public RxTimePicker(Action<TimePicker> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxTimePickerExtensions
    {
        public static T OnTimeChanged<T>(this T timepicker, Action<TimeSpan> action) where T : IRxTimePicker
        {
            timepicker.TimeChangedAction = action;
            return timepicker;
        }

        public static T Format<T>(this T timepicker, string format) where T : IRxTimePicker
        {
            timepicker.Format = format;
            return timepicker;
        }

        public static T TextColor<T>(this T timepicker, Color textColor) where T : IRxTimePicker
        {
            timepicker.TextColor = textColor;
            return timepicker;
        }

        public static T CharacterSpacing<T>(this T timepicker, double characterSpacing) where T : IRxTimePicker
        {
            timepicker.CharacterSpacing = characterSpacing;
            return timepicker;
        }

        public static T Time<T>(this T timepicker, TimeSpan time) where T : IRxTimePicker
        {
            timepicker.Time = time;
            return timepicker;
        }

        public static T FontFamily<T>(this T timepicker, string fontFamily) where T : IRxTimePicker
        {
            timepicker.FontFamily = fontFamily;
            return timepicker;
        }

        public static T FontSize<T>(this T timepicker, double fontSize) where T : IRxTimePicker
        {
            timepicker.FontSize = fontSize;
            return timepicker;
        }

        public static T FontSize<T>(this T timepicker, NamedSize size) where T : IRxTimePicker
        {
            timepicker.FontSize = Device.GetNamedSize(size, typeof(TimePicker));
            return timepicker;
        }

        public static T FontAttributes<T>(this T timepicker, FontAttributes fontAttributes) where T : IRxTimePicker
        {
            timepicker.FontAttributes = fontAttributes;
            return timepicker;
        }
    }
}