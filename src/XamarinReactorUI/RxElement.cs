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

    //public abstract class RxElement : VisualNode
    //{
    //    protected Element _nativeControl;

    //    protected RxElement()
    //    { }

    //    private bool _styled = false;
    //    private RxTheme _theme = null;

    //    internal override void Layout(RxTheme theme = null)
    //    {
    //        _theme = theme;
    //        base.Layout(theme);
    //    }

    //    protected override void OnMount()
    //    {
    //        if (!_styled)
    //        {
    //            var styleForMe = _theme?.GetStyleFor(this);
    //            if (styleForMe != null)
    //            {
    //                var themedNode = (RxElement)Activator.CreateInstance(GetType());
    //                styleForMe(themedNode);
    //                themedNode._nativeControl = _nativeControl;
    //                themedNode.OnUpdate();
    //                themedNode.OnMigrated(this);
    //            }

    //            _styled = true;
    //        }


    //        base.OnMount();
    //    }

    //    protected virtual void OnMigrated(VisualNode newNode)
    //    {
    //    }
    //}

    public abstract class RxElement<T> : VisualNode<T>, IRxElement where T : Element, new()
    {

        //protected T NativeControl { get => (T)_nativeControl; }


        //private readonly Action<T> _componentRefAction;
        //private readonly Dictionary<BindableProperty, object> _attachedProperties = new Dictionary<BindableProperty, object>();
        //public Action<object, System.ComponentModel.PropertyChangingEventArgs> PropertyChangingAction { get; set; }
        //public Action<object, PropertyChangedEventArgs> PropertyChangedAction { get; set; }

        protected RxElement()
        { }

        protected RxElement(Action<T> componentRefAction)
            :base(componentRefAction)
        {
            //_componentRefAction = componentRefAction;
        }

        //internal override void MergeWith(VisualNode newNode)
        //{
        //    if (newNode.GetType() == GetType())
        //    {
        //        ((RxElement<T>)newNode)._nativeControl = this._nativeControl;
        //        ((RxElement<T>)newNode)._isMounted = this._nativeControl != null;
        //        ((RxElement<T>)newNode)._componentRefAction?.Invoke(NativeControl);
        //        OnMigrated(newNode);


        //        base.MergeWith(newNode);
        //    }
        //    else
        //    {
        //        this.Unmount();
        //    }
        //}

        //protected override void OnMigrated(VisualNode newNode)
        //{
        //    if (NativeControl != null)
        //    {
        //        NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
        //        NativeControl.PropertyChanging -= NativeControl_PropertyChanging;

        //        foreach (var attachedProperty in _attachedProperties)
        //        {
        //            NativeControl.SetValue(attachedProperty.Key, attachedProperty.Key.DefaultValue);
        //        }
        //    }

        //    _attachedProperties.Clear();

        //    base.OnMigrated(newNode);
        //}

        private bool _styled = false;
        private RxTheme _theme = null;

        internal override void Layout(RxTheme theme = null)
        {
            _theme = theme;
            base.Layout(theme);
        }

        protected override void OnMount()
        {
            base.OnMount();

            if (!_styled)
            {
                var styleForMe = _theme?.GetStyleFor(this);
                if (styleForMe != null)
                {
                    var themedNode = (RxElement<T>)Activator.CreateInstance(GetType());
                    styleForMe(themedNode);
                    themedNode._nativeControl = _nativeControl;
                    themedNode.OnUpdate();
                    themedNode.OnMigrated(this);
                }

                _styled = true;
            }
        }

        //protected override void OnUnmount()
        //{
        //    if (NativeControl != null)
        //    {
        //        NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
        //        NativeControl.PropertyChanging -= NativeControl_PropertyChanging;
        //    }

        //    if (_nativeControl != null)
        //    {
        //        Parent.RemoveChild(this, _nativeControl);
        //        _nativeControl = null;
        //        _componentRefAction?.Invoke(null);
        //    }

        //    base.OnUnmount();
        //}

        //protected override void OnUpdate()
        //{
        //    foreach (var attachedProperty in _attachedProperties)
        //    {
        //        NativeControl.SetValue(attachedProperty.Key, attachedProperty.Value);
        //    }

        //    if (PropertyChangedAction != null)
        //        NativeControl.PropertyChanged += NativeControl_PropertyChanged;
        //    if (PropertyChangingAction != null)
        //        NativeControl.PropertyChanging += NativeControl_PropertyChanging;

        //    base.OnUpdate();
        //}

        //private void NativeControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    PropertyChangedAction?.Invoke(sender, e);
        //}

        //private void NativeControl_PropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        //{
        //    PropertyChangingAction?.Invoke(sender, new System.ComponentModel.PropertyChangingEventArgs(e.PropertyName));
        //}

        //public void SetAttachedProperty(BindableProperty property, object value)
        //    => _attachedProperties[property] = value;
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