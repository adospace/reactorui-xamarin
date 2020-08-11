using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
//using Xamarin.Forms.Shapes;
//using XamarinReactorUI.Shapes;

namespace XamarinReactorUI.Shapes
{
    //public interface IRxGeometryGroup
    //{
    //    FillRule FillRule { get; set; }
    //}

    //public class RxGeometryGroup : RxGeometry<GeometryGroup>, IRxGeometryGroup, IEnumerable<IRxGeometry>
    //{
    //    private readonly List<IRxGeometry> _internalChildren = new List<IRxGeometry>();

    //    public RxGeometryGroup()
    //    {
    //    }

    //    public RxGeometryGroup(Action<GeometryGroup> componentRefAction)
    //        : base(componentRefAction)
    //    {
    //    }

    //    public FillRule FillRule { get; set; } = (FillRule)GeometryGroup.FillRuleProperty.DefaultValue;

    //    protected override void OnUpdate()
    //    {
    //        NativeControl.FillRule = FillRule;

    //        base.OnUpdate();
    //    }

    //    public void Add(IRxGeometry child)
    //    {
    //        _internalChildren.Add(child);
    //    }

    //    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    //    {
    //        if (childControl is Geometry view)
    //        {
    //            //System.Diagnostics.Debug.WriteLine($"StackLayout ({Key ?? GetType()}) inserting {widget.Key ?? widget.GetType()} at index {widget.ChildIndex}");
    //            NativeControl.Children.Insert(widget.ChildIndex, view);
    //        }
    //        else
    //        {
    //            throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
    //        }

    //        base.OnAddChild(widget, childControl);
    //    }

    //    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    //    {
    //        NativeControl.Children.Remove((Geometry)childControl);

    //        base.OnRemoveChild(widget, childControl);
    //    }

    //    protected override IEnumerable<VisualNode> RenderChildren()
    //    {
    //        return _internalChildren.Cast<VisualNode>();
    //    }

    //    public IEnumerator<IRxGeometry> GetEnumerator()
    //    {
    //        return _internalChildren.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return _internalChildren.GetEnumerator();
    //    }
    //}

    //public static class RxGeometryGroupExtensions
    //{
    //    public static T FillRule<T>(this T geometrygroup, FillRule fillRule) where T : IRxGeometryGroup
    //    {
    //        geometrygroup.FillRule = fillRule;
    //        return geometrygroup;
    //    }
    //}
}