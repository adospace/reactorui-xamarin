using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTableView
    {
        int RowHeight { get; set; }
        bool HasUnevenRows { get; set; }
        TableIntent Intent { get; set; }
    }

    public class RxTableView<T> : RxView<T>, IRxTableView, IEnumerable<RxTableRoot> where T : TableView, new()
    {
        public readonly List<RxTableRoot> _children = new List<RxTableRoot>();

        public RxTableView()
        {
        }

        public RxTableView(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public int RowHeight { get; set; } = (int)TableView.RowHeightProperty.DefaultValue;
        public bool HasUnevenRows { get; set; } = (bool)TableView.HasUnevenRowsProperty.DefaultValue;
        public TableIntent Intent { get; set; }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is TableRoot root)
                NativeControl.Root = root;
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            if (childControl is TableRoot _)
                NativeControl.Root = null;
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnRemoveChild(widget, childControl);
        }


        public IEnumerator<RxTableRoot> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        protected override void OnUpdate()
        {
            NativeControl.RowHeight = RowHeight;
            NativeControl.HasUnevenRows = HasUnevenRows;
            NativeControl.Intent = Intent;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _children;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(RxTableRoot root)
        {
            if (_children.Count > 0)
                throw new InvalidOperationException("Root already added");

            _children.Add(root);
        }
    }

    public class RxTableView : RxTableView<TableView>
    {
        public RxTableView()
        {
        }

        public RxTableView(Action<TableView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxTableViewExtensions
    {
        public static T RowHeight<T>(this T tableview, int rowHeight) where T : IRxTableView
        {
            tableview.RowHeight = rowHeight;
            return tableview;
        }

        public static T HasUnevenRows<T>(this T tableview, bool hasUnevenRows) where T : IRxTableView
        {
            tableview.HasUnevenRows = hasUnevenRows;
            return tableview;
        }

        public static T Intent<T>(this T tableview, TableIntent intent) where T : IRxTableView
        {
            tableview.Intent = intent;
            return tableview;
        }

    }
}