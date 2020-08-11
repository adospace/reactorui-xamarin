using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
//using Xamarin.Forms.Shapes;

namespace XamarinReactorUI.Shapes
{
    //public interface IRxPolygon : IRxShape
    //{
    //    PointCollection Points { get; set; }
    //    FillRule FillRule { get; set; }
    //}

    //public class RxPolygon : RxShape<Polygon>, IRxPolygon
    //{
    //    public RxPolygon()
    //    {
    //    }

    //    public RxPolygon(Action<Polygon> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }

    //    public PointCollection Points { get; set; } = (PointCollection)Polygon.PointsProperty.DefaultValue;
    //    public FillRule FillRule { get; set; } = (FillRule)Polygon.FillRuleProperty.DefaultValue;

    //    protected override void OnUpdate()
    //    {
    //        NativeControl.Points = Points;
    //        NativeControl.FillRule = FillRule;

    //        base.OnUpdate();
    //    }

    //    protected override void OnAnimate()
    //    {
    //        NativeControl.Points = Points;
    //        NativeControl.FillRule = FillRule;

    //        base.OnAnimate();
    //    }

    //    protected override IEnumerable<VisualNode> RenderChildren()
    //    {
    //        yield break;
    //    }
    //}

    //public static class RxPolygonExtensions
    //{
    //    public static T Points<T>(this T polygon, PointCollection points) where T : IRxPolygon
    //    {
    //        polygon.Points = points;
    //        return polygon;
    //    }

    //    public static T Points<T>(this T polygon, params Point[] points) where T : IRxPolygon
    //    {
    //        var pointCollection = new PointCollection();
    //        points.ForEach(p => pointCollection.Add(p));
    //        polygon.Points = pointCollection;
    //        return polygon;
    //    }

    //    public static T FillRule<T>(this T polygon, FillRule fillRule) where T : IRxPolygon
    //    {
    //        polygon.FillRule = fillRule;
    //        return polygon;
    //    }
    //}
}