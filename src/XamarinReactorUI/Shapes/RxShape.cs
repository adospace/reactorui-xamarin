using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using XamarinReactorUI.Animations;

namespace XamarinReactorUI.Shapes
{
    public interface IRxShape : IVisualNode
    {
        Brush Fill { get; set; }
        Brush Stroke { get; set; }
        double StrokeThickness { get; set; }
        DoubleCollection StrokeDashArray { get; set; }
        double StrokeDashOffset { get; set; }
        PenLineCap StrokeLineCap { get; set; }
        PenLineJoin StrokeLineJoin { get; set; }
        Stretch Aspect { get; set; }
    }

    public abstract class RxShape<T> : RxView<T>, IRxShape where T : Shape, new()
    {
        public RxShape()
        {
        }

        public RxShape(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Brush Fill { get; set; } = (Brush)Shape.FillProperty.DefaultValue;
        public Brush Stroke { get; set; } = (Brush)Shape.StrokeProperty.DefaultValue;
        public double StrokeThickness { get; set; } = (double)Shape.StrokeThicknessProperty.DefaultValue;
        public DoubleCollection StrokeDashArray { get; set; } = (DoubleCollection)Shape.StrokeDashArrayProperty.DefaultValue;
        public double StrokeDashOffset { get; set; } = (double)Shape.StrokeDashOffsetProperty.DefaultValue;
        public PenLineCap StrokeLineCap { get; set; } = (PenLineCap)Shape.StrokeLineCapProperty.DefaultValue;
        public PenLineJoin StrokeLineJoin { get; set; } = (PenLineJoin)Shape.StrokeLineJoinProperty.DefaultValue;
        public Stretch Aspect { get; set; } = (Stretch)Shape.AspectProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Fill = new SolidColorBrush(Color.Red);
            //NativeControl.Fill = Fill;
            //NativeControl.Stroke = Stroke;
            //NativeControl.StrokeThickness = StrokeThickness;
            //NativeControl.StrokeDashArray = StrokeDashArray ?? new DoubleCollection();
            //NativeControl.StrokeDashOffset = StrokeDashOffset;
            //NativeControl.StrokeLineCap = StrokeLineCap;
            //NativeControl.StrokeLineJoin = StrokeLineJoin;
            //NativeControl.Aspect = Aspect;

            base.OnUpdate();
        }

        //protected override void OnAnimate()
        //{
        //    NativeControl.Fill = Fill;
        //    NativeControl.Stroke = Stroke;
        //    NativeControl.StrokeThickness = StrokeThickness;
        //    NativeControl.StrokeDashArray = StrokeDashArray;
        //    NativeControl.StrokeDashOffset = StrokeDashOffset;
        //    NativeControl.StrokeLineCap = StrokeLineCap;
        //    NativeControl.StrokeLineJoin = StrokeLineJoin;
        //    NativeControl.Aspect = Aspect;

        //    base.OnAnimate();
        //}
        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxShapeExtensions
    {
        public static T Fill<T>(this T shape, Color fill, RxColorAnimation customAnimation = null) where T : IRxShape
        {
            shape.Fill = new SolidColorBrush(fill);
            shape.AppendAnimatable(Shape.FillProperty, customAnimation ?? new RxSimpleColorAnimation(fill), v => shape.Fill = new SolidColorBrush(v.CurrentValue()));
            return shape;
        }

        public static T Stroke<T>(this T shape, Color stroke, RxColorAnimation customAnimation = null) where T : IRxShape
        {
            shape.Stroke = new SolidColorBrush(stroke);
            shape.AppendAnimatable(Shape.StrokeProperty, customAnimation ?? new RxSimpleColorAnimation(stroke), v => shape.Stroke = new SolidColorBrush(v.CurrentValue()));
            return shape;
        }

        public static T Fill<T>(this T shape, Brush fill) where T : IRxShape
        {
            shape.Fill = fill;
            return shape;
        }

        public static T Stroke<T>(this T shape, Brush stroke) where T : IRxShape
        {
            shape.Stroke = stroke;
            return shape;
        }

        public static T StrokeThickness<T>(this T shape, double strokeThickness, RxDoubleAnimation customAnimation = null) where T : IRxShape
        {
            shape.StrokeThickness = strokeThickness;
            shape.AppendAnimatable(Shape.StrokeThicknessProperty, customAnimation ?? new RxDoubleAnimation(strokeThickness), v => shape.StrokeThickness = v.CurrentValue());
            return shape;
        }

        public static T StrokeDashArray<T>(this T shape, DoubleCollection strokeDashArray) where T : IRxShape
        {
            shape.StrokeDashArray = strokeDashArray;
            return shape;
        }

        public static T StrokeDashOffset<T>(this T shape, double strokeDashOffset, RxDoubleAnimation customAnimation = null) where T : IRxShape
        {
            shape.StrokeDashOffset = strokeDashOffset;
            shape.AppendAnimatable(Shape.StrokeDashOffsetProperty, customAnimation ?? new RxDoubleAnimation(strokeDashOffset), v => shape.StrokeDashOffset = v.CurrentValue());
            return shape;
        }

        public static T StrokeLineCap<T>(this T shape, PenLineCap strokeLineCap) where T : IRxShape
        {
            shape.StrokeLineCap = strokeLineCap;
            return shape;
        }

        public static T StrokeLineJoin<T>(this T shape, PenLineJoin strokeLineJoin) where T : IRxShape
        {
            shape.StrokeLineJoin = strokeLineJoin;
            return shape;
        }

        public static T Aspect<T>(this T shape, Stretch aspect) where T : IRxShape
        {
            shape.Aspect = aspect;
            return shape;
        }
    }
}