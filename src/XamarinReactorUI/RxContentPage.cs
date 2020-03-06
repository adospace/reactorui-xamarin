
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxContentPage
    {
    }

    public class RxContentPage : RxPage<Xamarin.Forms.ContentPage>, IRxContentPage, IEnumerable<VisualNode>
    {
        private List<VisualNode> _contents = new List<VisualNode>();
        private readonly ContentPage _contentPage;

        public RxContentPage()
        {

        }

        public RxContentPage(ContentPage contentPage)
        {
            _contentPage = contentPage;
        }

        protected override void OnMount()
        {
            _nativeControl = _contentPage;
            base.OnMount();
        }

        public void Add(VisualNode child)
        {
            _contents = new List<VisualNode>(new[] { child });
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        protected override void OnAddChild(RxElement widget, View nativeControl)
        {
            NativeControl.Content = nativeControl;

            base.OnAddChild(widget, nativeControl);
        }

        protected override void OnRemoveChild(RxElement widget, View nativeControl)
        {
            NativeControl.Content = null;

            base.OnRemoveChild(widget, nativeControl);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            if (_contents.Count > 0)
                yield return _contents[0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class RxContentPageExtensions
    {

    }

}