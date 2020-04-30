//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Xamarin.Forms;

//namespace XamarinReactorUI
//{
//    public interface IRxTableView
//    {
//        int RowHeight { get; set; }
//        bool HasUnevenRows { get; set; }
//    }

//    public class RxTableView<T> : RxView<T>, IRxTableView, IEnumerable<RxTableSection> where T : TableView, new()
//    {
//        public readonly List<VisualNode> _children = new List<VisualNode>();

//        public RxTableView()
//        {
//        }

//        public RxTableView(Action<T> componentRefAction)
//            : base(componentRefAction)
//        {
//        }

//        public int RowHeight { get; set; } = (int)TableView.RowHeightProperty.DefaultValue;
//        public bool HasUnevenRows { get; set; } = (bool)TableView.HasUnevenRowsProperty.DefaultValue;


//        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
//        {
//            if (childControl is TableRoot root)
//                NativeControl.Root = root;
//            else
//            {
//                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
//            }

//            base.OnAddChild(widget, childControl);
//        }

//        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
//        {
//            if (childControl is TableRoot _)
//                NativeControl.Root = null;
//            else
//            {
//                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
//            }

//            base.OnRemoveChild(widget, childControl);
//        }


//        public IEnumerator<RxTableRoot> GetEnumerator()
//        {
//            return _children.GetEnumerator();
//        }

//        protected override void OnUpdate()
//        {
//            NativeControl.RowHeight = RowHeight;
//            NativeControl.HasUnevenRows = HasUnevenRows;

//            base.OnUpdate();
//        }

//        protected override IEnumerable<VisualNode> RenderChildren()
//        {
//            return _children;
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }

//    public class RxTableView : RxTableView<TableView>
//    {
//        public RxTableView()
//        {
//        }

//        public RxTableView(Action<TableView> componentRefAction)
//            : base(componentRefAction)
//        {
//        }
//    }

//    public static class RxTableViewExtensions
//    {
//        public static T RowHeight<T>(this T tableview, int rowHeight) where T : IRxTableView
//        {
//            tableview.RowHeight = rowHeight;
//            return tableview;
//        }

//        public static T HasUnevenRows<T>(this T tableview, bool hasUnevenRows) where T : IRxTableView
//        {
//            tableview.HasUnevenRows = hasUnevenRows;
//            return tableview;
//        }
//    }
//}