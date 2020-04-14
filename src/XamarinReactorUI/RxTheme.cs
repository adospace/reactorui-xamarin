using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTheme
    {
        RxTheme StyleFor<TS>(Action<TS> action) where TS : VisualNode;
    }

    public class RxTheme : VisualNode, IEnumerable<VisualNode>, IRxTheme
    {
        private readonly List<VisualNode> _internalChildren = new List<VisualNode>();

        private readonly Dictionary<Type, Action<VisualNode>> _styles = new Dictionary<Type, Action<VisualNode>>();

        internal sealed override void Layout(RxTheme theme)
        {
            base.Layout(this);
        }

        internal void Style(VisualNode node)
        {
            if (node is RxTheme)
                return;

            if (_styles.TryGetValue(node.GetType(), out var styleAction))
                styleAction(node);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode[] nodes)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            foreach (var node in nodes)
                _internalChildren.Add(node);
        }

        protected sealed override void OnAddChild(VisualNode widget, Element nativeControl)
        {
            Parent.AddChild(this, nativeControl);
        }

        protected sealed override void OnRemoveChild(VisualNode widget, Element nativeControl)
        {
            Parent.RemoveChild(this, nativeControl);
        }

        public RxTheme StyleFor<TS>(Action<TS> action) where TS : VisualNode
        {
            _styles[typeof(TS)] = node => action((TS)node);
            return this;
        }
    }

    public static class RxThemeExtensions
    {
        public static T StyleFor<T, TS>(this T theme, Action<TS> action) where T : IRxTheme where TS : VisualNode
        {
            theme.StyleFor<TS>(action);
            return theme;
        }

        public static RxTheme UseTheme<T>(this T node, RxTheme theme) where T : VisualNode
        {
            theme.Add(node);
            return theme;
        }

        public static RxTheme UseTheme<T>(this IEnumerable<T> node, RxTheme theme) where T : VisualNode
        {
            theme.Add(node.ToArray());
            return theme;
        }
    }
}
