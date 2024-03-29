﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxEntry : IRxInputView
    {
        ReturnType ReturnType { get; set; }
        //string Placeholder { get; set; }
        //Color PlaceholderColor { get; set; }
        bool IsPassword { get; set; }
        //string Text { get; set; }
        //Color TextColor { get; set; }
        //double CharacterSpacing { get; set; }
        TextAlignment HorizontalTextAlignment { get; set; }
        TextAlignment VerticalTextAlignment { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        FontAttributes FontAttributes { get; set; }
        bool IsTextPredictionEnabled { get; set; }
        int CursorPosition { get; set; }
        int SelectionLength { get; set; }
        ClearButtonVisibility ClearButtonVisibility { get; set; }
        //Keyboard Keyboard { get; set; }
        Action<string> CompletedAction { get; set; }

    }

    public class RxEntry<T> : RxInputView<T>, IRxEntry where T : Entry, new()
    {
        public RxEntry()
        {
        }

        public RxEntry(string text)
            : base(text)
        {

        }

        public RxEntry(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public ReturnType ReturnType { get; set; } = (ReturnType)Entry.ReturnTypeProperty.DefaultValue;
        public bool IsPassword { get; set; } = (bool)Entry.IsPasswordProperty.DefaultValue;
        public TextAlignment HorizontalTextAlignment { get; set; } = (TextAlignment)Entry.HorizontalTextAlignmentProperty.DefaultValue;
        public TextAlignment VerticalTextAlignment { get; set; } = (TextAlignment)Entry.VerticalTextAlignmentProperty.DefaultValue;
        public string FontFamily { get; set; } = (string)Entry.FontFamilyProperty.DefaultValue;
        public double FontSize { get; set; } = (double)Entry.FontSizeProperty.DefaultValue;
        public FontAttributes FontAttributes { get; set; } = (FontAttributes)Entry.FontAttributesProperty.DefaultValue;
        public bool IsTextPredictionEnabled { get; set; } = (bool)Entry.IsTextPredictionEnabledProperty.DefaultValue;
        public int CursorPosition { get; set; } = (int)Entry.CursorPositionProperty.DefaultValue;
        public int SelectionLength { get; set; } = (int)Entry.SelectionLengthProperty.DefaultValue;
        public ClearButtonVisibility ClearButtonVisibility { get; set; } = (ClearButtonVisibility)Entry.ClearButtonVisibilityProperty.DefaultValue;
        public Action<string> CompletedAction { get; set; }

        protected override void OnUpdate()
        {
            //NativeControl.ReturnType = ReturnType;
            //NativeControl.Placeholder = Placeholder;
            //NativeControl.PlaceholderColor = PlaceholderColor;
            //NativeControl.IsPassword = IsPassword;
            //NativeControl.HorizontalTextAlignment = HorizontalTextAlignment;
            //NativeControl.VerticalTextAlignment = VerticalTextAlignment;
            //NativeControl.FontFamily = FontFamily;
            //NativeControl.FontSize = FontSize;
            //NativeControl.FontAttributes = FontAttributes;
            //NativeControl.IsTextPredictionEnabled = IsTextPredictionEnabled;
            //NativeControl.CursorPosition = CursorPosition;
            //NativeControl.SelectionLength = SelectionLength;
            //NativeControl.ClearButtonVisibility = ClearButtonVisibility;

            if (NativeControl.ReturnType != ReturnType) NativeControl.ReturnType = ReturnType;
            if (NativeControl.Placeholder != Placeholder) NativeControl.Placeholder = Placeholder;
            if (NativeControl.PlaceholderColor != PlaceholderColor) NativeControl.PlaceholderColor = PlaceholderColor;
            if (NativeControl.IsPassword != IsPassword) NativeControl.IsPassword = IsPassword;
            if (NativeControl.HorizontalTextAlignment != HorizontalTextAlignment) NativeControl.HorizontalTextAlignment = HorizontalTextAlignment;
            if (NativeControl.VerticalTextAlignment != VerticalTextAlignment) NativeControl.VerticalTextAlignment = VerticalTextAlignment;
            if (NativeControl.FontFamily != FontFamily) NativeControl.FontFamily = FontFamily;
            if (NativeControl.FontSize != FontSize) NativeControl.FontSize = FontSize;
            if (NativeControl.FontAttributes != FontAttributes) NativeControl.FontAttributes = FontAttributes;
            if (NativeControl.IsTextPredictionEnabled != IsTextPredictionEnabled) NativeControl.IsTextPredictionEnabled = IsTextPredictionEnabled;
            if (NativeControl.CursorPosition != CursorPosition) NativeControl.CursorPosition = CursorPosition;
            if (NativeControl.SelectionLength != SelectionLength) NativeControl.SelectionLength = SelectionLength;
            if (NativeControl.ClearButtonVisibility != ClearButtonVisibility) NativeControl.ClearButtonVisibility = ClearButtonVisibility;


            if (CompletedAction != null)
                NativeControl.Completed += NativeControl_Completed;

            base.OnUpdate();
        }

        private void NativeControl_Completed(object sender, EventArgs e)
        {
            CompletedAction?.Invoke(NativeControl.Text);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
            {
                NativeControl.Completed -= NativeControl_Completed;
            }

            base.OnUnmount();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.Completed -= NativeControl_Completed;
            }

            var newEntry = (RxEntry)newNode;
            if (newEntry != null && NativeControl != null)
            {
                newEntry.CursorPosition = NativeControl.CursorPosition;
                newEntry.SelectionLength = NativeControl.SelectionLength;
            }

            base.OnMigrated(newNode);
        }
    }

    public class RxEntry : RxEntry<Entry>
    {
        public RxEntry()
        {
        }

        public RxEntry(string text)
            : base(text)
        {

        }

        public RxEntry(Action<Entry> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static class RxEntryExtensions
    {
        public static T OnCompleted<T>(this T entry, Action<string> action) where T : IRxEntry
        {
            entry.CompletedAction = action;
            return entry;
        }

        public static T ReturnType<T>(this T entry, ReturnType returnType) where T : IRxEntry
        {
            entry.ReturnType = returnType;
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