using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTableSection
    {
    }

    public class RxTableSection : RxTableSectionBase<TableSection>, IRxTableSection
    {
        public RxTableSection()
        {
        }

        public RxTableSection(Action<TableSection> componentRefAction)
            :base(componentRefAction)
        {
        }

        protected override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is Cell cell)
                NativeControl.Add(cell);
            else
            {
                throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, nativeControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is Cell cell)
                NativeControl.Remove(cell);
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

    public static class RxTableSectionExtensions
    {
    }
}