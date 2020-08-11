using System;
using System.Collections.Generic;
using Xamarin.Forms;
//using Xamarin.Forms.Shapes;
using XamarinReactorUI.Animations;

namespace XamarinReactorUI.Shapes
{
    //public interface IRxLine : IRxShape
    //{
    //    double X1 { get; set; }
    //    double Y1 { get; set; }
    //    double X2 { get; set; }
    //    double Y2 { get; set; }
    //}

    //public class RxLine : RxShape<Line>, IRxLine
    //{
    //    public RxLine()
    //    {
    //    }

    //    public RxLine(Action<Line> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }

    //    public double X1 { get; set; } = (double)Line.X1Property.DefaultValue;
    //    public double Y1 { get; set; } = (double)Line.Y1Property.DefaultValue;
    //    public double X2 { get; set; } = (double)Line.X2Property.DefaultValue;
    //    public double Y2 { get; set; } = (double)Line.Y2Property.DefaultValue;

    //    protected override void OnUpdate()
    //    {
    //        NativeControl.X1 = X1;
    //        NativeControl.Y1 = Y1;
    //        NativeControl.X2 = X2;
    //        NativeControl.Y2 = Y2;

    //        base.OnUpdate();
    //    }

    //    protected override void OnAnimate()
    //    {
    //        NativeControl.X1 = X1;
    //        NativeControl.Y1 = Y1;
    //        NativeControl.X2 = X2;
    //        NativeControl.Y2 = Y2;

    //        base.OnAnimate();
    //    }

    //    protected override IEnumerable<VisualNode> RenderChildren()
    //    {
    //        yield break;
    //    }
    //}

    //public static class RxLineExtensions
    //{
    //    public static T X1<T>(this T line, double x1, RxDoubleAnimation customAnimation = null) where T : IRxLine
    //    {
    //        line.X1 = x1;
    //        line.AppendAnimatable(Line.X1Property, customAnimation ?? new RxDoubleAnimation(x1), v => line.X1 = v.CurrentValue());
    //        return line;
    //    }

    //    public static T Y1<T>(this T line, double y1, RxDoubleAnimation customAnimation = null) where T : IRxLine
    //    {
    //        line.Y1 = y1;
    //        line.AppendAnimatable(Line.Y1Property, customAnimation ?? new RxDoubleAnimation(y1), v => line.Y1 = v.CurrentValue());
    //        return line;
    //    }

    //    public static T X2<T>(this T line, double x2, RxDoubleAnimation customAnimation = null) where T : IRxLine
    //    {
    //        line.X2 = x2;
    //        line.AppendAnimatable(Line.X2Property, customAnimation ?? new RxDoubleAnimation(x2), v => line.X2 = v.CurrentValue());
    //        return line;
    //    }

    //    public static T Y2<T>(this T line, double y2, RxDoubleAnimation customAnimation = null) where T : IRxLine
    //    {
    //        line.Y2 = y2;
    //        line.AppendAnimatable(Line.Y2Property, customAnimation ?? new RxDoubleAnimation(y2), v => line.Y2 = v.CurrentValue());
    //        return line;
    //    }

    //    public static T StartPoint<T>(this T linegeometry, Point startPoint, RxPointAnimation customAnimation = null) where T : IRxLine
    //    {
    //        linegeometry.X1 = startPoint.X;
    //        linegeometry.Y1 = startPoint.Y;
    //        linegeometry.AppendAnimatable(LineGeometry.StartPointProperty, customAnimation ?? new RxSimplePointAnimation(startPoint), v =>
    //        {
    //            var currentPoint = v.CurrentValue();
    //            linegeometry.X1 = currentPoint.X;
    //            linegeometry.Y1 = currentPoint.Y;
    //        });
    //        return linegeometry;
    //    }

    //    public static T EndPoint<T>(this T linegeometry, Point endPoint, RxPointAnimation customAnimation = null) where T : IRxLine
    //    {
    //        linegeometry.X2 = endPoint.X;
    //        linegeometry.Y2 = endPoint.Y;
    //        linegeometry.AppendAnimatable(LineGeometry.EndPointProperty, customAnimation ?? new RxSimplePointAnimation(endPoint), v =>
    //        {
    //            var currentPoint = v.CurrentValue();
    //            linegeometry.X2 = currentPoint.X;
    //            linegeometry.Y2 = currentPoint.Y;
    //        });
    //        return linegeometry;
    //    }
    //}
}