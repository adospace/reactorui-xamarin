using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public abstract class VisualNode
    {
        protected VisualNode()
        {
            System.Diagnostics.Debug.WriteLine($"{this}->Created()");
        }

        public object Key { get; set; }

        protected VisualNode Parent { get; private set; }

        private bool _invalidated = false;
        protected void Invalidate()
        {
            _invalidated = true;
            System.Diagnostics.Debug.WriteLine($"{this}->Invalidated()");
        }

        private IReadOnlyList<VisualNode> _children = null;
        internal IReadOnlyList<VisualNode> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<VisualNode>(RenderChildren());
                    foreach (var child in _children)
                        child.Parent = this;
                }
                return _children;
            }
        }

        internal virtual void MergeWith(VisualNode newNode)
        {
            if (newNode == this)
                return;

            for (int i = 0; i < Children.Count; i++)
            {
                if (newNode.Children.Count > i)
                {
                    Children[i].MergeWith(newNode.Children[i]);
                }
            }

            for (int i = newNode.Children.Count; i < Children.Count; i++)
            {
                Children[i].OnUnmount();
                Children[i].Parent = null;
            }

            Parent = null;
        }

        internal virtual void MergeChildrenFrom(IReadOnlyList<VisualNode> oldChildren)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (oldChildren.Count > i)
                {
                    oldChildren[i].MergeWith(Children[i]);
                }
            }

            for (int i = Children.Count; i < oldChildren.Count; i++)
            {
                oldChildren[i].OnUnmount();
                oldChildren[i].Parent = null;
            }
        }

        protected abstract IEnumerable<VisualNode> RenderChildren();

        protected bool _isMounted = false;
        protected bool _stateChanged = true;

        internal void Layout()
        {
            if (_invalidated)
            {
                System.Diagnostics.Debug.WriteLine($"{this}->Layout(Invalidated)");
                var oldChildren = Children;
                _children = null;
                MergeChildrenFrom(oldChildren);
                _invalidated = false;
            }

            if (!_isMounted && Parent != null)
                OnMount();

            if (_stateChanged)
                OnUpdate();

            foreach (var child in Children)
                child.Layout();

        }

        protected virtual void OnMount()
        {
            _isMounted = true;
        }

        protected virtual void OnUnmount()
        {
            _isMounted = false;
        }

        protected virtual void OnUpdate()
        {
            _stateChanged = false;
        }


        internal void AddChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {
            OnAddChild(widget, nativeControl);
        }

        protected virtual void OnAddChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {

        }

        internal void RemoveChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {
            OnRemoveChild(widget, nativeControl);
        }

        protected virtual void OnRemoveChild(RxElement widget, Xamarin.Forms.View nativeControl)
        {

        }
    }
}
