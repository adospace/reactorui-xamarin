using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxShellSection : IRxShellGroupItem
    {
    }

    public abstract class RxShellSection<T> : RxShellGroupItem<T>, IRxShellSection, IEnumerable<VisualNode> where T : Xamarin.Forms.ShellSection, new()
    {
        private readonly List<VisualNode> _items = new List<VisualNode>();
        private readonly Dictionary<Element, ShellContent> _elementItemMap = new Dictionary<Element, ShellContent>();

        public RxShellSection()
        {
        }

        public RxShellSection(string title)
            : base(title)
        {
        }

        public RxShellSection(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public void Add(VisualNode visualNode)
        {
            _items.Add(visualNode);
        }

        protected override void OnAddChild(VisualNode widget, Element childControl)
        {
            if (childControl is ShellContent item)
                NativeControl.Items.Insert(widget.ChildIndex, item);
            else if (childControl is Page page)
                NativeControl.Items.Insert(widget.ChildIndex, new ShellContent() { Content = page });
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            _elementItemMap[childControl] = NativeControl.Items[NativeControl.Items.Count - 1];

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, Element childControl)
        {
            NativeControl.Items.Remove(_elementItemMap[childControl]);

            base.OnRemoveChild(widget, childControl);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _items;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class RxShellSectionExtensions
    {
    }
}