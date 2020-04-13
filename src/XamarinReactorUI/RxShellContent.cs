using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxShellContent
    {
    }

    public class RxShellContent<T> : RxBaseShellItem<T>, IRxShellContent, IEnumerable<VisualNode> where T : ShellContent, new()
    {
        private readonly List<VisualNode> _contents = new List<VisualNode>();

        public RxShellContent()
        {
        }

        public RxShellContent(VisualNode content)
        {
            _contents.Add(content);
        }

        public RxShellContent(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public void Add(VisualNode node)
        {
            _contents.Clear();
            _contents.Add(node);
        }

        protected override void OnAddChild(VisualNode widget, Element childNativeControl)
        {
            NativeControl.Content = childNativeControl;

            base.OnAddChild(widget, childNativeControl);
        }

        protected override void OnRemoveChild(VisualNode widget, Element childNativeControl)
        {
            NativeControl.Content = null;

            base.OnRemoveChild(widget, childNativeControl);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _contents;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class RxShellContent : RxShellContent<ShellContent>
    {
        public RxShellContent()
        {
        }

        public RxShellContent(VisualNode content)
            : base(content)
        {
        }

        public RxShellContent(Action<ShellContent> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxShellContentExtensions
    {
    }
}