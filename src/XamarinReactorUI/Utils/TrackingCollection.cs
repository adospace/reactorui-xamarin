using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamarinReactorUI.Utils
{
    public class TrackingCollection<T> : Collection<T>
    {
        private enum TrackingOperation
        {
            Insert,

            Remove,

            Replace,

            Clear
        }

        private class TrackedItem
        {
            public int Index { get; }
            public TrackingOperation Operation { get; }
            public T[] Items { get; }
            public T NewItem { get; }

            private TrackedItem(TrackingOperation operation, T[] items, int index)
            {
                Operation = operation;
                Items = items;
                Index = index;
            }

            private TrackedItem(TrackingOperation operation, T item, int index)
            {
                Operation = operation;
                Items = new[] { item };
                Index = index;
            }

            private TrackedItem(TrackingOperation operation, T oldItem, T newItem, int index)
            {
                Operation = operation;
                Items = new[] { oldItem };
                NewItem = newItem;
                Index = index;
            }

            private TrackedItem(TrackingOperation operation)
            {
                Operation = operation;
            }

            public static TrackedItem Clear() => new TrackedItem(TrackingOperation.Clear);

            public static TrackedItem Insert(T item, int index) => new TrackedItem(TrackingOperation.Insert, item, index);

            public static TrackedItem Insert(T[] items, int index) => new TrackedItem(TrackingOperation.Insert, items, index);

            public static TrackedItem Remove(T item, int index) => new TrackedItem(TrackingOperation.Remove, item, index);

            public static TrackedItem Replace(T oldItem, T newItem, int index) => new TrackedItem(TrackingOperation.Replace, oldItem, newItem, index);
        }

        private readonly LinkedList<TrackedItem> _trackedItems = new LinkedList<TrackedItem>();

        private bool _suspendTracking = false;

        public TrackingCollection()
        { }

        public TrackingCollection(IList<T> items)
            : base(items)
        { }

        protected override void InsertItem(int index, T item)
        {
            if (!_suspendTracking)
                _trackedItems.AddLast(new LinkedListNode<TrackedItem>(TrackedItem.Insert(item, index)));
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            if (!_suspendTracking)
                _trackedItems.AddLast(new LinkedListNode<TrackedItem>(TrackedItem.Remove(Items[index], index)));
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            if (!_suspendTracking)
                _trackedItems.AddLast(new LinkedListNode<TrackedItem>(TrackedItem.Replace(Items[index], item, index)));
            base.SetItem(index, item);
        }

        protected override void ClearItems()
        {
            if (!_suspendTracking)
                _trackedItems.AddLast(new LinkedListNode<TrackedItem>(TrackedItem.Clear()));
            base.ClearItems();
        }

        public void AddRange(IEnumerable<T> items)
        {
            _suspendTracking = true;
            _trackedItems.AddLast(new LinkedListNode<TrackedItem>(TrackedItem.Insert(items.ToArray(), Count)));
            foreach (var item in items)
                Items.Add(item);
            _suspendTracking = false;
        }

        public void Reset()
        {
            _trackedItems.Clear();
        }

        
    }
}