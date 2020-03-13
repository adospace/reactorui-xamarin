using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxActivityIndicator
    {
        bool IsRunning { get; set; }
        Color Color { get; set; }
    }

    public sealed class RxActivityIndicator : RxView<ActivityIndicator>, IRxActivityIndicator
    {

        public RxActivityIndicator(Action<ActivityIndicator> componentRefAction)
            : base(componentRefAction)
        {

        }


        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }

        private readonly NullableField<bool> _isRunning = new NullableField<bool>();
        public bool IsRunning { get => _isRunning.GetValueOrDefault(); set => _isRunning.Value = value; }

        private readonly NullableField<Color> _color = new NullableField<Color>();
        public Color Color { get => _color.GetValueOrDefault(); set => _color.Value = value; }

        protected override void OnUpdate()
        {
            if (_isRunning.HasValue) NativeControl.IsRunning = _isRunning.Value;
            if (_color.HasValue) NativeControl.Color = _color.Value;

            base.OnUpdate();
        }
    }


    public static class RxActivityIndicatorExtensions
    {
        public static T IsRunning<T>(this T activityIndicator, bool isRunning) where T : IRxActivityIndicator
        {
            activityIndicator.IsRunning = isRunning;
            return activityIndicator;
        }

        public static T Color<T>(this T activityIndicator, Color color) where T : IRxActivityIndicator
        {
            activityIndicator.Color = color;
            return activityIndicator;
        }
    }
}
