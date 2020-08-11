using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;
//using Xamarin.Forms.Shapes;

namespace XamarinReactorUI.Shapes
{
    //public interface IRxPathGeometry : IVisualNode
    //{
    //    PathFigureCollection Figures { get; set; }
    //    FillRule FillRule { get; set; }
    //}

    //public class RxPathGeometry : RxGeometry<PathGeometry>, IRxPathGeometry
    //{
    //    public RxPathGeometry()
    //    {
    //    }

    //    public RxPathGeometry(Action<PathGeometry> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }

    //    public PathFigureCollection Figures { get; set; } = (PathFigureCollection)PathGeometry.FiguresProperty.DefaultValue;
    //    public FillRule FillRule { get; set; } = (FillRule)PathGeometry.FillRuleProperty.DefaultValue;

    //    protected override void OnUpdate()
    //    {
    //        NativeControl.Figures = Figures;
    //        NativeControl.FillRule = FillRule;

    //        base.OnUpdate();
    //    }

    //    protected override void OnAnimate()
    //    {
    //        NativeControl.Figures = Figures;
    //        NativeControl.FillRule = FillRule;

    //        base.OnAnimate();
    //    }

    //    protected override IEnumerable<VisualNode> RenderChildren()
    //    {
    //        yield break;
    //    }
    //}

    //public static class RxPathGeometryExtensions
    //{
    //    public static T Figures<T>(this T pathgeometry, PathFigureCollection figures) where T : IRxPathGeometry
    //    {
    //        pathgeometry.Figures = figures;
    //        return pathgeometry;
    //    }

    //    public static T Figures<T>(this T pathgeometry, params PathFigure[] figures) where T : IRxPathGeometry
    //    {
    //        var figuresCollection = new PathFigureCollection();
    //        figures.ForEach(f => figuresCollection.Add(f));
    //        pathgeometry.Figures = figuresCollection;
    //        return pathgeometry;
    //    }

    //    public static T Figures<T>(this T pathgeometry, string data) where T : IRxPathGeometry
    //    {
    //        pathgeometry.Figures = (PathFigureCollection)new PathFigureCollectionConverter().ConvertFromInvariantString(data);
    //        return pathgeometry;
    //    }

    //    public static T FillRule<T>(this T pathgeometry, FillRule fillRule) where T : IRxPathGeometry
    //    {
    //        pathgeometry.FillRule = fillRule;
    //        return pathgeometry;
    //    }
    //}
}