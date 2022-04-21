using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxLayout : IRxView
    {
        bool IsClippedToBounds { get; set; }
        bool CascadeInputTransparent { get; set; }
        Thickness Padding { get; set; }
    }

    public abstract class RxLayout<T> : RxView<T>, IEnumerable<VisualNode>, IRxLayout where T : Layout, new()
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

        public bool IsClippedToBounds { get; set; } = (bool)Xamarin.Forms.Layout.IsClippedToBoundsProperty.DefaultValue;
        public bool CascadeInputTransparent { get; set; } = (bool)Xamarin.Forms.Layout.CascadeInputTransparentProperty.DefaultValue;
        public Thickness Padding { get; set; } = (Thickness)Xamarin.Forms.Layout.PaddingProperty.DefaultValue;

        protected override void OnUpdate()
        {
            if (NativeControl.IsClippedToBounds != IsClippedToBounds) NativeControl.IsClippedToBounds = IsClippedToBounds;
            if (NativeControl.CascadeInputTransparent != CascadeInputTransparent) NativeControl.CascadeInputTransparent = CascadeInputTransparent;
            if (NativeControl.Padding != Padding) NativeControl.Padding = Padding;

            base.OnUpdate();
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
                return;
                //throw new ArgumentNullException(nameof(nodes));
            }

            foreach (var node in nodes)
                _internalChildren.Add(node);
        }

        public void Add(object genericNode)
        {
            if (genericNode == null)
            {
                return;
            }

            if (genericNode is VisualNode visualNode)
            {
                _internalChildren.Add(visualNode);
            }
            else if (genericNode is IEnumerable nodes)
            {
                foreach (var node in nodes.Cast<VisualNode>())
                    _internalChildren.Add(node);
            }
            else
            {
                throw new NotSupportedException($"Unable to add value of type '{genericNode.GetType()}' under {typeof(T)}");
            }        
        }
    }

    public static class RxLayoutExtensions
    {
        public static T IsClippedToBounds<T>(this T layout, bool isClippedToBounds) where T : IRxLayout
        {
            layout.IsClippedToBounds = isClippedToBounds;
            return layout;
        }

        public static T CascadeInputTransparent<T>(this T layout, bool cascadeInputTransparent) where T : IRxLayout
        {
            layout.CascadeInputTransparent = cascadeInputTransparent;
            return layout;
        }

        public static T Padding<T>(this T layout, Thickness padding) where T : IRxLayout
        {
            layout.Padding = padding;
            return layout;
        }

        public static T Padding<T>(this T layout, double leftRight, double topBottom) where T : IRxLayout
        {
            layout.Padding = new Thickness(leftRight, topBottom);
            return layout;
        }

        public static T Padding<T>(this T layout, double left, double top, double right, double bottom) where T : IRxLayout
        {
            layout.Padding = new Thickness(left, top, right, bottom);
            return layout;
        }

        public static T Padding<T>(this T layout, double uniformSize) where T : IRxLayout
        {
            layout.Padding = new Thickness(uniformSize);
            return layout;
        }
    }
}