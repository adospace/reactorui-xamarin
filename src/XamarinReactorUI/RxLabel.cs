using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxLabel
    {
        //
        // Summary:
        //     Gets or sets the maximum number of lines allowed in the Xamarin.Forms.Label.
        //
        // Remarks:
        //     To be added.
        int MaxLines { get; set; }
        //
        // Summary:
        //     Gets or sets the multiplier to apply to the default line height when displaying
        //     text.
        //
        // Remarks:
        //     To be added.
        double LineHeight { get; set; }
        //
        // Summary:
        //     Gets the size of the font for the label.
        //
        // Remarks:
        //     To be added.
        [TypeConverter(typeof(FontSizeConverter))]
        double FontSize { get; set; }
        //
        // Summary:
        //     Gets the font family to which the font for the label belongs.
        //
        // Remarks:
        //     To be added.
        string FontFamily { get; set; }
        //
        // Summary:
        //     Gets or sets the Xamarin.Forms.TextDecorations applied to Xamarin.Forms.Label.Text.
        //
        // Remarks:
        //     To be added.
        TextDecorations TextDecorations { get; set; }
        //
        // Summary:
        //     Gets a value that indicates whether the font for the label is bold, italic, or
        //     neither.
        //
        // Remarks:
        //     To be added.
        FontAttributes FontAttributes { get; set; }
        //
        // Summary:
        //     Gets or sets the vertical alignement of the Text property. This is a bindable
        //     property.
        //
        // Remarks:
        //     To be added.
        TextAlignment VerticalTextAlignment { get; set; }
        //
        // Summary:
        //     To be added.
        //
        // Remarks:
        //     To be added.
        TextType TextType { get; set; }
        //
        // Summary:
        //     Gets or sets the Xamarin.Forms.Color for the text of this Label. This is a bindable
        //     property.
        Color TextColor { get; set; }
        //
        // Summary:
        //     Gets or sets the text for the Label. This is a bindable property.
        //
        // Remarks:
        //     Setting Text to a non-null value will set the FormattedText property to null.
        string Text { get; set; }
        //
        // Summary:
        //     Gets or sets the LineBreakMode for the Label. This is a bindable property.
        LineBreakMode LineBreakMode { get; set; }
        //
        // Summary:
        //     Gets or sets the horizontal alignment of the Text property. This is a bindable
        //     property.
        //
        // Remarks:
        //     To be added.
        TextAlignment HorizontalTextAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets the formatted text for the Label. This is a bindable property.
        //
        // Remarks:
        //     Setting FormattedText to a non-null value will set the Text property to null.
        FormattedString FormattedText { get; set; }

        Thickness Padding { get; set; }
        double CharacterSpacing { get; set; }
    }

    public class RxLabel : RxView<Xamarin.Forms.Label>, IRxLabel
    {
        public RxLabel()
        { 
        
        }
        
        public RxLabel(string text)
        {
            Text = text;
        }

        private readonly NullableField<string> _text = new NullableField<string>();
        public string Text { get => _text.GetValueOrDefault(); set => _text.Value = value; }

        private readonly NullableField<int> _maxLines = new NullableField<int>();
        public int MaxLines { get => _maxLines.GetValueOrDefault(); set => _maxLines.Value = value; }

        private readonly NullableField<double> _lineHeight = new NullableField<double>();
        public double LineHeight { get => _lineHeight.GetValueOrDefault(); set => _lineHeight.Value = value; }

        private readonly NullableField<double> _fontSize = new NullableField<double>();
        public double FontSize { get => _fontSize.GetValueOrDefault(); set => _fontSize.Value = value; }

        private readonly NullableField<string> _fontFamily = new NullableField<string>();
        public string FontFamily { get => _fontFamily.GetValueOrDefault(); set => _fontFamily.Value = value; }
        
        private readonly NullableField<TextDecorations> _textDecorations = new NullableField<TextDecorations>();
        public TextDecorations TextDecorations { get => _textDecorations.GetValueOrDefault(); set => _textDecorations.Value = value; }
        
        private readonly NullableField<TextAlignment> _verticalTextAlignment = new NullableField<TextAlignment>();
        public TextAlignment VerticalTextAlignment { get => _verticalTextAlignment.GetValueOrDefault(); set => _verticalTextAlignment.Value = value; }
        
        private readonly NullableField<TextType> _textType = new NullableField<TextType>();
        public TextType TextType { get => _textType.GetValueOrDefault(); set => _textType.Value = value; }
        
        private readonly NullableField<FontAttributes> _fontAttributes = new NullableField<FontAttributes>();
        public FontAttributes FontAttributes { get => _fontAttributes.GetValueOrDefault(); set => _fontAttributes.Value = value; }
        
        private readonly NullableField<Color> _textColor = new NullableField<Color>();
        public Color TextColor { get => _textColor.GetValueOrDefault(); set => _textColor.Value = value; }
        
        private readonly NullableField<LineBreakMode> _lineBreakMode = new NullableField<LineBreakMode>();
        public LineBreakMode LineBreakMode { get => _lineBreakMode.GetValueOrDefault(); set => _lineBreakMode.Value = value; }
        
        private readonly NullableField<TextAlignment> _horizontalTextAlignment = new NullableField<TextAlignment>();
        public TextAlignment HorizontalTextAlignment { get => _horizontalTextAlignment.GetValueOrDefault(); set => _horizontalTextAlignment.Value = value; }
                     
        private readonly NullableField<FormattedString> _formattedText = new NullableField<FormattedString>();
        public FormattedString FormattedText { get => _formattedText.GetValueOrDefault(); set => _formattedText.Value = value; }

        private readonly NullableField<Thickness> _padding = new NullableField<Thickness>();
        public Thickness Padding { get => _padding.GetValueOrDefault(); set => _padding.Value = value; }
        
        private readonly NullableField<double> _characterSpacing = new NullableField<double>();
        public double CharacterSpacing { get => _characterSpacing.GetValueOrDefault(); set => _characterSpacing.Value = value; }

        protected override void OnUpdate()
        {
            if (_text.HasValue) NativeControl.Text = _text.Value;
            if (_maxLines.HasValue) NativeControl.MaxLines = _maxLines.Value;
            if (_lineHeight.HasValue) NativeControl.LineHeight = _lineHeight.Value;
            if (_fontSize.HasValue) NativeControl.FontSize = _fontSize.Value;
            if (_fontFamily.HasValue) NativeControl.FontFamily = _fontFamily.Value;
            if (_textDecorations.HasValue) NativeControl.TextDecorations = _textDecorations.Value;
            if (_fontAttributes.HasValue) NativeControl.FontAttributes = _fontAttributes.Value;
            if (_verticalTextAlignment.HasValue) NativeControl.VerticalTextAlignment = _verticalTextAlignment.Value;
            if (_textType.HasValue) NativeControl.TextType = _textType.Value;
            if (_textColor.HasValue) NativeControl.TextColor = _textColor.Value;
            if (_lineBreakMode.HasValue) NativeControl.LineBreakMode = _lineBreakMode.Value;
            if (_horizontalTextAlignment.HasValue) NativeControl.HorizontalTextAlignment = _horizontalTextAlignment.Value;
            if (_formattedText.HasValue) NativeControl.FormattedText = _formattedText.Value;
            if (_padding.HasValue) NativeControl.Padding = _padding.Value;
            if (_characterSpacing.HasValue) NativeControl.CharacterSpacing = _characterSpacing.Value;

            
            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxLabelExtensions
    {
        public static T Text<T>(this T label, string text) where T : IRxLabel
        {
            label.Text = text;
            return label;
        }

        public static T FontFamily<T>(this T label, string text) where T : IRxLabel
        {
            label.FontFamily = text;
            return label;
        }

        public static T FontSize<T>(this T label, NamedSize size) where T : IRxLabel
        {
            label.FontSize = Device.GetNamedSize(size, typeof(Label));
            return label;
        }

        public static T FontSize<T>(this T label, double size) where T : IRxLabel
        {
            label.FontSize = size;
            return label;
        }

        public static T TextColor<T>(this T label, Color color) where T : IRxLabel
        {
            label.TextColor = color;
            return label;
        }

        public static T Padding<T>(this T label, Thickness margin) where T : IRxLabel
        {
            label.Padding = margin;
            return label;
        }

        public static T Padding<T>(this T label, double left, double right, double top, double bottom) where T : IRxLabel
        {
            label.Padding = new Thickness(left, right, top, bottom);
            return label;
        }
    }
}
