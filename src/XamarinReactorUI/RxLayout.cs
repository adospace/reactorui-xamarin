using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinReactorUI
{
    public abstract class RxLayout<T> : RxView<T>, IEnumerable<VisualNode> where T : Xamarin.Forms.Layout, new()
    {
        protected RxLayout(params VisualNode[] children)
        {
            _internalChildren.AddRange(children);
        }

        private readonly List<VisualNode> _internalChildren = new List<VisualNode>();

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren.Where(_ => _ != null);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(VisualNode node)
        {
            _internalChildren.Add(node);
        }


    }
}
