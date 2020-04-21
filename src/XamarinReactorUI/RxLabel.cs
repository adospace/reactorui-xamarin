using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxLabel : IRxView
    {
        TextAlignment HorizontalTextAlignment { get; set; }
        TextAlignment VerticalTextAlignment { get; set; }
        Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        string Text { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        FontAttributes FontAttributes { get; set; }
        TextDecorations TextDecorations { get; set; }
        FormattedString FormattedText { get; set; }
        LineBreakMode LineBreakMode { get; set; }
        double LineHeight { get; set; }
        int MaxLines { get; set; }
        Thickness Padding { get; set; }
        TextType TextType { get; set; }
    }

    public class RxLabel<T> : RxView<T>, IRxLabel where T : Label, new()
    {
        public RxLabel()
        {
        }

        public RxLabel(string text)
        {
            Text = text;
        }

        public RxLabel(Action<Xamarin.Forms.Label> componentRefAction)
            : base(componentRefAction)
        {
        }

        public TextAlignment HorizontalTextAlignment { get; set; } = (TextAlignment)Label.HorizontalTextAlignmentProperty.DefaultValue;
        public TextAlignment VerticalTextAlignment { get; set; } = (TextAlignment)Label.VerticalTextAlignmentProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)Label.TextColorProperty.DefaultValue;
        public double CharacterSpacing { get; set; } = (double)Label.CharacterSpacingProperty.DefaultValue;
        public string Text { get; set; } = (string)Label.TextProperty.DefaultValue;
        public string FontFamily { get; set; } = (string)Label.FontFamilyProperty.DefaultValue;
        public double FontSize { get; set; } = (double)Label.FontSizeProperty.DefaultValue;
        public FontAttributes FontAttributes { get; set; } = (FontAttributes)Label.FontAttributesProperty.DefaultValue;
        public TextDecorations TextDecorations { get; set; } = (TextDecorations)Label.TextDecorationsProperty.DefaultValue;
        public FormattedString FormattedText { get; set; } = (FormattedString)Label.FormattedTextProperty.DefaultValue;
        public LineBreakMode LineBreakMode { get; set; } = (LineBreakMode)Label.LineBreakModeProperty.DefaultValue;
        public double LineHeight { get; set; } = (double)Label.LineHeightProperty.DefaultValue;
        public int MaxLines { get; set; } = (int)Label.MaxLinesProperty.DefaultValue;
        public Thickness Padding { get; set; } = (Thickness)Label.PaddingProperty.DefaultValue;
        public TextType TextType { get; set; } = (TextType)Label.TextTypeProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.HorizontalTextAlignment = HorizontalTextAlignment;
            NativeControl.VerticalTextAlignment = VerticalTextAlignment;
            NativeControl.TextColor = TextColor;
            NativeControl.CharacterSpacing = CharacterSpacing;
            NativeControl.Text = Text;
            NativeControl.FontFamily = FontFamily;
            NativeControl.FontSize = FontSize;
            NativeControl.FontAttributes = FontAttributes;
            NativeControl.TextDecorations = TextDecorations;
            NativeControl.FormattedText = FormattedText;
            NativeControl.LineBreakMode = LineBreakMode;
            NativeControl.LineHeight = LineHeight;
            NativeControl.MaxLines = MaxLines;
            NativeControl.Padding = Padding;
            NativeControl.TextType = TextType;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxLabel : RxLabel<Label>
    {
        public RxLabel()
        {
        }

        public RxLabel(string text)
            : base(text)
        {
        }

        public RxLabel(Action<Xamarin.Forms.Label> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxLabelExtensions
    {
        public static T HorizontalTextAlignment<T>(this T label, TextAlignment horizontalTextAlignment) where T : IRxLabel
        {
            label.HorizontalTextAlignment = horizontalTextAlignment;
            return label;
        }

        public static T VerticalTextAlignment<T>(this T label, TextAlignment verticalTextAlignment) where T : IRxLabel
        {
            label.VerticalTextAlignment = verticalTextAlignment;
            return label;
        }

        public static T TextColor<T>(this T label, Color textColor) where T : IRxLabel
        {
            label.TextColor = textColor;
            return label;
        }

        public static T CharacterSpacing<T>(this T label, double characterSpacing) where T : IRxLabel
        {
            label.CharacterSpacing = characterSpacing;
            return label;
        }

        public static T Text<T>(this T label, string text) where T : IRxLabel
        {
            label.Text = text;
            return label;
        }

        public static T FontFamily<T>(this T label, string fontFamily) where T : IRxLabel
        {
            label.FontFamily = fontFamily;
            return label;
        }

        public static T FontSize<T>(this T label, double fontSize) where T : IRxLabel
        {
            label.FontSize = fontSize;
            return label;
        }

        public static T FontSize<T>(this T label, NamedSize size) where T : IRxLabel
        {
            label.FontSize = Device.GetNamedSize(size, typeof(Label));
            return label;
        }

        public static T FontAttributes<T>(this T label, FontAttributes fontAttributes) where T : IRxLabel
        {
            label.FontAttributes = fontAttributes;
            return label;
        }

        public static T TextDecorations<T>(this T label, TextDecorations textDecorations) where T : IRxLabel
        {
            label.TextDecorations = textDecorations;
            return label;
        }

        public static T FormattedText<T>(this T label, FormattedString formattedText) where T : IRxLabel
        {
            label.FormattedText = formattedText;
            return label;
        }

        public static T LineBreakMode<T>(this T label, LineBreakMode lineBreakMode) where T : IRxLabel
        {
            label.LineBreakMode = lineBreakMode;
            return label;
        }

        public static T LineHeight<T>(this T label, double lineHeight) where T : IRxLabel
        {
            label.LineHeight = lineHeight;
            return label;
        }

        public static T MaxLines<T>(this T label, int maxLines) where T : IRxLabel
        {
            label.MaxLines = maxLines;
            return label;
        }

        public static T Padding<T>(this T label, Thickness padding) where T : IRxLabel
        {
            label.Padding = padding;
            return label;
        }

        public static T Padding<T>(this T label, double leftRight, double topBottom) where T : IRxLabel
        {
            label.Padding = new Thickness(leftRight, topBottom);
            return label;
        }

        public static T Padding<T>(this T label, double left, double top, double right, double bottom) where T : IRxLabel
        {
            label.Padding = new Thickness(left, top, right, bottom);
            return label;
        }

        public static T Padding<T>(this T label, double uniformSize) where T : IRxLabel
        {
            label.Padding = new Thickness(uniformSize);
            return label;
        }

        public static T TextType<T>(this T label, TextType textType) where T : IRxLabel
        {
            label.TextType = textType;
            return label;
        }
    }
}