﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxShellItem : IRxShellGroupItem
    {
    }

    public abstract class RxShellItem<T> : RxShellGroupItem<T>, IRxShellItem, IEnumerable<IRxShellSection> where T : Xamarin.Forms.ShellItem, new()
    {
        private readonly List<IRxShellSection> _items = new List<IRxShellSection>();
        private readonly Dictionary<BindableObject, ShellSection> _elementItemMap = new Dictionary<BindableObject, ShellSection>();

        public RxShellItem()
        {

        }

        public RxShellItem(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public void Add(IRxShellSection section)
        {
            _items.Add(section);
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is ShellSection item)
                NativeControl.Items.Insert(widget.ChildIndex, item);
            else if (childControl is Page page)
                NativeControl.Items.Insert(widget.ChildIndex, new ShellContent() { Content = page });
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            _elementItemMap[childControl] = NativeControl.Items[NativeControl.Items.Count - 1];

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            NativeControl.Items.Remove(_elementItemMap[childControl]);

            base.OnRemoveChild(widget, childControl);
        }

        public IEnumerator<IRxShellSection> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        protected override void OnUpdate()
        {

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _items.Cast<VisualNode>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

    public static class RxShellItemExtensions
    {
    }

}
