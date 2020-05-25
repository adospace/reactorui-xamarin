using System;
using Xamarin.Forms;

namespace XamarinReactorUI.Animations
{
    public abstract class RxAnimation
    {
        protected RxAnimation()
        {
        }

        public abstract bool IsCompleted();

        internal void MigrateFrom(RxAnimation previousAnimation)
        {
            OnMigrateFrom(previousAnimation);
        }

        protected virtual void OnMigrateFrom(RxAnimation previousAnimation)
        {
        }
    }
}