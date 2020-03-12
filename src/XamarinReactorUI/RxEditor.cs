using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxEditor
    {
        string Text { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        FontAttributes FontAttributes { get; set; }
        Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        string Placeholder { get; set; }
        Color PlaceholderColor { get; set; }
        bool IsTextPredictionEnabled { get; set; }
        EditorAutoSizeOption AutoSize { get; set; }
    }

    public class RxEditor : RxInputView<Xamarin.Forms.Editor>, IRxEditor
    {
        public RxEditor()
        {
        }

        public string FontFamily { get; set; } = (string)Editor.FontFamilyProperty.DefaultValue;
        public double FontSize { get; set; } = (double)Editor.FontSizeProperty.DefaultValue;
        public FontAttributes FontAttributes { get; set; } = (FontAttributes)Editor.FontAttributesProperty.DefaultValue;
        public bool IsTextPredictionEnabled { get; set; } = (bool)Editor.IsTextPredictionEnabledProperty.DefaultValue;
        public EditorAutoSizeOption AutoSize { get; set; } = (EditorAutoSizeOption)Editor.AutoSizeProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.FontFamily = FontFamily;
            NativeControl.FontSize = FontSize;
            NativeControl.FontAttributes = FontAttributes;
            NativeControl.IsTextPredictionEnabled = IsTextPredictionEnabled;
            NativeControl.AutoSize = AutoSize;

            base.OnUpdate();
        }
    }

    public static class RxEditorExtensions
    {
        public static T FontFamily<T>(this T editor, string fontFamily) where T : IRxEditor
        {
            editor.FontFamily = fontFamily;
            return editor;
        }

        public static T FontSize<T>(this T editor, double fontSize) where T : IRxEditor
        {
            editor.FontSize = fontSize;
            return editor;
        }

        public static T FontSize<T>(this T editor, NamedSize size) where T : IRxEditor
        {
            editor.FontSize = Device.GetNamedSize(size, typeof(Editor));
            return editor;
        }

        public static T FontAttributes<T>(this T editor, FontAttributes fontAttributes) where T : IRxEditor
        {
            editor.FontAttributes = fontAttributes;
            return editor;
        }

        public static T IsTextPredictionEnabled<T>(this T editor, bool isTextPredictionEnabled) where T : IRxEditor
        {
            editor.IsTextPredictionEnabled = isTextPredictionEnabled;
            return editor;
        }

        public static T AutoSize<T>(this T editor, EditorAutoSizeOption autoSize) where T : IRxEditor
        {
            editor.AutoSize = autoSize;
            return editor;
        }
    }
}