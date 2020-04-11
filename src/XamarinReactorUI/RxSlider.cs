using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxSlider
    {
        double Minimum { get; set; }
        double Maximum { get; set; }
        double Value { get; set; }
        Color MinimumTrackColor { get; set; }
        Color MaximumTrackColor { get; set; }
        Color ThumbColor { get; set; }
        ImageSource ThumbImageSource { get; set; }
        Action<ValueChangedEventArgs> ValueChangedAction { get; set; }
        Action DragStartedAction { get; set; }
        Action DragCompletedAction { get; set; }
    }

    public class RxSlider : RxView<Slider>, IRxSlider
    {
        public RxSlider()
        {
        }

        public RxSlider(Action<Slider> componentRefAction)
            : base(componentRefAction)
        {
        }

        public double Minimum { get; set; } = (double)Slider.MinimumProperty.DefaultValue;
        public double Maximum { get; set; } = (double)Slider.MaximumProperty.DefaultValue;
        public double Value { get; set; } = (double)Slider.ValueProperty.DefaultValue;
        public Color MinimumTrackColor { get; set; } = (Color)Slider.MinimumTrackColorProperty.DefaultValue;
        public Color MaximumTrackColor { get; set; } = (Color)Slider.MaximumTrackColorProperty.DefaultValue;
        public Color ThumbColor { get; set; } = (Color)Slider.ThumbColorProperty.DefaultValue;
        public ImageSource ThumbImageSource { get; set; } = (ImageSource)Slider.ThumbImageSourceProperty.DefaultValue;
        public Action DragStartedAction { get; set; }
        public Action DragCompletedAction { get; set; }
        public Action<ValueChangedEventArgs> ValueChangedAction { get; set; }

        protected override void OnUpdate()
        {
            //WARN: validation rules apply!!
            //https://forums.xamarin.com/discussion/19131/invalid-value-for-slider-minimum
            //TODO: if Maximum < NativeControl.Minimum or if Minimum > NativeControl.Maximum -> Exception!!
            NativeControl.Maximum = Maximum;
            NativeControl.Minimum = Minimum;

            NativeControl.Value = Value;
            NativeControl.MinimumTrackColor = MinimumTrackColor;
            NativeControl.MaximumTrackColor = MaximumTrackColor;
            NativeControl.ThumbColor = ThumbColor;
            NativeControl.ThumbImageSource = ThumbImageSource;
            if (DragStartedAction != null)
                NativeControl.DragStarted += NativeControl_DragStarted;
            if (DragCompletedAction != null)
                NativeControl.DragCompleted += NativeControl_DragCompleted;
            if (ValueChangedAction != null)
                NativeControl.ValueChanged += NativeControl_ValueChanged;

            base.OnUpdate();
        }

        private void NativeControl_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueChangedAction?.Invoke(e);
        }

        private void NativeControl_DragStarted(object sender, EventArgs e)
        {
            DragStartedAction?.Invoke();
        }

        private void NativeControl_DragCompleted(object sender, EventArgs e)
        {
            DragCompletedAction?.Invoke();
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
                NativeControl.DragStarted -= NativeControl_DragStarted;
            if (NativeControl != null)
                NativeControl.DragCompleted -= NativeControl_DragCompleted;
            if (NativeControl != null)
                NativeControl.ValueChanged -= NativeControl_ValueChanged;

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            NativeControl.DragStarted -= NativeControl_DragStarted;
            NativeControl.DragCompleted -= NativeControl_DragCompleted;
            NativeControl.ValueChanged -= NativeControl_ValueChanged;

            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxSliderExtensions
    {
        public static T OnValueChanged<T>(this T slider, Action<ValueChangedEventArgs> action) where T : IRxSlider
        {
            slider.ValueChangedAction = action;
            return slider;
        }

        public static T OnDragStarted<T>(this T slider, Action action) where T : IRxSlider
        {
            slider.DragStartedAction = action;
            return slider;
        }

        public static T OnDragCompleted<T>(this T slider, Action action) where T : IRxSlider
        {
            slider.DragCompletedAction = action;
            return slider;
        }

        public static T Minimum<T>(this T slider, double minimum) where T : IRxSlider
        {
            slider.Minimum = minimum;
            return slider;
        }

        public static T Maximum<T>(this T slider, double maximum) where T : IRxSlider
        {
            slider.Maximum = maximum;
            return slider;
        }

        public static T Value<T>(this T slider, double value) where T : IRxSlider
        {
            slider.Value = value;
            return slider;
        }

        public static T MinimumTrackColor<T>(this T slider, Color minimumTrackColor) where T : IRxSlider
        {
            slider.MinimumTrackColor = minimumTrackColor;
            return slider;
        }

        public static T MaximumTrackColor<T>(this T slider, Color maximumTrackColor) where T : IRxSlider
        {
            slider.MaximumTrackColor = maximumTrackColor;
            return slider;
        }

        public static T ThumbColor<T>(this T slider, Color thumbColor) where T : IRxSlider
        {
            slider.ThumbColor = thumbColor;
            return slider;
        }

        public static T ThumbImageSource<T>(this T slider, ImageSource thumbImageSource) where T : IRxSlider
        {
            slider.ThumbImageSource = thumbImageSource;
            return slider;
        }

        public static T ThumbImage<T>(this T slider, string file) where T : IRxSlider
        {
            slider.ThumbImageSource = ImageSource.FromFile(file);
            return slider;
        }

        public static T ThumbImage<T>(this T slider, string fileAndroid, string fileiOS) where T : IRxSlider
        {
            slider.ThumbImageSource = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return slider;
        }

        public static T ThumbImage<T>(this T slider, string resourceName, Assembly sourceAssembly) where T : IRxSlider
        {
            slider.ThumbImageSource = ImageSource.FromResource(resourceName, sourceAssembly);
            return slider;
        }

        public static T ThumbImage<T>(this T slider, Uri imageUri) where T : IRxSlider
        {
            slider.ThumbImageSource = ImageSource.FromUri(imageUri);
            return slider;
        }

        public static T ThumbImage<T>(this T slider, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IRxSlider
        {
            slider.ThumbImageSource = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return slider;
        }

        public static T ThumbImage<T>(this T slider, Func<Stream> imageStream) where T : IRxSlider
        {
            slider.ThumbImageSource = ImageSource.FromStream(imageStream);
            return slider;
        }

        //public static T DragStartedCommand<T>(this T slider, ICommand dragStartedCommand) where T : IRxSlider
        //{
        //    slider.DragStartedCommand = dragStartedCommand;
        //    return slider;
        //}

        //public static T DragCompletedCommand<T>(this T slider, ICommand dragCompletedCommand) where T : IRxSlider
        //{
        //    slider.DragCompletedCommand = dragCompletedCommand;
        //    return slider;
        //}
    }
}