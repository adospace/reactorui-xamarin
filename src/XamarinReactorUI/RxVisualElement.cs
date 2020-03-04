using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxVisualElement
    {
        bool IsEnabled { get; set; }
        bool IsVisible { get; set; }
        Color BackgroundColor { get; set; }
    }

    public abstract class RxVisualElement<T> : RxElement, IRxVisualElement where T : Xamarin.Forms.VisualElement, new()
    {
        protected RxVisualElement()
        {
        }

        protected T NativeControl { get => (T)_nativeControl; }

        private readonly NullableField<bool> _isEnabled = new NullableField<bool>();
        public bool IsEnabled { get => _isEnabled.GetValueOrDefault(); set => _isEnabled.Value = value; }

        private readonly NullableField<bool> _isVisible = new NullableField<bool>();
        public bool IsVisible { get => _isVisible.GetValueOrDefault(); set => _isVisible.Value = value; }

        private readonly NullableField<Color> _backgroundColor = new NullableField<Color>();
        public Color BackgroundColor { get => _backgroundColor.GetValueOrDefault(); set => _backgroundColor.Value = value; }

        protected override void OnUpdate()
        {
            if (_isEnabled.HasValue) NativeControl.IsEnabled = _isEnabled.Value;
            if (_isVisible.HasValue) NativeControl.IsVisible = _isVisible.Value;
            if (_backgroundColor.HasValue) NativeControl.BackgroundColor = _backgroundColor.Value;
            base.OnUpdate();
        }
    }

    public static class RxVisualElementExtensions
    {
        public static T IsEnabled<T>(this T element, bool enabled) where T : IRxVisualElement
        {
            element.IsEnabled = enabled;
            return element;
        }

        public static T IsVisible<T>(this T element, bool visible) where T : IRxVisualElement
        {
            element.IsVisible = visible;
            return element;
        }

        public static T BackgroundColor<T>(this T element, Color color) where T : IRxVisualElement
        {
            element.BackgroundColor = color;
            return element;
        }
    }
}
