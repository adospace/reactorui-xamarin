using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.Animations
{
    public abstract class RxPointAnimation : RxTweenAnimation
    {
        public RxPointAnimation(Easing easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract Point CurrentValue();
    }
}
