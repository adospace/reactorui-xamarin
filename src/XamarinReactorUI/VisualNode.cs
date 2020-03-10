using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public abstract class VisualNode
    {
        protected VisualNode()
        {
            //System.Diagnostics.Debug.WriteLine($"{this}->Created()");
        }

        public object Key { get; set; }

        protected VisualNode Parent { get; private set; }

        private bool _invalidated = false;
        protected void Invalidate()
        {
            _invalidated = true;
            //System.Diagnostics.Debug.WriteLine($"{this}->Invalidated()");
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
                //System.Diagnostics.Debug.WriteLine($"{this}->Layout(Invalidated)");
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


        internal void AddChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {
            if (widget is null)
            {
                throw new ArgumentNullException(nameof(widget));
            }

            if (nativeControl is null)
            {
                throw new ArgumentNullException(nameof(nativeControl));
            }

            OnAddChild(widget, nativeControl);
        }

        protected virtual void OnAddChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {

        }

        internal void RemoveChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {
            if (widget is null)
            {
                throw new ArgumentNullException(nameof(widget));
            }

            if (nativeControl is null)
            {
                throw new ArgumentNullException(nameof(nativeControl));
            }

            OnRemoveChild(widget, nativeControl);
        }

        protected virtual void OnRemoveChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {

        }

        private IValueSet _context;
        public IValueSet Context
        {
            get => _context ?? Parent?.Context;
            set => _context = value;
        }

        public T GetContext<T>() where T : new()
        {
            var context = Context;
            if (context is T)
                return (T)context;

            var newContext = new T();
            context.CopyPropertiesTo(newContext);
            return newContext;
        }

        private readonly Dictionary<string, object> _metadata = new Dictionary<string, object>();
        public void SetMetadata<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("can'be null or empty", nameof(key));
            }

            _metadata[key] = value;
        }

        public void SetMetadata<T>(T value)
        {
            _metadata[typeof(T).FullName] = value;
        }

        public T GetMetadata<T>(string key, T defaultValue = default)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("can'be null or empty", nameof(key));
            }

            if (_metadata.TryGetValue(key, out var value))
                return (T)value;

            return defaultValue;
        }

        public T GetMetadata<T>(T defaultValue = default) 
            => GetMetadata(typeof(T).FullName, defaultValue);
    }

    public static class VisualNodeExtensions
    {
        public static T WithMetadata<T>(this T node, string key, object value) where T : VisualNode
        {
            node.SetMetadata(key, value);
            return node;
        }

        public static T WithMetadata<T>(this T node, object value) where T : VisualNode
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            node.SetMetadata(value.GetType().FullName, value);
            return node;
        }
    }
}
