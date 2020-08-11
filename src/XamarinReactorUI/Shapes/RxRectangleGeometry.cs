using System;
using System.Collections.Generic;
using Xamarin.Forms;
//using Xamarin.Forms.Shapes;

namespace XamarinReactorUI.Shapes
{
    //public interface IRxRectangleGeometry : IVisualNode
    //{
    //    Xamarin.Forms.Rectangle Rect { get; set; }
    //}

    //public class RxRectangleGeometry<T> : RxGeometry<T>, IRxRectangleGeometry where T : RectangleGeometry, new()
    //{
    //    public RxRectangleGeometry()
    //    {
    //    }

    //    public RxRectangleGeometry(Action<T> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }

    //    public Xamarin.Forms.Rectangle Rect { get; set; } = (Xamarin.Forms.Rectangle)RectangleGeometry.RectProperty.DefaultValue;

    //    protected override void OnUpdate()
    //    {
    //        NativeControl.Rect = Rect;

    //        base.OnUpdate();
    //    }

    //    protected override void OnAnimate()
    //    {
    //        NativeControl.Rect = Rect;

    //        base.OnAnimate();
    //    }

    //    protected override IEnumerable<VisualNode> RenderChildren()
    //    {
    //        yield break;
    //    }
    //}

    //public class RxRectangleGeometry : RxRectangleGeometry<RectangleGeometry>
    //{
    //    public RxRectangleGeometry()
    //    {
    //    }

    //    public RxRectangleGeometry(Action<RectangleGeometry> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }
    //}

    //public static class RxRectangleGeometryExtensions
    //{
    //    public static T Rect<T>(this T rectanglegeometry, Xamarin.Forms.Rectangle rect) where T : IRxRectangleGeometry
    //    {
    //        rectanglegeometry.Rect = rect;
    //        return rectanglegeometry;
    //    }
    //}
}