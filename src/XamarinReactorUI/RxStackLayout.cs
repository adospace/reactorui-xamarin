using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI
{
    public class RxStackLayout : RxLayout<Xamarin.Forms.StackLayout>, IEnumerable<VisualNode>
    {
        public RxStackLayout(params VisualNode[] children)
            : base(children)
        { }

        protected override void OnAddChild(RxElement widget, Xamarin.Forms.View child)
        {
            NativeControl.Children.Add(child);
            
            base.OnAddChild(widget, child);
        }

        protected override void OnRemoveChild(RxElement widget, Xamarin.Forms.View child)
        {
            NativeControl.Children.Remove(child);

            base.OnRemoveChild(widget, child);
        }
    }
}
