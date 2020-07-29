using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using XamarinReactorUI.Animations;

namespace XamarinReactorUI
{
    public interface IRxEllipse : IRxShape
    {
    }

    public class RxEllipse : RxShape<Ellipse>, IRxEllipse
    {
        public RxEllipse()
        {
        }

        public RxEllipse(Action<Ellipse> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxEllipseExtensions
    {
    }
}