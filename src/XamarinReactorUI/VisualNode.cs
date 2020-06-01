using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XamarinReactorUI.Animations;

namespace XamarinReactorUI
{
    public interface IVisualNode
    {
        void AppendAnimatable<T>(object key, T animation, Action<T> action) where T : RxAnimation;
    }

    public static class VisualNodeExtensions
    {
        public static T OnPropertyChanged<T>(this T element, Action<object, PropertyChangedEventArgs> action) where T : VisualNode
        {
            element.PropertyChangedAction = action;
            return element;
        }

        public static T OnPropertyChanging<T>(this T element, Action<object, System.ComponentModel.PropertyChangingEventArgs> action) where T : VisualNode
        {
            element.PropertyChangingAction = action;
            return element;
        }

        public static T When<T>(this T node, bool flag, Action<T> actionToApplyWhenFlagIsTrue) where T : VisualNode
        {
            if (flag)
            {
                actionToApplyWhenFlagIsTrue(node);
            }
            return node;
        }

        public static T WithAnimation<T>(this T node, Easing easing = null, double duration = 600) where T : VisualNode
        {
            node.EnableCurrentAnimatableProperties(easing, duration);
            return node;
        }

        public static T WithKey<T>(this T node, object key) where T : VisualNode
        {
            node.Key = key;
            return node;
        }

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
        public static T WithOutAnimation<T>(this T node) where T : VisualNode
        {
            node.DisableCurrentAnimatableProperties();
            return node;
        }
    }

    public abstract class VisualNode : IVisualNode
    {
        protected bool _isMounted = false;

        protected bool _stateChanged = true;

        private readonly Dictionary<object, Animatable> _animatables = new Dictionary<object, Animatable>();

        private readonly Dictionary<string, object> _metadata = new Dictionary<string, object>();

        private IReadOnlyList<VisualNode> _children = null;

        private bool _invalidated = false;

        protected VisualNode()
        {
            //System.Diagnostics.Debug.WriteLine($"{this}->Created()");
        }

        public int ChildIndex { get; private set; }
        public object Key { get; set; }
        public Action<object, PropertyChangedEventArgs> PropertyChangedAction { get; set; }
        public Action<object, System.ComponentModel.PropertyChangingEventArgs> PropertyChangingAction { get; set; }
        //internal event EventHandler LayoutCycleRequest;
        internal IReadOnlyList<VisualNode> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<VisualNode>(RenderChildren().Where(_ => _ != null));
                    for (int i = 0; i < _children.Count; i++)
                    {
                        _children[i].ChildIndex = i;
                        _children[i].Parent = this;
                    }
                }
                return _children;
            }
        }

        internal bool IsAnimationFrameRequested { get; private set; } = false;
        internal bool IsLayoutCycleRequired { get; set; } = true;
        internal VisualNode Parent { get; private set; }

        public void AppendAnimatable<T>(object key, T animation, Action<T> action) where T : RxAnimation
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (animation is null)
            {
                throw new ArgumentNullException(nameof(animation));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var newAnimatableProperty = new Animatable(key, animation, new Action<RxAnimation>(target => action((T)target)));

            _animatables[key] = newAnimatableProperty;
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

        internal void AddChild(VisualNode widget, BindableObject childNativeControl)
        {
            OnAddChild(widget, childNativeControl);
        }

        internal bool Animate()
        {
            if (!IsAnimationFrameRequested)
                return false;

            var animated = AnimateThis();

            OnAnimate();

            foreach (var child in Children)
            {
                if (child.Animate())
                    animated = true;
            }

            if (!animated)
            {
                IsAnimationFrameRequested = false;
            }

            return animated;
        }

        internal void DisableCurrentAnimatableProperties()
        {
            _animatables.Where(_ => _.Value.IsEnabled == null).ForEach(_ =>
              {
                  _.Value.IsEnabled = false;
              });
        }

        internal void EnableCurrentAnimatableProperties(Easing easing = null, double duration = 600)
        {
            _animatables.Where(_ => _.Value.IsEnabled == null).Select(_ => _.Value).ForEach(_ =>
              {
                  if (_.Animation is RxTweenAnimation tweenAnimation)
                  {
                      tweenAnimation.Easing = tweenAnimation.Easing ?? easing;
                      tweenAnimation.Duration = tweenAnimation.Duration ?? duration;
                      _.IsEnabled = true;
                  }
              });
        }

        internal virtual void Layout(RxTheme theme = null)
        {
            if (!IsLayoutCycleRequired)
                return;

            IsLayoutCycleRequired = false;

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

            CommitAnimations();

            AnimateThis();

            if (_stateChanged)
                OnUpdate();

            foreach (var child in Children)
                child.Layout(theme);
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
                oldChildren[i].Unmount();
                oldChildren[i].Parent = null;
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
                Children[i].Unmount();
                Children[i].Parent = null;
            }

            Parent = null;
        }

        internal void RemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
            OnRemoveChild(widget, childNativeControl);
        }

        internal void Unmount()
        {
            OnUnmount();
        }

        protected internal virtual void OnLayoutCycleRequested()
        {
        }

        protected virtual void CommitAnimations()
        {
            if (_animatables.Any(_ => _.Value.IsEnabled.GetValueOrDefault() && !_.Value.Animation.IsCompleted()))
            {
                RequestAnimationFrame();
            }
        }

        protected T GetParent<T>() where T : VisualNode
        {
            var parent = Parent;
            while (parent != null && !(parent is T))
                parent = parent.Parent;

            return (T)parent;
        }
        protected void Invalidate()
        {
            _invalidated = true;

            RequireLayoutCycle();

            OnInvalidated();
            //System.Diagnostics.Debug.WriteLine($"{this}->Invalidated()");
        }

        protected virtual void OnAddChild(VisualNode widget, BindableObject childNativeControl)
        {
        }

        protected virtual void OnAnimate()
        {
        }

        protected virtual void OnInvalidated()
        {
        }
        protected virtual void OnMigrated(VisualNode newNode)
        {
            foreach (var newAnimatableProperty in newNode._animatables)
            {
                if (_animatables.TryGetValue(newAnimatableProperty.Key, out var oldAnimatableProperty))
                {
                    if (oldAnimatableProperty.Animation.GetType() == newAnimatableProperty.Value.Animation.GetType())
                    {
                        newAnimatableProperty.Value.Animation.MigrateFrom(oldAnimatableProperty.Animation);
                    }
                }
            }

            _animatables.Clear();
        }

        protected virtual void OnMount()
        {
            _isMounted = true;
        }

        protected virtual void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
        }

        protected virtual void OnUnmount()
        {
            _isMounted = false;
        }

        protected virtual void OnUpdate()
        {
            _stateChanged = false;
        }

        protected abstract IEnumerable<VisualNode> RenderChildren();

        private bool AnimateThis()
        {
            bool animated = false;
            _animatables.Where(_ => _.Value.IsEnabled.GetValueOrDefault() && !_.Value.Animation.IsCompleted()).ForEach(_ =>
              {
                  _.Value.Animate();
                  animated = true;
              });

            return animated;
        }

        private void RequestAnimationFrame()
        {
            IsAnimationFrameRequested = true;
            Parent?.RequestAnimationFrame();
        }

        private void RequireLayoutCycle()
        {
            if (IsLayoutCycleRequired)
                return;

            IsLayoutCycleRequired = true;
            Parent?.RequireLayoutCycle();
            OnLayoutCycleRequested();
        }
    }

    public abstract class VisualNode<T> : VisualNode where T : BindableObject, new()
    {
        protected BindableObject _nativeControl;

        private readonly Dictionary<BindableProperty, object> _attachedProperties = new Dictionary<BindableProperty, object>();
        private readonly Action<T> _componentRefAction;
        protected VisualNode()
        { }

        protected VisualNode(Action<T> componentRefAction)
        {
            _componentRefAction = componentRefAction;
        }

        protected T NativeControl { get => (T)_nativeControl; }
        public void SetAttachedProperty(BindableProperty property, object value)
            => _attachedProperties[property] = value;

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode.GetType() == GetType())
            {
                ((VisualNode<T>)newNode)._nativeControl = this._nativeControl;
                ((VisualNode<T>)newNode)._isMounted = this._nativeControl != null;
                ((VisualNode<T>)newNode)._componentRefAction?.Invoke(NativeControl);
                OnMigrated(newNode);

                base.MergeWith(newNode);
            }
            else
            {
                this.Unmount();
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
                NativeControl.PropertyChanging -= NativeControl_PropertyChanging;

                foreach (var attachedProperty in _attachedProperties)
                {
                    NativeControl.SetValue(attachedProperty.Key, attachedProperty.Key.DefaultValue);
                }
            }

            _attachedProperties.Clear();

            base.OnMigrated(newNode);
        }

        protected override void OnMount()
        {
            _nativeControl = _nativeControl ?? new T();
            Parent.AddChild(this, _nativeControl);
            _componentRefAction?.Invoke(NativeControl);

            base.OnMount();
        }

        protected override void OnUnmount()
        {
            if (_nativeControl != null)
            {
                _nativeControl.PropertyChanged -= NativeControl_PropertyChanged;
                _nativeControl.PropertyChanging -= NativeControl_PropertyChanging;
                Parent.RemoveChild(this, _nativeControl);

                if (_nativeControl is Element element)
                {
                    if (element.Parent != null)
                    {
#if DEBUG
                        //throw new InvalidOperationException();
#endif
                    }
                }

                _nativeControl = null;
                _componentRefAction?.Invoke(null);
            }

            base.OnUnmount();
        }

        protected override void OnUpdate()
        {
            foreach (var attachedProperty in _attachedProperties)
            {
                NativeControl.SetValue(attachedProperty.Key, attachedProperty.Value);
            }

            if (PropertyChangedAction != null)
                NativeControl.PropertyChanged += NativeControl_PropertyChanged;
            if (PropertyChangingAction != null)
                NativeControl.PropertyChanging += NativeControl_PropertyChanging;

            base.OnUpdate();
        }

        private void NativeControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedAction?.Invoke(sender, e);
        }

        private void NativeControl_PropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {
            PropertyChangingAction?.Invoke(sender, new System.ComponentModel.PropertyChangingEventArgs(e.PropertyName));
        }
    }
}