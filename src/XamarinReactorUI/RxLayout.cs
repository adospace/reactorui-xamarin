using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxLayout
    {
        Thickness Padding { get; set; }
    }

    public abstract class RxLayout<T> : RxView<T>, IEnumerable<VisualNode>, IRxLayout where T : Xamarin.Forms.Layout, new()
    {
        protected RxLayout(params VisualNode[] children)
        {
            if (children is null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            _internalChildren.AddRange(children);
        }

        protected RxLayout(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        private readonly List<VisualNode> _internalChildren = new List<VisualNode>();

        private readonly NullableField<Thickness> _padding = new NullableField<Thickness>();
        public Thickness Padding { get => _padding.GetValueOrDefault(); set => _padding.Value = value; }

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

        //public void Add(VisualNode node)
        //{
        //    if (node is null)
        //    {
        //        throw new ArgumentNullException(nameof(node));
        //    }

        //    _internalChildren.Add(node);
        //}

        public void Add(params VisualNode[] nodes)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            foreach (var node in nodes)
                _internalChildren.Add(node);
        }

        protected override void OnUpdate()
        {
            if (_padding.HasValue) NativeControl.Padding = _padding.Value;

            base.OnUpdate();
        }
    }

    public static class RxLayoutExtensions
    {
        public static T Padding<T>(this T layout, Thickness padding) where T : IRxLayout
        {
            layout.Padding = padding;
            return layout;        
        }

        public static T Padding<T>(this T button, double leftRight, double topBottom) where T : IRxLayout
        {
            button.Padding = new Thickness(leftRight, topBottom);
            return button;
        }

        public static T Padding<T>(this T button, double uniformSize) where T : IRxLayout
        {
            button.Padding = new Thickness(uniformSize);
            return button;
        }
    }
}
