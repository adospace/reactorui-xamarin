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

    public class RxListView : RxView<ListView>, IRxListView
    {
        public RxListView(IEnumerable itemsSource = null)
        {
            ItemsSource = itemsSource;
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

    public static class RxListViewExtensions
    {
        public static T ItemsSource<T>(this T listView, IEnumerable itemsSource) where  T : ListView
        {
            listView.ItemsSource = itemsSource;
            return listView;
        }
    }
}
