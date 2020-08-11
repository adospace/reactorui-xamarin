using System;
using System.Collections.Generic;
using Xamarin.Forms;
//using Xamarin.Forms.Shapes;
using XamarinReactorUI.Animations;
using XamarinReactorUI.Shapes;

namespace XamarinReactorUI.Shapes
{
    //public interface IRxEllipseGeometry : IVisualNode
    //{
    //    Point CenterPosition { get; set; }
    //    double RadiusX { get; set; }
    //    double RadiusY { get; set; }
    //}

    //public class RxEllipseGeometry<T> : RxGeometry<T>, IRxEllipseGeometry where T : EllipseGeometry, new()
    //{
    //    public RxEllipseGeometry()
    //    {
    //    }

    //    public RxEllipseGeometry(Action<T> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }

    //    public Point CenterPosition { get; set; } = (Point)EllipseGeometry.CenterProperty.DefaultValue;
    //    public double RadiusX { get; set; } = (double)EllipseGeometry.RadiusXProperty.DefaultValue;
    //    public double RadiusY { get; set; } = (double)EllipseGeometry.RadiusYProperty.DefaultValue;

    //    protected override void OnUpdate()
    //    {
    //        NativeControl.Center = CenterPosition;
    //        NativeControl.RadiusX = RadiusX;
    //        NativeControl.RadiusY = RadiusY;

    //        base.OnUpdate();
    //    }

    //    protected override void OnAnimate()
    //    {
    //        //System.Diagnostics.Debug.WriteLine($"RxEllipseGeometry()=>RadiusX={RadiusX} RadiusY={RadiusY}");

    //        NativeControl.Center = CenterPosition;
    //        NativeControl.RadiusX = RadiusX;
    //        NativeControl.RadiusY = RadiusY;

    //        base.OnAnimate();
    //    }

    //    protected override IEnumerable<VisualNode> RenderChildren()
    //    {
    //        yield break;
    //    }
    //}

    //public class RxEllipseGeometry : RxEllipseGeometry<EllipseGeometry>
    //{
    //    public RxEllipseGeometry()
    //    {
    //    }

    //    public RxEllipseGeometry(Action<EllipseGeometry> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }
    //}

    //public static class RxEllipseGeometryExtensions
    //{
    //    public static T Center<T>(this T ellipsegeometry, Point center, RxPointAnimation customAnimation = null) where T : IRxEllipseGeometry
    //    {
    //        ellipsegeometry.CenterPosition = center;
    //        ellipsegeometry.AppendAnimatable(EllipseGeometry.CenterProperty, customAnimation ?? new RxSimplePointAnimation(center), v => ellipsegeometry.CenterPosition = v.CurrentValue());
    //        return ellipsegeometry;
    //    }

    //    public static T RadiusX<T>(this T ellipsegeometry, double radiusX, RxDoubleAnimation customAnimation = null) where T : IRxEllipseGeometry
    //    {
    //        ellipsegeometry.RadiusX = radiusX;
    //        ellipsegeometry.AppendAnimatable(EllipseGeometry.RadiusXProperty, customAnimation ?? new RxDoubleAnimation(radiusX), v => ellipsegeometry.RadiusX = v.CurrentValue());
    //        return ellipsegeometry;
    //    }

    //    public static T RadiusY<T>(this T ellipsegeometry, double radiusY, RxDoubleAnimation customAnimation = null) where T : IRxEllipseGeometry
    //    {
    //        ellipsegeometry.RadiusY = radiusY;
    //        ellipsegeometry.AppendAnimatable(EllipseGeometry.RadiusYProperty, customAnimation ?? new RxDoubleAnimation(radiusY), v => ellipsegeometry.RadiusY = v.CurrentValue());
    //        return ellipsegeometry;
    //    }
    //}
}