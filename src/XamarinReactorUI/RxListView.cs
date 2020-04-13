using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxListView
    {
        IEnumerable ItemsSource { get; set; }
    }

    public class RxListView<T> : RxView<T>, IRxListView where T : ListView, new()
    {
        public RxListView(IEnumerable itemsSource = null)
        {
            ItemsSource = itemsSource;
        }
        
        public RxListView(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        private readonly NullableField<IEnumerable> _itemsSource = new NullableField<IEnumerable>();
        public IEnumerable ItemsSource { get => _itemsSource.GetValueOrDefault(); set => _itemsSource.Value = value; }

        protected override void OnUpdate()
        {
            if (_itemsSource.HasValue) NativeControl.ItemsSource = _itemsSource.Value;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxListView : RxListView<ListView>
    {
        public RxListView(IEnumerable itemsSource = null)
            : base(itemsSource)
        {
        }

        public RxListView(Action<ListView> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxListViewExtensions
    {
        public static T ItemsSource<T>(this T listView, IEnumerable itemsSource) where  T : ListView
        {
            listView.ItemsSource = itemsSource;
            return listView;
        }
    }
}
