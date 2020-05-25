﻿using System;
using Xamarin.Forms;

namespace XamarinReactorUI.Animations
{
    public class RxDoubleAnimation : RxTweenAnimation
    {
        public RxDoubleAnimation(double targetValue, Easing easing = null, double? duration = null) : base(easing, duration)
        {
            TargetValue = targetValue;
            //System.Diagnostics.Debug.WriteLine($"RxDoubleAnimation(TargetValue={TargetValue})");
        }

        public double TargetValue { get; }
        public double? StartValue { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted || StartValue == TargetValue;
        
        public double CurrentValue()
        {
            if (StartValue == null)
                return TargetValue;

            var currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var elapsedTime = currentTime - StartTime;
            var duration = Duration ?? DefaultDuration;

            System.Diagnostics.Debug.Assert(elapsedTime >= 0);

            if (elapsedTime >= duration)
            {
                _isCompleted = true;
                return TargetValue;
            }

            var easing = Easing ?? Easing.Linear;

            var easingValue = easing.Ease(elapsedTime / duration);

            var v = StartValue.Value + (TargetValue - StartValue.Value) * easingValue;
            //System.Diagnostics.Debug.WriteLine($"easingValue={easingValue} currentValue={v}");
            return v;
        }

        private double Completion()
        {
            if (StartValue == null)
                return 1.0;

            return (CurrentValue() - StartValue.Value) / (TargetValue - StartValue.Value);
        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            //System.Diagnostics.Debug.Assert(previousAnimation != this);
            //System.Diagnostics.Debug.WriteLine($"Migrate StartValue from {StartValue} to {((RxDoubleAnimation)previousAnimation).TargetValue} (TargetValue={TargetValue})");
            var previousDoubleAnimation = ((RxDoubleAnimation)previousAnimation);

            //StartValue = previousDoubleAnimation.CurrentValue();
            StartValue = previousDoubleAnimation.TargetValue;

            if (!previousDoubleAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration - duration * previousDoubleAnimation.Completion());
                //System.Diagnostics.Debug.WriteLine($"previousCompletion={previousDoubleAnimation.Completion()} -> completion={Completion()}");
                //if (StartTime < 0)
                    System.Diagnostics.Debug.Assert(StartTime > 0);
            }

            base.OnMigrateFrom(previousAnimation);
        }
    }
}