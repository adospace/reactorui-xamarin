using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.Animations
{
    public enum ColorTransitionModel
    { 
        RGB,

        HSL,
    }

    public class RxSimpleColorAnimation : RxColorAnimation
    {
        public RxSimpleColorAnimation(Color targetColor, ColorTransitionModel colorTransitionModel = ColorTransitionModel.HSL, Easing easing = null, double? duration = null)
            : base(easing, duration)
        {
            TargetColor = targetColor;
            TransitionModel = colorTransitionModel;
        }

        public Color TargetColor { get; }
        public ColorTransitionModel TransitionModel { get; }
        public Color? StartColor { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted;

        public override Color CurrentValue()
        {
            if (StartColor == null)
                return TargetColor;

            var currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var elapsedTime = currentTime - StartTime;
            var duration = Duration ?? DefaultDuration;

            if (elapsedTime >= duration)
            {
                _isCompleted = true;
                return TargetColor;
            }

            var easing = Easing ?? Easing.Linear;

            var easingValue = easing.Ease(elapsedTime / duration);

            if (TransitionModel == ColorTransitionModel.RGB)
            {
                return Color.FromRgba(
                   StartColor.Value.R + (TargetColor.R - StartColor.Value.R) * easingValue,
                   StartColor.Value.G + (TargetColor.G - StartColor.Value.G) * easingValue,
                   StartColor.Value.B + (TargetColor.B - StartColor.Value.B) * easingValue,
                   StartColor.Value.A + (TargetColor.A - StartColor.Value.A) * easingValue
                   );
            }

            return Color.FromHsla(
                StartColor.Value.Hue + (TargetColor.Hue - StartColor.Value.Hue) * easingValue,
                StartColor.Value.Saturation + (TargetColor.Saturation - StartColor.Value.Saturation) * easingValue,
                StartColor.Value.Luminosity + (TargetColor.Luminosity - StartColor.Value.Luminosity) * easingValue,
                StartColor.Value.A + (TargetColor.A - StartColor.Value.A) * easingValue
                );
        }

        private double Completion()
        {
            if (StartColor == null)
                return 1.0;

            if (TransitionModel == ColorTransitionModel.RGB)
            {
                return Math.Pow(
                    Math.Pow((CurrentValue().R - StartColor.Value.R) / (TargetColor.R - StartColor.Value.R), 2.0) +
                    Math.Pow((CurrentValue().G - StartColor.Value.G) / (TargetColor.G - StartColor.Value.G), 2.0) +
                    Math.Pow((CurrentValue().B - StartColor.Value.B) / (TargetColor.B - StartColor.Value.B), 2.0) +
                    Math.Pow((CurrentValue().A - StartColor.Value.A) / (TargetColor.A - StartColor.Value.A), 2.0), 0.25);
            }

            return Math.Pow(
                Math.Pow((CurrentValue().Hue - StartColor.Value.Hue) / (TargetColor.Hue - StartColor.Value.Hue), 2.0) +
                Math.Pow((CurrentValue().Saturation - StartColor.Value.Saturation) / (TargetColor.Saturation - StartColor.Value.Saturation), 2.0) +
                Math.Pow((CurrentValue().Luminosity - StartColor.Value.Luminosity) / (TargetColor.Luminosity - StartColor.Value.Luminosity), 2.0) +
                Math.Pow((CurrentValue().A - StartColor.Value.A) / (TargetColor.A - StartColor.Value.A), 2.0), 0.25);

        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            //System.Diagnostics.Debug.Assert(previousAnimation != this);
            //System.Diagnostics.Debug.WriteLine($"Migrate StartValue from {StartValue} to {((RxDoubleAnimation)previousAnimation).TargetValue} (TargetValue={TargetValue})");
            var previousDoubleAnimation = ((RxSimpleColorAnimation)previousAnimation);
            StartColor = previousDoubleAnimation.TargetColor;

            if (!previousDoubleAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration - duration * previousDoubleAnimation.Completion());
            }

            base.OnMigrateFrom(previousAnimation);
        }

    }
}
