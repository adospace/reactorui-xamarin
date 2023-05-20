using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using XamarinReactorUI.Internals;

namespace XamarinReactorUI
{
    public interface IRxComponent
    {
        RxContext Context { get; }
    }

    public abstract class RxComponent : VisualNode, IEnumerable<VisualNode>, IVisualNodeWithAttachedProperties
    {
        private readonly Dictionary<BindableProperty, object> _attachedProperties = new Dictionary<BindableProperty, object>();

        public abstract VisualNode Render();

        public void SetAttachedProperty(BindableProperty property, object value)
            => _attachedProperties[property] = value;

        private BindableObject _nativeControl;

        private readonly List<VisualNode> _children = new List<VisualNode>();

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(params VisualNode[] nodes)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            _children.AddRange(nodes);
        }

        protected new IReadOnlyList<VisualNode> Children()
            => _children;

        private IRxHostElement GetPageHost()
        {
            var current = Parent;
            while (current != null && !(current is IRxHostElement))
                current = current.Parent;

            return current as IRxHostElement;
        }

        protected Page ContainerPage
        {
            get
            {
                return GetPageHost()?.ContainerPage;
            }
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            foreach (var attachedProperty in _attachedProperties)
            {
                nativeControl.SetValue(attachedProperty.Key, attachedProperty.Value);
            }

            Parent.AddChild(this, nativeControl);

            _nativeControl = nativeControl;
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            Parent.RemoveChild(this, nativeControl);
            
            foreach (var attachedProperty in _attachedProperties)
            {
                nativeControl.SetValue(attachedProperty.Key, attachedProperty.Key.DefaultValue);
            }

            _nativeControl = null;
        }

        protected sealed override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Render();
        }

        protected sealed override void OnUpdate()
        {
            if (_nativeControl != null)
            {
                foreach (var attachedProperty in _attachedProperties)
                {
                    _nativeControl.SetValue(attachedProperty.Key, attachedProperty.Value);
                }
            }

            base.OnUpdate();
        }

        protected sealed override void OnAnimate()
        {
            base.OnAnimate();
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode.GetType().FullName == GetType().FullName)
            {
                ((RxComponent)newNode)._isMounted = true;
                ((RxComponent)newNode)._nativeControl = _nativeControl;
                _nativeControl = null;
                ((RxComponent)newNode).OnPropsChanged();
                base.MergeWith(newNode);
            }
            else
            {
                Unmount();
            }
        }

        protected sealed override void OnMount()
        {
            //System.Diagnostics.Debug.WriteLine($"Mounting {Key ?? GetType()} under {Parent.Key ?? Parent.GetType()} at index {ChildIndex}");

            base.OnMount();

            OnMounted();
        }

        protected virtual void OnMounted()
        {
        }

        protected sealed override void OnUnmount()
        {
            OnWillUnmount();

            foreach (var child in base.Children)
            {
                child.Unmount();
            }

            base.OnUnmount();
        }

        protected virtual void OnWillUnmount()
        {
        }

        protected virtual void OnPropsChanged()
        { }

        public INavigation Navigation
            => RxApplication.Instance.Navigation;

        public RxContext Context
            => RxApplication.Instance.Context;

    }

    public static class RxComponentExtensions
    {
        public static T WithContext<T>(this T node, string key, object value) where T : IRxComponent
        {
            node.Context[key] = value;
            return node;
        }
    }

    internal interface IRxComponentWithState
    {
        object State { get; internal set; }

        PropertyInfo[] StateProperties { get; }

        void ForwardState(object stateFromOldComponent);

        bool InheritedState { get; }
    }

    internal interface IRxComponentWithProps
    {
        object Props { get; internal set; }

        PropertyInfo[] PropsProperties { get; }
    }

    public interface IState
    {
    }

    public interface IProps
    {
    }

    public abstract class RxComponentWithProps<P> : RxComponent, IRxComponentWithProps where P : class, IProps, new()
    {
        private readonly bool _derivedProps;
        private P _props;

        public RxComponentWithProps(P props = null)
        {
            _props = props;
            _derivedProps = props != null;
        }

        public P Props
        {
            get => _props ??= new P();
        }

        object IRxComponentWithProps.Props
        {
            get => Props;
            set
            {
                if (_props != null)
                {
                    throw new InvalidOperationException();
                }

                _props = (P)value;
            }
        }

        public PropertyInfo[] PropsProperties => typeof(P).GetProperties().Where(_ => _.CanWrite).ToArray();
        internal override void MergeWith(VisualNode newNode)
        {
            if (!_derivedProps && newNode is IRxComponentWithProps newComponentWithProps)
            {
                if (newNode.GetType() == GetType())
                {
                    newComponentWithProps.Props = Props;
                }
                else if (newNode.GetType().FullName == this.GetType().FullName)
                {
                    Props.CopyPropertiesTo(newComponentWithProps.Props, newComponentWithProps.PropsProperties);
                }
            }

            base.MergeWith(newNode);
        }
    }

    public abstract class RxComponent<S, P> : RxComponentWithProps<P>, IRxComponentWithState where S : class, IState, new() where P : class, IProps, new()
    {
        private IRxComponentWithState _newComponent;
        private readonly bool _inheritedState;
        private S _state;

        protected RxComponent(S state = null, P props = null)
            : base(props)
        {
            _state = state;
            _inheritedState = state != null;
        }

        public S State
        {
            get => _state ??= new S();
        }

        object IRxComponentWithState.State
        {
            get => State;
            set
            {
                if (_state != null)
                {
                    throw new InvalidOperationException();
                }
                _state = (S)value;
            }
        }

        public PropertyInfo[] StateProperties => typeof(S).GetProperties().Where(_ => _.CanWrite).ToArray();

        bool IRxComponentWithState.InheritedState => _inheritedState;

        void IRxComponentWithState.ForwardState(object stateFromOldComponent)
        {
            if (Application.Current.Dispatcher.IsInvokeRequired)
                Application.Current.Dispatcher.BeginInvokeOnMainThread(Invalidate);
            else
                Invalidate();
        }

        protected virtual void SetState(Action<S> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (_newComponent != null && _newComponent.State is S newComponentState)
            {
                System.Diagnostics.Debug.WriteLine("XamarinReactorUI: Forward state to new component");
                action(newComponentState);
                _newComponent.ForwardState(newComponentState);
            }
            else
            {
                action(State);

                if (Application.Current.Dispatcher.IsInvokeRequired)
                    Application.Current.Dispatcher.BeginInvokeOnMainThread(Invalidate);
                else
                    Invalidate();
            }
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode is IRxComponentWithState newComponentWithState)
            {
                _newComponent = newComponentWithState;
                if (!_newComponent.InheritedState)
                {
                    if (newNode.GetType() == this.GetType())
                    {
                        newComponentWithState.State = State;
                    }
                    else if (newNode.GetType().FullName == this.GetType().FullName)
                    {
                        System.Diagnostics.Debug.WriteLine("WARNING: State copied after hot-reload");
                        State.CopyPropertiesTo(newComponentWithState.State, newComponentWithState.StateProperties);
                    }
                }
            }

            base.MergeWith(newNode);
        }
    }

    public class EmptyProps : IProps
    { }

    public abstract class RxComponent<S> : RxComponent<S, EmptyProps> where S : class, IState, new()
    {
        protected RxComponent(S state = null, EmptyProps props = null)
            : base(state, props)
        {
        }
    }
}