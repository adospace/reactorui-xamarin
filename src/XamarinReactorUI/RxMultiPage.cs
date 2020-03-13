using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public abstract class RxMultiPage<T, TPAGE> : RxPage<T>, IEnumerable<IRxPage> where T : Xamarin.Forms.MultiPage<TPAGE>, new() where TPAGE : Xamarin.Forms.Page
    {
        private readonly List<VisualNode> _internalChildren = new List<VisualNode>();

        protected RxMultiPage()
        {

        }

        protected RxMultiPage(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }


        protected override void OnAddChild(RxElement widget, Element nativeControl)
        {
            if (nativeControl is TPAGE page)
                NativeControl.Children.Add(page);
            else
            {
                throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, nativeControl);
        }

        protected override void OnRemoveChild(RxElement widget, Element nativeControl)
        {
            NativeControl.Children.Remove((TPAGE)nativeControl);

            base.OnRemoveChild(widget, nativeControl);
        }

        public void Add(IRxPage page)
        {
            _internalChildren.Add((VisualNode)page);
        }

        public IEnumerator<IRxPage> GetEnumerator()
        {
            return _internalChildren.Cast<IRxPage>().GetEnumerator();
        }

        protected override void OnUpdate()
        {

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
