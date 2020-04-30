using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTableRoot
    {
    }

    public class RxTableRoot : RxTableSectionBase<TableRoot>, IRxTableRoot
    {
        public RxTableRoot()
        {
        }

        public RxTableRoot(Action<TableRoot> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is TableSection section)
                NativeControl.Add(section);
            else
            {
                throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, nativeControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is TableSection section)
                NativeControl.Remove(section);
            else
            {
                throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnRemoveChild(widget, nativeControl);
        }

        protected override void OnUpdate()
        {

            base.OnUpdate();
        }

    }

    public static class RxTableRootExtensions
    {
    }
}