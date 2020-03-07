
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxEntry
    {
        ReturnType ReturnType { get; set; }
        //ICommand ReturnCommand { get; set; }
        //object ReturnCommandParameter { get; set; }
        string Placeholder { get; set; }
        Color PlaceholderColor { get; set; }
        bool IsPassword { get; set; }
        string Text { get; set; }
        Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        TextAlignment HorizontalTextAlignment { get; set; }
        TextAlignment VerticalTextAlignment { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        FontAttributes FontAttributes { get; set; }
        bool IsTextPredictionEnabled { get; set; }
        int CursorPosition { get; set; }
        int SelectionLength { get; set; }
        ClearButtonVisibility ClearButtonVisibility { get; set; }
    }

    public class RxEntry : RxView<Xamarin.Forms.Entry>, IRxEntry
    {
        public RxEntry()
        {

        }

        public ReturnType ReturnType { get; set; } = (ReturnType)Entry.ReturnTypeProperty.DefaultValue;
        //public ICommand ReturnCommand { get; set; } = (ICommand)Entry.ReturnCommandProperty.DefaultValue;
        //public object ReturnCommandParameter { get; set; } = (object)Entry.ReturnCommandParameterProperty.DefaultValue;
        public string Placeholder { get; set; } = (string)Entry.PlaceholderProperty.DefaultValue;
        public Color PlaceholderColor { get; set; } = (Color)Entry.PlaceholderColorProperty.DefaultValue;
        public bool IsPassword { get; set; } = (bool)Entry.IsPasswordProperty.DefaultValue;
        public string Text { get; set; } = (string)Entry.TextProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)Entry.TextColorProperty.DefaultValue;
        public double CharacterSpacing { get; set; } = (double)Entry.CharacterSpacingProperty.DefaultValue;
        public TextAlignment HorizontalTextAlignment { get; set; } = (TextAlignment)Entry.HorizontalTextAlignmentProperty.DefaultValue;
        public TextAlignment VerticalTextAlignment { get; set; } = (TextAlignment)Entry.VerticalTextAlignmentProperty.DefaultValue;
        public string FontFamily { get; set; } = (string)Entry.FontFamilyProperty.DefaultValue;
        public double FontSize { get; set; } = (double)Entry.FontSizeProperty.DefaultValue;
        public FontAttributes FontAttributes { get; set; } = (FontAttributes)Entry.FontAttributesProperty.DefaultValue;
        public bool IsTextPredictionEnabled { get; set; } = (bool)Entry.IsTextPredictionEnabledProperty.DefaultValue;
        public int CursorPosition { get; set; } = (int)Entry.CursorPositionProperty.DefaultValue;
        public int SelectionLength { get; set; } = (int)Entry.SelectionLengthProperty.DefaultValue;
        public ClearButtonVisibility ClearButtonVisibility { get; set; } = (ClearButtonVisibility)Entry.ClearButtonVisibilityProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.ReturnType = ReturnType;
            //NativeControl.ReturnCommand = ReturnCommand;
            //NativeControl.ReturnCommandParameter = ReturnCommandParameter;
            NativeControl.Placeholder = Placeholder;
            NativeControl.PlaceholderColor = PlaceholderColor;
            NativeControl.IsPassword = IsPassword;
            NativeControl.Text = Text;
            NativeControl.TextColor = TextColor;
            NativeControl.CharacterSpacing = CharacterSpacing;
            NativeControl.HorizontalTextAlignment = HorizontalTextAlignment;
            NativeControl.VerticalTextAlignment = VerticalTextAlignment;
            NativeControl.FontFamily = FontFamily;
            NativeControl.FontSize = FontSize;
            NativeControl.FontAttributes = FontAttributes;
            NativeControl.IsTextPredictionEnabled = IsTextPredictionEnabled;
            NativeControl.CursorPosition = CursorPosition;
            NativeControl.SelectionLength = SelectionLength;
            NativeControl.ClearButtonVisibility = ClearButtonVisibility;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxEntryExtensions
    {
        public static T ReturnType<T>(this T entry, ReturnType returnType) where T : IRxEntry
        {
            entry.ReturnType = returnType;
            return entry;
        }



        //public static T ReturnCommand<T>(this T entry, ICommand returnCommand) where T : IRxEntry
        //{
        //    entry.ReturnCommand = returnCommand;
        //    return entry;
        //}



        //public static T ReturnCommandParameter<T>(this T entry, object returnCommandParameter) where T : IRxEntry
        //{
        //    entry.ReturnCommandParameter = returnCommandParameter;
        //    return entry;
        //}



        public static T Placeholder<T>(this T entry, string placeholder) where T : IRxEntry
        {
            entry.Placeholder = placeholder;
            return entry;
        }



        public static T PlaceholderColor<T>(this T entry, Color placeholderColor) where T : IRxEntry
        {
            entry.PlaceholderColor = placeholderColor;
            return entry;
        }



        public static T IsPassword<T>(this T entry, bool isPassword) where T : IRxEntry
        {
            entry.IsPassword = isPassword;
            return entry;
        }



        public static T Text<T>(this T entry, string text) where T : IRxEntry
        {
            entry.Text = text;
            return entry;
        }



        public static T TextColor<T>(this T entry, Color textColor) where T : IRxEntry
        {
            entry.TextColor = textColor;
            return entry;
        }



        public static T CharacterSpacing<T>(this T entry, double characterSpacing) where T : IRxEntry
        {
            entry.CharacterSpacing = characterSpacing;
            return entry;
        }



        public static T HorizontalTextAlignment<T>(this T entry, TextAlignment horizontalTextAlignment) where T : IRxEntry
        {
            entry.HorizontalTextAlignment = horizontalTextAlignment;
            return entry;
        }



        public static T VerticalTextAlignment<T>(this T entry, TextAlignment verticalTextAlignment) where T : IRxEntry
        {
            entry.VerticalTextAlignment = verticalTextAlignment;
            return entry;
        }



        public static T FontFamily<T>(this T entry, string fontFamily) where T : IRxEntry
        {
            entry.FontFamily = fontFamily;
            return entry;
        }



        public static T FontSize<T>(this T entry, double fontSize) where T : IRxEntry
        {
            entry.FontSize = fontSize;
            return entry;
        }
        public static T FontSize<T>(this T entry, NamedSize size) where T : IRxEntry
        {
            entry.FontSize = Device.GetNamedSize(size, typeof(Entry));
            return entry;
        }



        public static T FontAttributes<T>(this T entry, FontAttributes fontAttributes) where T : IRxEntry
        {
            entry.FontAttributes = fontAttributes;
            return entry;
        }



        public static T IsTextPredictionEnabled<T>(this T entry, bool isTextPredictionEnabled) where T : IRxEntry
        {
            entry.IsTextPredictionEnabled = isTextPredictionEnabled;
            return entry;
        }



        public static T CursorPosition<T>(this T entry, int cursorPosition) where T : IRxEntry
        {
            entry.CursorPosition = cursorPosition;
            return entry;
        }



        public static T SelectionLength<T>(this T entry, int selectionLength) where T : IRxEntry
        {
            entry.SelectionLength = selectionLength;
            return entry;
        }



        public static T ClearButtonVisibility<T>(this T entry, ClearButtonVisibility clearButtonVisibility) where T : IRxEntry
        {
            entry.ClearButtonVisibility = clearButtonVisibility;
            return entry;
        }



    }

}
