using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public abstract class RxComponent : VisualNode
    {
        public abstract VisualNode Render();

        protected sealed override void OnAddChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {
            Parent.AddChild(widget, nativeControl);
        }

        protected sealed override void OnRemoveChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {
            Parent.RemoveChild(widget, nativeControl);
        }

        protected sealed override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Render();
        }
    }
}
