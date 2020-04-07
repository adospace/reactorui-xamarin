using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxComponent
    {
        RxContext Context { get; }
    }

    public abstract class RxComponent : VisualNode
    {
        public abstract VisualNode Render();

        public Page _containerPage;

        private RxComponent GetAncestorComponent()
        {
            var current = Parent;
            while (current != null && !(current is RxComponent))
                current = current.Parent;

            return current as RxComponent;
        }

        protected Page ContainerPage
        {
            get
            {
                return _containerPage ?? GetAncestorComponent()?.ContainerPage;
            }
            set
            {
                _containerPage = value;
            }
        }

        protected sealed override void OnAddChild(VisualNode widget, Element nativeControl)
        {
            if (nativeControl is Page page)
            {
                ContainerPage = page;
            }

            Parent.AddChild(this, nativeControl);
        }

        protected sealed override void OnRemoveChild(VisualNode widget, Element nativeControl)
        {
            if (ContainerPage == nativeControl)
            {
                ContainerPage = null;
            }

            Parent.RemoveChild(this, nativeControl);
        }

        protected sealed override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Render();
        }

        protected sealed override void OnUpdate()
        {
            base.OnUpdate();
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode.GetType().FullName == GetType().FullName)
            {
                ((RxComponent)newNode)._isMounted = true;

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

            foreach (var child in Children)
            {
                child.Unmount();
            }

            base.OnUnmount();
        }

        protected virtual void OnWillUnmount()
        {
            
        }

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
        object State { get; }

        PropertyInfo[] Properties { get; }
    }

    public abstract class RxComponent<S> : RxComponent, IRxComponentWithState where S : class, IValueSet, new()
    {
        protected RxComponent(S state = null)
        {
            State = state ?? new S();
        }

        public S State { get; private set; }

        public PropertyInfo[] Properties => typeof(S).GetProperties().Where(_ => _.CanWrite).ToArray();

        object IRxComponentWithState.State => State;

        protected virtual void SetState(Action<S> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(State);
            if (Application.Current.Dispatcher.IsInvokeRequired)
                Application.Current.Dispatcher.BeginInvokeOnMainThread(Invalidate);
            else
                Invalidate();
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode is IRxComponentWithState newComponent)
            {
                State.CopyPropertiesTo(newComponent.State, newComponent.Properties);
            }

            base.MergeWith(newNode);
        }


    }
}
