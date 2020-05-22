using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace XamarinReactorUI
{
    public interface IVisualNode
    {
        void AppendAnimatableProperty(object key, AnimatableProperty action);

        void ClearAnimatableProperties();
    }

    public abstract class VisualNode : IVisualNode
    {
        protected VisualNode()
        {
            //System.Diagnostics.Debug.WriteLine($"{this}->Created()");
        }

        public object Key { get; set; }
        public int ChildIndex { get; private set; }
        internal VisualNode Parent { get; private set; }

        protected T GetParent<T>() where T : VisualNode
        {
            var parent = Parent;
            while (parent != null && !(parent is T))
                parent = parent.Parent;

            return (T)parent;
        }

        public Action<object, System.ComponentModel.PropertyChangingEventArgs> PropertyChangingAction { get; set; }
        public Action<object, PropertyChangedEventArgs> PropertyChangedAction { get; set; }


        private bool _invalidated = false;

        protected void Invalidate()
        {
            _invalidated = true;

            RequireLayoutCycle();

            OnInvalidated();
            //System.Diagnostics.Debug.WriteLine($"{this}->Invalidated()");
        }

        protected virtual void OnInvalidated() { }

        internal bool IsLayoutCycleRequired { get; set; } = true;

        private void RequireLayoutCycle()
        {
            if (IsLayoutCycleRequired)
                return;

            IsLayoutCycleRequired = true;
            Parent?.RequireLayoutCycle();
            OnLayoutCycleRequested();
        }

        protected internal virtual void OnLayoutCycleRequested()
        {
        }

        //internal event EventHandler LayoutCycleRequest;

        private IReadOnlyList<VisualNode> _children = null;

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

        protected abstract IEnumerable<VisualNode> RenderChildren();

        protected bool _isMounted = false;
        protected bool _stateChanged = true;

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

            if (_stateChanged)
                OnUpdate();

            foreach (var child in Children)
                child.Layout(theme);

            CommitAnimations();
        }

        protected virtual void OnMount()
        {
            _isMounted = true;
        }

        internal void Unmount()
        {
            OnUnmount();
        }

        protected virtual void OnUnmount()
        {
            _isMounted = false;
        }

        protected virtual void OnUpdate()
        {
            _stateChanged = false;
        }

        protected virtual void OnMigrated(VisualNode newNode)
        {
        }

        internal void AddChild(VisualNode widget, BindableObject childNativeControl)
        {
            OnAddChild(widget, childNativeControl);
        }

        protected virtual void OnAddChild(VisualNode widget, BindableObject childNativeControl)
        {

        }

        internal void RemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
            OnRemoveChild(widget, childNativeControl);
        }

        protected virtual void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
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

        private readonly Dictionary<object, AnimatableProperty> _animatableProperties = new Dictionary<object, AnimatableProperty>();

        public void AppendAnimatableProperty(object key, AnimatableProperty animatableProperty)
        {
            animatableProperty.AttachNode(this);

            if (_animatableProperties.TryGetValue(key, out var existingAnimtableProperty))
            { 
                if (existingAnimtableProperty.GetType() == animatableProperty.GetType())
                {
                    animatableProperty.Migrate(existingAnimtableProperty);
                    return;
                }

                existingAnimtableProperty.IsEnabled = false;
            }

            _animatableProperties[key] = animatableProperty;
        }

        public void ClearAnimatableProperties()
        {
            _animatableProperties.Clear();
        }

        internal void EnableAnimatableProperties()
            => _animatableProperties.ForEach(_ => _.Value.IsEnabled = true);

        internal void RemoveDisabledAnimatableProperties()
            => _animatableProperties.ToList().ForEach(_ =>
            {
                if (!_.Value.IsEnabled)
                    _animatableProperties.Remove(_.Key);
            });

        protected virtual void CommitAnimations()
        {
            RemoveDisabledAnimatableProperties();

            if (_animatableProperties.Any())
            {
                RequestAnimationFrame();
            }
        }

        private void RequestAnimationFrame()
        {
            IsAnimationFrameRequested = true;
            Parent?.RequestAnimationFrame();
        }

        internal bool IsAnimationFrameRequested { get; private set; } = false;

        public void Animate()
        {
            if (!IsAnimationFrameRequested)
                return;

            _animatableProperties.ForEach(_ => 
            {
                _.Value.Animate();
            });

            foreach (var child in Children)
            {
                child.Animate();
            }
        }
    }

    public abstract class VisualNode<T> : VisualNode where T : BindableObject, new()
    {
        protected BindableObject _nativeControl;

        protected T NativeControl { get => (T)_nativeControl; }

        private readonly Action<T> _componentRefAction;
        private readonly Dictionary<BindableProperty, object> _attachedProperties = new Dictionary<BindableProperty, object>();
        protected VisualNode()
        { }

        protected VisualNode(Action<T> componentRefAction)
        {
            _componentRefAction = componentRefAction;
        }

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
                        throw new InvalidOperationException();
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

        public void SetAttachedProperty(BindableProperty property, object value)
            => _attachedProperties[property] = value;
    }

    public abstract class AnimatableProperty
    {
        public object Key { get; }
        public Easing Easing { get; }
        public double Duration { get; }

        public AnimatableProperty(object key, Easing easing = null, TimeSpan? duration = null)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Easing = easing ?? Easing.Linear;
            Duration = duration == null ? 600.0 : duration.Value.TotalMilliseconds;
        }

        public bool IsEnabled { get; internal set; }

        protected int CurrentEasingTime { get; set; }

        protected IVisualNode AttachedNode { get; private set; }

        internal void AttachNode(IVisualNode node)
            => AttachedNode = node;

        internal void Animate()
        {
            OnAnimate();
        }

        protected abstract void OnAnimate();

        internal void Migrate(AnimatableProperty animatableProperty)
        {
            OnMigrate(animatableProperty);
        }

        protected virtual void OnMigrate(AnimatableProperty animatableProperty)
        {
            CurrentEasingTime = animatableProperty.CurrentEasingTime;
        }
    }

    public class AnimatableDoubleProperty : AnimatableProperty
    {
        public AnimatableDoubleProperty(object key, double value, Action<IVisualNode, double> action, Easing easing = null, TimeSpan? duration = null) : base(key, easing, duration)
        {
            Value = value;
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public double Value { get; }
        public double? StartValue { get; private set; }
        public Action<IVisualNode, double> Action { get; }

        protected override void OnAnimate()
        {
            if (!IsEnabled)
                return;

            if (StartValue == null)
                return;

            if (CurrentEasingTime >= Duration)
                return;

            Action(AttachedNode, StartValue.Value + (Value - StartValue.Value) * Easing.Ease(CurrentEasingTime / Duration));
        }

        protected override void OnMigrate(AnimatableProperty animatableProperty)
        {
            StartValue = ((AnimatableDoubleProperty)animatableProperty).Value;
        }
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

        public static T WithKey<T>(this T node, object key) where T : VisualNode
        {
            node.Key = key;
            return node;
        }

        public static T WithAnimation<T>(this T node) where T : VisualNode
        {
            node.EnableAnimatableProperties();
            return node;
        }

        public static T WithOutAnimation<T>(this T node) where T : VisualNode
        {
            node.RemoveDisabledAnimatableProperties();
            return node;
        }
    }
}