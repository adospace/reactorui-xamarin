using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxInputView
    {
        string Text { get; set; }
        Keyboard Keyboard { get; set; }
        bool IsSpellCheckEnabled { get; set; }
        int MaxLength { get; set; }
        bool IsReadOnly { get; set; }
        string Placeholder { get; set; }
        Color PlaceholderColor { get; set; }
        Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        Action<TextChangedEventArgs> TextChangedAction { get; set; }
        Action<string> AfterTextChangedAction { get; set; }
    }

    public abstract class RxInputView<T> : RxView<T>, IRxInputView where T : InputView, new()
    {
        protected RxInputView()
        {
        }

        protected RxInputView(string text)
        {
            Text = text;
        }

        protected RxInputView(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public string Text { get; set; } = (string)InputView.TextProperty.DefaultValue;
        public Keyboard Keyboard { get; set; } = (Keyboard)InputView.KeyboardProperty.DefaultValue;
        public bool IsSpellCheckEnabled { get; set; } = (bool)InputView.IsSpellCheckEnabledProperty.DefaultValue;
        public int MaxLength { get; set; } = (int)InputView.MaxLengthProperty.DefaultValue;
        public bool IsReadOnly { get; set; } = (bool)InputView.IsReadOnlyProperty.DefaultValue;
        public string Placeholder { get; set; } = (string)InputView.PlaceholderProperty.DefaultValue;
        public Color PlaceholderColor { get; set; } = (Color)InputView.PlaceholderColorProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)InputView.TextColorProperty.DefaultValue;
        public double CharacterSpacing { get; set; } = (double)InputView.CharacterSpacingProperty.DefaultValue;
        public Action<TextChangedEventArgs> TextChangedAction { get; set; }
        public Action<string> AfterTextChangedAction { get; set; }

        private bool IsFocused { get; set; }

        protected override void OnUpdate()
        {
            if (NativeControl.Text != Text)
                NativeControl.Text = Text;
            NativeControl.Keyboard = Keyboard;
            NativeControl.IsSpellCheckEnabled = IsSpellCheckEnabled;
            NativeControl.MaxLength = MaxLength;
            NativeControl.IsReadOnly = IsReadOnly;
            NativeControl.Placeholder = Placeholder;
            NativeControl.PlaceholderColor = PlaceholderColor;
            NativeControl.TextColor = TextColor;
            NativeControl.CharacterSpacing = CharacterSpacing;

            if (IsFocused)
                NativeControl.Focus();

            if (TextChangedAction != null)
                NativeControl.TextChanged += NativeControl_TextChanged;
            if (AfterTextChangedAction != null)
                NativeControl.Unfocused += NativeControl_Unfocused;
            
            base.OnUpdate();
        }

        private void NativeControl_Unfocused(object sender, FocusEventArgs e)
        {
            if (NativeControl.Text != Text)
                AfterTextChangedAction?.Invoke(NativeControl.Text);
        }

        private void NativeControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChangedAction?.Invoke(e);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.TextChanged -= NativeControl_TextChanged;
                NativeControl.Unfocused -= NativeControl_Unfocused;
            }


            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            NativeControl.TextChanged -= NativeControl_TextChanged;
            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxInputViewExtensions
    {
        public static T OnAfterTextChanged<T>(this T entry, Action<string> action) where T : IRxInputView
        {
            entry.AfterTextChangedAction = action;
            return entry;
        }

        public static T OnTextChanged<T>(this T entry, Action<TextChangedEventArgs> textChangedAction) where T : IRxInputView
        {
            entry.TextChangedAction = textChangedAction;
            return entry;
        }

        public static T Text<T>(this T inputview, string text) where T : IRxInputView
        {
            inputview.Text = text;
            return inputview;
        }

        public static T Keyboard<T>(this T inputview, Keyboard keyboard) where T : IRxInputView
        {
            inputview.Keyboard = keyboard;
            return inputview;
        }

        public static T IsSpellCheckEnabled<T>(this T inputview, bool isSpellCheckEnabled) where T : IRxInputView
        {
            inputview.IsSpellCheckEnabled = isSpellCheckEnabled;
            return inputview;
        }

        public static T MaxLength<T>(this T inputview, int maxLength) where T : IRxInputView
        {
            inputview.MaxLength = maxLength;
            return inputview;
        }

        public static T IsReadOnly<T>(this T inputview, bool isReadOnly) where T : IRxInputView
        {
            inputview.IsReadOnly = isReadOnly;
            return inputview;
        }

        public static T Placeholder<T>(this T inputview, string placeholder) where T : IRxInputView
        {
            inputview.Placeholder = placeholder;
            return inputview;
        }

        public static T PlaceholderColor<T>(this T inputview, Color placeholderColor) where T : IRxInputView
        {
            inputview.PlaceholderColor = placeholderColor;
            return inputview;
        }

        public static T TextColor<T>(this T inputview, Color textColor) where T : IRxInputView
        {
            inputview.TextColor = textColor;
            return inputview;
        }

        public static T CharacterSpacing<T>(this T inputview, double characterSpacing) where T : IRxInputView
        {
            inputview.CharacterSpacing = characterSpacing;
            return inputview;
        }

    }
}