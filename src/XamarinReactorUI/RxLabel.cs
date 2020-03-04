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

        public string Text { get; set; } = (string)Label.TextProperty.DefaultValue;

        public int MaxLines { get; set; } = (int)Label.MaxLinesProperty.DefaultValue;

        public double LineHeight { get; set; } = (double)Label.LineHeightProperty.DefaultValue;

        public double FontSize { get; set; } = (double)Label.FontSizeProperty.DefaultValue;

        public string FontFamily { get; set; } = (string)Label.FontFamilyProperty.DefaultValue;

        public TextDecorations TextDecorations { get; set; } = (TextDecorations)Label.TextDecorationsProperty.DefaultValue;

        public TextAlignment VerticalTextAlignment { get; set; } = (TextAlignment)Label.VerticalTextAlignmentProperty.DefaultValue;

        public TextType TextType { get; set; } = (TextType)Label.TextTypeProperty.DefaultValue;

        public FontAttributes FontAttributes { get; set; } = (FontAttributes)Label.FontAttributesProperty.DefaultValue;

        public Color TextColor { get; set; } = (Color)Label.TextColorProperty.DefaultValue;

        public LineBreakMode LineBreakMode { get; set; } = (LineBreakMode)Label.LineBreakModeProperty.DefaultValue;

        public TextAlignment HorizontalTextAlignment { get; set; } = (TextAlignment)Label.HorizontalTextAlignmentProperty.DefaultValue;

        public FormattedString FormattedText { get; set; } = (FormattedString)Label.FormattedTextProperty.DefaultValue;

        public Thickness Padding { get; set; } = (Thickness)Label.PaddingProperty.DefaultValue;

        public double CharacterSpacing { get; set; } = (double)Label.CharacterSpacingProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Text = Text;
            NativeControl.MaxLines = MaxLines;
            NativeControl.LineHeight = LineHeight;
            NativeControl.FontSize = FontSize;
            NativeControl.FontFamily = FontFamily;
            NativeControl.TextDecorations = TextDecorations;
            NativeControl.FontAttributes = FontAttributes;
            NativeControl.VerticalTextAlignment = VerticalTextAlignment;
            NativeControl.TextType = TextType;
            NativeControl.TextColor = TextColor;
            NativeControl.LineBreakMode = LineBreakMode;
            NativeControl.HorizontalTextAlignment = HorizontalTextAlignment;
            NativeControl.FormattedText = FormattedText;
            NativeControl.Padding = Padding;
            NativeControl.CharacterSpacing = CharacterSpacing;

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
