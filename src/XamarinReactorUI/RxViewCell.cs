using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxViewCell : IRxCell
    {
    }

    public class RxViewCell<T> : RxCell<T>, IRxViewCell, IEnumerable<VisualNode> where T : ViewCell, new()
    {
        private readonly List<VisualNode> _contents = new List<VisualNode>();

        public RxViewCell()
        {
        }

        public RxViewCell(VisualNode child)
        {
            _contents.Add(child);
        }

        public RxViewCell(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        public void Add(VisualNode node)
        {
            _contents.Add(node);
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
        {
            if (childNativeControl is View view)
            {
                NativeControl.View = view;
            }
            else
            {
                throw new InvalidOperationException($"Type '{childNativeControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childNativeControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
            NativeControl.View = null;

            base.OnRemoveChild(widget, childNativeControl);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _contents;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class RxViewCell : RxViewCell<Xamarin.Forms.ViewCell>
    {
        public RxViewCell()
        {
        }

        public RxViewCell(Action<ViewCell> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxViewCellExtensions
    {
    }
}