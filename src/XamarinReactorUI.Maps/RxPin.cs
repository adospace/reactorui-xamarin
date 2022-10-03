using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamarinReactorUI.Maps
{
    public interface IRxPin
    {
        PinType Type { get; set; }
        Position Position { get; set; }
        string Address { get; set; }
        string Label { get; set; }


        Action MarkerClickedAction { get; set; }
        Action<object, PinClickedEventArgs> MarkerClickedActionWithArgs { get; set; }
        Action InfoWindowClickedAction { get; set; }
        Action<object, PinClickedEventArgs> InfoWindowClickedActionWithArgs { get; set; }
    }

    public abstract class RxPin<T> : RxElement<T>, IRxPin where T : Pin, new()
    {
        public RxPin()
        {

        }

        public RxPin(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }


        public PinType Type { get; set; } = (PinType)Pin.TypeProperty.DefaultValue;
        public Position Position { get; set; } = (Position)Pin.PositionProperty.DefaultValue;
        public string Address { get; set; } = (string)Pin.AddressProperty.DefaultValue;
        public string Label { get; set; } = (string)Pin.LabelProperty.DefaultValue;

        public Action MarkerClickedAction { get; set; }
        public Action<object, PinClickedEventArgs> MarkerClickedActionWithArgs { get; set; }

        public Action InfoWindowClickedAction { get; set; }
        public Action<object, PinClickedEventArgs> InfoWindowClickedActionWithArgs { get; set; }

        protected override void OnMount()
        {
            _nativeControl = _nativeControl ?? new T();

            UpdateCore();

            base.OnMount();
        }

        protected override void OnUpdate()
        {
            UpdateCore();

            if (MarkerClickedAction != null || MarkerClickedActionWithArgs != null)
                NativeControl.MarkerClicked += NativeControl_MarkerClicked;
            if (InfoWindowClickedAction != null || InfoWindowClickedActionWithArgs != null)
                NativeControl.InfoWindowClicked += NativeControl_InfoWindowClicked;

            base.OnUpdate();
        }

        private void NativeControl_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            MarkerClickedAction?.Invoke();
            MarkerClickedActionWithArgs?.Invoke(sender, e);
        }

        private void NativeControl_InfoWindowClicked(object sender, PinClickedEventArgs e)
        {
            InfoWindowClickedAction?.Invoke();
            InfoWindowClickedActionWithArgs?.Invoke(sender, e);
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                NativeControl.MarkerClicked -= NativeControl_MarkerClicked;
                NativeControl.InfoWindowClicked -= NativeControl_InfoWindowClicked;
            }

            base.OnMigrated(newNode);
        }

        private void UpdateCore()
        { 
            NativeControl.Type = Type;
            NativeControl.Position = Position;
            NativeControl.Address = Address;
            NativeControl.Label = Label;
        }



        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxPin : RxPin<Pin>
    {
        public RxPin() { }

        public RxPin(Action<Pin> componentRefAction)
            : base(componentRefAction)
        {

        }
    }


    public static class RxPinExtensions
    {
        public static T Type<T>(this T pin, PinType type) where T : IRxPin
        {
            pin.Type = type;
            return pin;
        }



        public static T Position<T>(this T pin, Position position) where T : IRxPin
        {
            pin.Position = position;
            return pin;
        }



        public static T Address<T>(this T pin, string address) where T : IRxPin
        {
            pin.Address = address;
            return pin;
        }



        public static T Label<T>(this T pin, string label) where T : IRxPin
        {
            pin.Label = label;
            return pin;
        }



        public static T OnMarkerClicked<T>(this T pin, Action action) where T : IRxPin
        {
            pin.MarkerClickedAction = action;
            return pin;
        }

        public static T OnMarkerClicked<T>(this T pin, Action<object, PinClickedEventArgs> action) where T : IRxPin
        {
            pin.MarkerClickedActionWithArgs = action;
            return pin;
        }

        public static T OnInfoWindowClicked<T>(this T pin, Action action) where T : IRxPin
        {
            pin.InfoWindowClickedAction = action;
            return pin;
        }

        public static T OnInfoWindowClicked<T>(this T pin, Action<object, PinClickedEventArgs> action) where T : IRxPin
        {
            pin.InfoWindowClickedActionWithArgs = action;
            return pin;
        }
    }

}
