using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI
{
    public class RxLabel : RxView<Xamarin.Forms.Label>
    {
        public RxLabel()
        { 
        
        }
        
        public RxLabel(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.Text = Text;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }
}
