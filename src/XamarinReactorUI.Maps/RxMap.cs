using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamarinReactorUI.Maps
{
    public interface IRxMap
    {
        MapType MapType { get; set; }
        bool IsShowingUser { get; set; }
        bool HasScrollEnabled { get; set; }
        bool HasZoomEnabled { get; set; }
        bool MoveToLastRegionOnLayoutChange { get; set; }
        Action<object, MapClickedEventArgs> ClickedAction { get; set; }
    }

    public class RxMap : RxView<Map>, IRxMap, IEnumerable<RxPin>//, IEnumerable<VisualNode>
    {
        private readonly List<RxPin> _internalChildren = new List<RxPin>();
        //private readonly List<VisualNode> _layoutChildren = new List<VisualNode>();

        public RxMap()
        {
        }

        public RxMap(IEnumerable<RxPin> nodes)
        {
            Add(nodes);
        }

        public RxMap(Action<Map> componentRefAction)
            : base(componentRefAction)
        {
        }

        public IEnumerator<RxPin> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(RxPin node)
        {
            _internalChildren.Add(node);
        }

        public void Add(IEnumerable<RxPin> nodes)
        {
            _internalChildren.AddRange(nodes);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }

        protected override void OnAddChild(VisualNode widget, Element childNativeControl)
        {
            if (childNativeControl is Pin pin)
            {
                NativeControl.Pins.Insert(widget.ChildIndex, pin);
            }
            else
            {
                throw new InvalidOperationException($"Type '{childNativeControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childNativeControl);
        }

        protected override void OnRemoveChild(VisualNode widget, Element childNativeControl)
        {
            if (childNativeControl is Pin pin)
            {
                NativeControl.Pins.Remove(pin);
            }

            base.OnRemoveChild(widget, childNativeControl);
        }

        public MapType MapType { get; set; } = (MapType)Map.MapTypeProperty.DefaultValue;
        public bool IsShowingUser { get; set; } = (bool)Map.IsShowingUserProperty.DefaultValue;
        public bool HasScrollEnabled { get; set; } = (bool)Map.HasScrollEnabledProperty.DefaultValue;
        public bool HasZoomEnabled { get; set; } = (bool)Map.HasZoomEnabledProperty.DefaultValue;
        public bool MoveToLastRegionOnLayoutChange { get; set; } = (bool)Map.MoveToLastRegionOnLayoutChangeProperty.DefaultValue;
        public Action<object, MapClickedEventArgs> ClickedAction { get; set; }

        protected override void OnUpdate()
        {
            NativeControl.MapType = MapType;
            NativeControl.IsShowingUser = IsShowingUser;
            NativeControl.HasScrollEnabled = HasScrollEnabled;
            NativeControl.HasZoomEnabled = HasZoomEnabled;
            NativeControl.MoveToLastRegionOnLayoutChange = MoveToLastRegionOnLayoutChange;

            if (ClickedAction != null)
                NativeControl.MapClicked += NativeControl_MapClicked;

            base.OnUpdate();
        }

        private void NativeControl_MapClicked(object sender, MapClickedEventArgs e)
        {
            ClickedAction?.Invoke(sender, e);
        }

        protected override void OnMigrated()
        {
            NativeControl.MapClicked -= NativeControl_MapClicked;
            base.OnMigrated();
        }
    }

    public static class RxMapExtensions
    {
        public static T OnClicked<T>(this T map, Action<object, MapClickedEventArgs> action) where T : IRxMap
        {
            map.ClickedAction = action;
            return map;
        }

        public static T MapType<T>(this T map, MapType mapType) where T : IRxMap
        {
            map.MapType = mapType;
            return map;
        }

        public static T IsShowingUser<T>(this T map, bool isShowingUser) where T : IRxMap
        {
            map.IsShowingUser = isShowingUser;
            return map;
        }

        public static T HasScrollEnabled<T>(this T map, bool hasScrollEnabled) where T : IRxMap
        {
            map.HasScrollEnabled = hasScrollEnabled;
            return map;
        }

        public static T HasZoomEnabled<T>(this T map, bool hasZoomEnabled) where T : IRxMap
        {
            map.HasZoomEnabled = hasZoomEnabled;
            return map;
        }

        //public static T ItemsSource<T>(this T map, IEnumerable itemsSource) where T : IRxMap
        //{
        //    map.ItemsSource = itemsSource;
        //    return map;
        //}

        //public static T ItemTemplate<T>(this T map, DataTemplate itemTemplate) where T : IRxMap
        //{
        //    map.ItemTemplate = itemTemplate;
        //    return map;
        //}

        //public static T ItemTemplateSelector<T>(this T map, DataTemplateSelector itemTemplateSelector) where T : IRxMap
        //{
        //    map.ItemTemplateSelector = itemTemplateSelector;
        //    return map;
        //}

        public static T MoveToLastRegionOnLayoutChange<T>(this T map, bool moveToLastRegionOnLayoutChange) where T : IRxMap
        {
            map.MoveToLastRegionOnLayoutChange = moveToLastRegionOnLayoutChange;
            return map;
        }
    }
}