using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public abstract class RxComponent : VisualNode
    {
        public abstract VisualNode Render();

        protected sealed override void OnAddChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {
            Parent.AddChild(widget, nativeControl);
        }

        protected sealed override void OnRemoveChild(RxElement widget, Xamarin.Forms.VisualElement nativeControl)
        {
            Parent.RemoveChild(widget, nativeControl);
        }

        protected sealed override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Render();
        }

        protected sealed override void OnMount()
        {
            base.OnMount();

            OnMounted();
        }

        protected virtual void OnMounted()
        { 
        
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

        public PropertyInfo[] Properties => typeof(S).GetProperties();

        object IRxComponentWithState.State => State;

        protected virtual void SetState(Action<S> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(State);
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
