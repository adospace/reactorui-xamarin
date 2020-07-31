using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinReactorUI.Animations;

namespace XamarinReactorUI.Shapes
{
    public interface IRxRectangle : IRxShape
    {
        double RadiusX { get; set; }
        double RadiusY { get; set; }
    }

    public class RxRectangle : RxShape<Xamarin.Forms.Shapes.Rectangle>, IRxRectangle
    {
        public RxRectangle()
        {
            Aspect = Stretch.Fill;
        }

        public RxRectangle(Action<Xamarin.Forms.Shapes.Rectangle> componentRefAction)
            : base(componentRefAction)
        {
            Aspect = Stretch.Fill;
        }

        public double RadiusX { get; set; } = (double)Xamarin.Forms.Shapes.Rectangle.RadiusXProperty.DefaultValue;
        public double RadiusY { get; set; } = (double)Xamarin.Forms.Shapes.Rectangle.RadiusYProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.RadiusX = RadiusX;
            NativeControl.RadiusY = RadiusY;

            base.OnUpdate();
        }

        protected override void OnAnimate()
        {
            NativeControl.RadiusX = RadiusX;
            NativeControl.RadiusY = RadiusY;

            base.OnAnimate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxRectangleExtensions
    {
        public static T RadiusX<T>(this T rectangle, double radiusX, RxDoubleAnimation customAnimation = null) where T : IRxRectangle
        {
            rectangle.RadiusX = radiusX;
            rectangle.AppendAnimatable(Xamarin.Forms.Shapes.Rectangle.RadiusXProperty, customAnimation ?? new RxDoubleAnimation(radiusX), v => rectangle.RadiusX = v.CurrentValue());
            return rectangle;
        }

        public static T RadiusY<T>(this T rectangle, double radiusY, RxDoubleAnimation customAnimation = null) where T : IRxRectangle
        {
            rectangle.RadiusY = radiusY;
            rectangle.AppendAnimatable(Xamarin.Forms.Shapes.Rectangle.RadiusYProperty, customAnimation ?? new RxDoubleAnimation(radiusY), v => rectangle.RadiusY = v.CurrentValue());
            return rectangle;
        }
    }
}