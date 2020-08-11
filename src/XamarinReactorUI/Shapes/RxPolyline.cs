using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
//using Xamarin.Forms.Shapes;

namespace XamarinReactorUI.Shapes
{
//    public interface IRxPolyline : IRxShape
//    {
//        PointCollection Points { get; set; }
//        FillRule FillRule { get; set; }
//    }

//    public class RxPolyline : RxShape<Polyline>, IRxPolyline
//    {
//        public RxPolyline()
//        {
//        }

//        public RxPolyline(Action<Polyline> componentRefAction)
//            : base(componentRefAction)
//        {
//        }

//        public PointCollection Points { get; set; } = (PointCollection)Polyline.PointsProperty.DefaultValue;
//        public FillRule FillRule { get; set; } = (FillRule)Polyline.FillRuleProperty.DefaultValue;

//        protected override void OnUpdate()
//        {
//            NativeControl.Points = Points;
//            NativeControl.FillRule = FillRule;

//            base.OnUpdate();
//        }

//        protected override void OnAnimate()
//        {
//            NativeControl.Points = Points;
//            NativeControl.FillRule = FillRule;

//            base.OnAnimate();
//        }

//        protected override IEnumerable<VisualNode> RenderChildren()
//        {
//            yield break;
//        }
//    }

//    public static class RxPolylineExtensions
//    {
//        public static T Points<T>(this T polyline, PointCollection points) where T : IRxPolyline
//        {
//            polyline.Points = points;
//            return polyline;
//        }

//        public static T Points<T>(this T polygon, params Point[] points) where T : IRxPolygon
//        {
//            var pointCollection = new PointCollection();
//            points.ForEach(p => pointCollection.Add(p));
//            polygon.Points = pointCollection;
//            return polygon;
//        }

//        public static T FillRule<T>(this T polyline, FillRule fillRule) where T : IRxPolyline
//        {
//            polyline.FillRule = fillRule;
//            return polyline;
//        }
//    }
}