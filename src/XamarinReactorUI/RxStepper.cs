using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxStepper
    {
        double Maximum { get; set; }
        double Minimum { get; set; }
        double Value { get; set; }
        double Increment { get; set; }
        Action<ValueChangedEventArgs> ValueChangedAction { get; set; }
    }

    public class RxStepper<T> : RxView<T>, IRxStepper where T : Stepper, new()
    {
        public RxStepper()
        {
        }

        public RxStepper(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public double Maximum { get; set; } = (double)Stepper.MaximumProperty.DefaultValue;
        public double Minimum { get; set; } = (double)Stepper.MinimumProperty.DefaultValue;
        public double Value { get; set; } = (double)Stepper.ValueProperty.DefaultValue;
        public double Increment { get; set; } = (double)Stepper.IncrementProperty.DefaultValue;
        public Action<ValueChangedEventArgs> ValueChangedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.Maximum = Maximum;
            NativeControl.Minimum = Minimum;
            NativeControl.Value = Value;
            NativeControl.Increment = Increment;

            if (ValueChangedAction != null)
            {
                NativeControl.ValueChanged += NativeControl_ValueChanged;
            }

            base.OnUpdate();
        }

        private void NativeControl_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueChangedAction?.Invoke(e);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.ValueChanged -= NativeControl_ValueChanged;
            }

            base.OnMigrated(newNode);
        }

        protected override void OnUnmount()
        {
            if (NativeControl != null)
            {
                NativeControl.ValueChanged -= NativeControl_ValueChanged;
            }

            base.OnUnmount();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxStepper : RxStepper<Stepper>
    {
        public RxStepper()
        {
        }

        public RxStepper(Action<Stepper> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxStepperExtensions
    {
        public static T OnValueChanged<T>(this T stepper, Action<ValueChangedEventArgs> action) where T : IRxStepper
        {
            stepper.ValueChangedAction = action;
            return stepper;
        }

        public static T Maximum<T>(this T stepper, double maximum) where T : IRxStepper
        {
            stepper.Maximum = maximum;
            return stepper;
        }

        public static T Minimum<T>(this T stepper, double minimum) where T : IRxStepper
        {
            stepper.Minimum = minimum;
            return stepper;
        }

        public static T Value<T>(this T stepper, double value) where T : IRxStepper
        {
            stepper.Value = value;
            return stepper;
        }

        public static T Increment<T>(this T stepper, double increment) where T : IRxStepper
        {
            stepper.Increment = increment;
            return stepper;
        }
    }
}