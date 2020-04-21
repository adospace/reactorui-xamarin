using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxBaseMenuItem : IRxElement
    {
    }

    public abstract class RxBaseMenuItem<T> : RxElement<T>, IRxBaseMenuItem where T : BaseMenuItem, new()
    {
        public RxBaseMenuItem()
        {
        }

        public RxBaseMenuItem(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxBaseMenuItemExtensions
    {
    }
}