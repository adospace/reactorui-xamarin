using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxElement
    {

    }

    public abstract class RxElement<T> : VisualNode, IRxElement where T : Element, new()
    {
        protected Element _nativeControl;

        protected T NativeControl { get => (T)_nativeControl; }

        private readonly Action<T> _componentRefAction;

        protected RxElement()
        { }

        protected RxElement(Action<T> componentRefAction)
        {
            _componentRefAction = componentRefAction;
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode.GetType() == GetType())
            {
                ((RxElement<T>)newNode)._nativeControl = this._nativeControl;
                ((RxElement<T>)newNode)._isMounted = this._nativeControl != null;
                OnMigrated(newNode);

                base.MergeWith(newNode);
            }
            else
            {
                this.Unmount();
            }
        }

        protected virtual void OnMigrated(VisualNode newNode)
        { }

        protected override void OnMount()
        {
            _nativeControl = _nativeControl ?? new T();
            //System.Diagnostics.Debug.WriteLine($"Mounting {Key ?? GetType()} under {Parent.Key ?? Parent.GetType()} at index {ChildIndex}");
            Parent.AddChild(this, _nativeControl);
            _componentRefAction?.Invoke(NativeControl);

            base.OnMount();
        }

        protected override void OnUnmount()
        {
            if (_nativeControl != null)
            {
                Parent.RemoveChild(this, _nativeControl);
                _nativeControl = null;
                _componentRefAction?.Invoke(null);
            }

            base.OnUnmount();
        }


        protected override void OnUpdate()
        {


            base.OnUpdate();
        }

    }


}
