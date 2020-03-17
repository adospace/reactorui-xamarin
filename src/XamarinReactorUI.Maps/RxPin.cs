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
    }

    public class RxPin : RxElement<Pin>, IRxPin
    {
        public RxPin()
        {

        }

        public RxPin(Action<Pin> componentRefAction)
            : base(componentRefAction)
        {

        }


        public PinType Type { get; set; } = (PinType)Pin.TypeProperty.DefaultValue;
        public Position Position { get; set; } = (Position)Pin.PositionProperty.DefaultValue;
        public string Address { get; set; } = (string)Pin.AddressProperty.DefaultValue;
        public string Label { get; set; } = (string)Pin.LabelProperty.DefaultValue;

        protected override void OnMount()
        {
            _nativeControl = _nativeControl ?? new Pin();

            UpdateCore();

            base.OnMount();
        }

        protected override void OnUpdate()
        {
            UpdateCore();

            base.OnUpdate();
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



    }

}
