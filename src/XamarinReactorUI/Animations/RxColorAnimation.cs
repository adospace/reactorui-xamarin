using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.Animations
{
    public abstract class RxColorAnimation : RxTweenAnimation
    {
        public RxColorAnimation(Easing easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract Color CurrentValue();
    }
}
