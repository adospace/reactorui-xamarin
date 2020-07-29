using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using XamarinReactorUI.Animations;

namespace XamarinReactorUI.Shapes
{
    public interface IRxLineGeometry : IVisualNode
    {
        Point StartPoint { get; set; }
        Point EndPoint { get; set; }
    }

    public class RxLineGeometry<T> : RxGeometry<T>, IRxLineGeometry where T : LineGeometry, new()
    {
        public RxLineGeometry()
        {
        }

        public RxLineGeometry(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        public Point StartPoint { get; set; } = (Point)LineGeometry.StartPointProperty.DefaultValue;
        public Point EndPoint { get; set; } = (Point)LineGeometry.EndPointProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.StartPoint = StartPoint;
            NativeControl.EndPoint = EndPoint;

            base.OnUpdate();
        }

        protected override void OnAnimate()
        {
            NativeControl.StartPoint = StartPoint;
            NativeControl.EndPoint = EndPoint;

            base.OnAnimate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public class RxLineGeometry : RxLineGeometry<LineGeometry>
    {
        public RxLineGeometry()
        {
        }

        public RxLineGeometry(Action<LineGeometry> componentRefAction)
            : base(componentRefAction)
        {
        }
    }

    public static class RxLineGeometryExtensions
    {
        public static T StartPoint<T>(this T linegeometry, Point startPoint, RxPointAnimation customAnimation = null) where T : IRxLineGeometry
        {
            linegeometry.StartPoint = startPoint;
            linegeometry.AppendAnimatable(LineGeometry.StartPointProperty, customAnimation ?? new RxSimplePointAnimation(startPoint), v => linegeometry.StartPoint = v.CurrentValue());
            return linegeometry;
        }

        public static T EndPoint<T>(this T linegeometry, Point endPoint, RxPointAnimation customAnimation = null) where T : IRxLineGeometry
        {
            linegeometry.EndPoint = endPoint;
            linegeometry.AppendAnimatable(LineGeometry.EndPointProperty, customAnimation ?? new RxSimplePointAnimation(endPoint), v => linegeometry.EndPoint = v.CurrentValue());
            return linegeometry;
        }
    }
}