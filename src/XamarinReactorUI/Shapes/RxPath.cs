using System;
using System.Collections.Generic;
using Xamarin.Forms;
//using Xamarin.Forms.Shapes;
using XamarinReactorUI.Animations;

namespace XamarinReactorUI.Shapes
{
    //public interface IRxPath : IRxShape
    //{
    //    Geometry Data { get; set; }
    //    Transform RenderTransform { get; set; }
    //}

    //public class RxPath<T> : RxShape<T>, IRxPath where T : Path, new()
    //{
    //    public RxPath()
    //    {
    //    }

    //    public RxPath(Action<T> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }

    //    public Geometry Data { get; set; } = (Geometry)Path.DataProperty.DefaultValue;
    //    public Transform RenderTransform { get; set; } = (Transform)Path.RenderTransformProperty.DefaultValue;

    //    protected override void OnUpdate()
    //    {
    //        NativeControl.Data = Data;
    //        NativeControl.RenderTransform = RenderTransform;

    //        base.OnUpdate();
    //    }

    //    protected override void OnAnimate()
    //    {
    //        NativeControl.Data = Data;
    //        NativeControl.RenderTransform = RenderTransform;

    //        base.OnAnimate();
    //    }

    //    protected override IEnumerable<VisualNode> RenderChildren()
    //    {
    //        yield break;
    //    }
    //}

    //public class RxPath : RxPath<Path>
    //{
    //    public RxPath()
    //    {
    //    }

    //    public RxPath(Action<Path> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }
    //}

    //public static class RxPathExtensions
    //{
    //    public static T Data<T>(this T path, Geometry data) where T : IRxPath
    //    {
    //        path.Data = data;
    //        return path;
    //    }

    //    public static T Data<T>(this T path, string data) where T : IRxPath
    //    {
    //        path.Data = (Geometry)new PathGeometryConverter().ConvertFromInvariantString(data);
    //        return path;
    //    }

    //    public static T RenderTransform<T>(this T path, Transform renderTransform) where T : IRxPath
    //    {
    //        path.RenderTransform = renderTransform;
    //        return path;
    //    }
    //}
}