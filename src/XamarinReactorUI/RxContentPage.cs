
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxContentPage : IRxPage
    {
    }

    public class RxContentPage<T> : RxPage<T>, IRxContentPage, IEnumerable<VisualNode> where T : ContentPage, new()
    {
        private readonly List<VisualNode> _contents = new List<VisualNode>();

        public RxContentPage()
        {

        }

        public RxContentPage(VisualNode content)
        {
            _contents.Add(content);
        }

        public RxContentPage(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public void Add(VisualNode child)
        {
            if (child is IRxView && _contents.OfType<IRxView>().Any())
                throw new InvalidOperationException("Content already set");

            _contents.Add(child);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is View view)
                NativeControl.Content = view;
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
            if (childControl is View)
                NativeControl.Content = null;
            else if (childControl is ToolbarItem toolbarItem)
                NativeControl.ToolbarItems.Remove(toolbarItem);

            base.OnRemoveChild(widget, childControl);
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

    public class RxContentPage : RxContentPage<ContentPage>
    {
        public RxContentPage()
        {

        }

        public RxContentPage(VisualNode content)
            : base(content)
        {

        }

        public RxContentPage(Action<ContentPage> componentRefAction)
            : base(componentRefAction)
        {
        }
    }
    
    public static class RxContentPageExtensions
    {

    }

}