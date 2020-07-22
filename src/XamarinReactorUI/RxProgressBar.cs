
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxProgressBar
    {
        Color ProgressColor { get; set; }
        double Progress { get; set; }
    }

    public class RxProgressBar<T> : RxView<T>, IRxProgressBar where T : Xamarin.Forms.ProgressBar, new()
    {
        public RxProgressBar()
        {

        }

        public RxProgressBar(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public Color ProgressColor { get; set; } = (Color)ProgressBar.ProgressColorProperty.DefaultValue;
        public double Progress { get; set; } = (double)ProgressBar.ProgressProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.ProgressColor = ProgressColor;
            NativeControl.Progress = Progress;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxProgressBar : RxProgressBar<Xamarin.Forms.ProgressBar>
    {
        public RxProgressBar()
        {

        }

        public RxProgressBar(Action<ProgressBar> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static class RxProgressBarExtensions
    {
        public static T ProgressColor<T>(this T progressbar, Color progressColor) where T : IRxProgressBar
        {
            progressbar.ProgressColor = progressColor;
            return progressbar;
        }



        public static T Progress<T>(this T progressbar, double progress) where T : IRxProgressBar
        {
            progressbar.Progress = progress;
            return progressbar;
        }



    }

}
