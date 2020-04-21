using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxElement
    {
        void SetAttachedProperty(BindableProperty property, object value);

        Action<object, System.ComponentModel.PropertyChangingEventArgs> PropertyChangingAction { get; set; }
        Action<object, PropertyChangedEventArgs> PropertyChangedAction { get; set; }
    }

    public abstract class RxElement<T> : VisualNode, IRxElement where T : Element, new()
    {
        protected Element _nativeControl;

        protected T NativeControl { get => (T)_nativeControl; }

        public Action<object, System.ComponentModel.PropertyChangingEventArgs> PropertyChangingAction { get; set; }
        public Action<object, PropertyChangedEventArgs> PropertyChangedAction { get; set; }

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
        }

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
            if (NativeControl != null)
            {
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
                NativeControl.PropertyChanging -= NativeControl_PropertyChanging;
            }

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

        private readonly Dictionary<BindableProperty, object> _attachedProperties = new Dictionary<BindableProperty, object>();

        public void SetAttachedProperty(BindableProperty property, object value)
            => _attachedProperties[property] = value;
    }

    public static class RxElementExtensions
    {
        public static T OnPropertyChanged<T>(this T element, Action<object, PropertyChangedEventArgs> action) where T : IRxElement
        {
            element.PropertyChangedAction = action;
            return element;
        }

        public static T OnPropertyChanging<T>(this T element, Action<object, System.ComponentModel.PropertyChangingEventArgs> action) where T : IRxElement
        {
            element.PropertyChangingAction = action;
            return element;
        }
    }
}