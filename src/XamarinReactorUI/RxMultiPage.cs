using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxMultiPage : IRxPage
    { }

    public abstract class RxMultiPage<T, TPAGE> : RxPage<T>, IRxMultiPage, IEnumerable<VisualNode> where T : Xamarin.Forms.MultiPage<TPAGE>, new() where TPAGE : Xamarin.Forms.Page
    {
        private readonly List<VisualNode> _internalChildren = new List<VisualNode>();

        protected RxMultiPage()
        {

        }

        protected RxMultiPage(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is TPAGE page)
                NativeControl.Children.Insert(widget.ChildIndex, page);
            else if (childControl is ToolbarItem toolbarItem)
                NativeControl.ToolbarItems.Add(toolbarItem);
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is TPAGE page)
                NativeControl.Children.Remove(page);
            else if (childControl is ToolbarItem toolbarItem)
                NativeControl.ToolbarItems.Remove(toolbarItem);

            base.OnRemoveChild(widget, childControl);
        }

        public void Add(VisualNode page)
        {
            _internalChildren.Add(page);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
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
