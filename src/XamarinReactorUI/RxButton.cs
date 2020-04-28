using System;
using System.Collections.Generic;
using Xamarin.Forms;
using static Xamarin.Forms.Button;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace XamarinReactorUI
{
    public interface IRxButton : IRxView
    {
        Action ClickAction { get; set; }
        ButtonContentLayout ContentLayout { get; set; }
        string Text { get; set; }
        Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        Font Font { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        FontAttributes FontAttributes { get; set; }
        double BorderWidth { get; set; }
        Color BorderColor { get; set; }
        int CornerRadius { get; set; }
        ImageSource ImageSource { get; set; }
        Thickness Padding { get; set; }
    }

    public class RxButton<T> : RxView<T>, IRxButton where T : Button, new()
    {
        public RxButton()
        {
        }

        public RxButton(string text)
        {
            Text = text;
        }

        public RxButton(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public ButtonContentLayout ContentLayout { get; set; } = (ButtonContentLayout)Button.ContentLayoutProperty.DefaultValue;
        public string Text { get; set; } = (string)Button.TextProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)Button.TextColorProperty.DefaultValue;
        public double CharacterSpacing { get; set; } = (double)Button.CharacterSpacingProperty.DefaultValue;
        public Font Font { get; set; } = (Font)Button.FontProperty.DefaultValue;
        public string FontFamily { get; set; } = (string)Button.FontFamilyProperty.DefaultValue;
        public double FontSize { get; set; } = (double)Button.FontSizeProperty.DefaultValue;
        public FontAttributes FontAttributes { get; set; } = (FontAttributes)Button.FontAttributesProperty.DefaultValue;
        public double BorderWidth { get; set; } = (double)Button.BorderWidthProperty.DefaultValue;
        public Color BorderColor { get; set; } = (Color)Button.BorderColorProperty.DefaultValue;
        public int CornerRadius { get; set; } = (int)Button.CornerRadiusProperty.DefaultValue;
        public ImageSource ImageSource { get; set; } = (ImageSource)Button.ImageSourceProperty.DefaultValue;
        public Thickness Padding { get; set; } = (Thickness)Button.PaddingProperty.DefaultValue;

        public Action ClickAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.ContentLayout = ContentLayout;
            NativeControl.Text = Text;
            NativeControl.TextColor = TextColor;
            NativeControl.CharacterSpacing = CharacterSpacing;
            NativeControl.Font = Font;
            NativeControl.FontFamily = FontFamily;
            NativeControl.FontSize = FontSize;
            NativeControl.FontAttributes = FontAttributes;
            NativeControl.BorderWidth = BorderWidth;
            NativeControl.BorderColor = BorderColor;
            NativeControl.CornerRadius = CornerRadius;
            NativeControl.ImageSource = ImageSource;
            NativeControl.Padding = Padding;

            if (ClickAction != null)
                NativeControl.Clicked += NativeControl_Clicked;

            base.OnUpdate();
        }

        private void NativeControl_Clicked(object sender, EventArgs e)
        {
            ClickAction?.Invoke();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.Clicked -= NativeControl_Clicked;

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
                NativeControl.Clicked -= NativeControl_Clicked;
            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxButton : RxButton<Button>, IRxButton
    {
        public RxButton()
        {
        }

        public RxButton(string text)
        {
            Text = text;
        }

        public RxButton(Action<Button> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxButtonViewExtensions
    {
        public static T OnClick<T>(this T button, Action clickAction) where T : IRxButton
        {
            button.ClickAction = clickAction;
            return button;
        }

        public static T ContentLayout<T>(this T button, ImagePosition position, double spacing) where T : IRxButton
        {
            button.ContentLayout = new ButtonContentLayout(position, spacing);
            return button;
        }

        public static T Text<T>(this T button, string text) where T : IRxButton
        {
            button.Text = text;
            return button;
        }

        public static T TextColor<T>(this T button, Color textColor) where T : IRxButton
        {
            button.TextColor = textColor;
            return button;
        }

        public static T CharacterSpacing<T>(this T button, double characterSpacing) where T : IRxButton
        {
            button.CharacterSpacing = characterSpacing;
            return button;
        }

        public static T Font<T>(this T button, Font font) where T : IRxButton
        {
            button.Font = font;
            return button;
        }

        public static T FontFamily<T>(this T button, string fontFamily) where T : IRxButton
        {
            button.FontFamily = fontFamily;
            return button;
        }

        public static T FontSize<T>(this T button, double fontSize) where T : IRxButton
        {
            button.FontSize = fontSize;
            return button;
        }

        public static T FontSize<T>(this T button, NamedSize size) where T : IRxButton
        {
            button.FontSize = Device.GetNamedSize(size, typeof(Button));
            return button;
        }

        public static T FontAttributes<T>(this T button, FontAttributes fontAttributes) where T : IRxButton
        {
            button.FontAttributes = fontAttributes;
            return button;
        }

        public static T BorderWidth<T>(this T button, double borderWidth) where T : IRxButton
        {
            button.BorderWidth = borderWidth;
            return button;
        }

        public static T BorderColor<T>(this T button, Color borderColor) where T : IRxButton
        {
            button.BorderColor = borderColor;
            return button;
        }

        public static T CornerRadius<T>(this T button, int cornerRadius) where T : IRxButton
        {
            button.CornerRadius = cornerRadius;
            return button;
        }

        public static T ImageSource<T>(this T button, ImageSource imageSource) where T : IRxButton
        {
            button.ImageSource = imageSource;
            return button;
        }

        public static T Padding<T>(this T button, Thickness padding) where T : IRxButton
        {
            button.Padding = padding;
            return button;
        }

        public static T Padding<T>(this T button, double leftRight, double topBottom) where T : IRxButton
        {
            button.Padding = new Thickness(leftRight, topBottom);
            return button;
        }

        public static T Padding<T>(this T button, double left, double top, double right, double bottom) where T : IRxButton
        {
            button.Padding = new Thickness(left, top, right, bottom);
            return button;
        }

        public static T Padding<T>(this T button, double uniformSize) where T : IRxButton
        {
            button.Padding = new Thickness(uniformSize);
            return button;
        }
    }
}