using System;
using System.Collections.Generic;
using Xamarin.Forms.Shapes;

namespace XamarinReactorUI.Shapes
{
    public interface IRxGeometry
    {
    }

    public abstract class RxGeometry<T> : VisualNode<T>, IRxGeometry where T : Geometry, new()
    {
        public RxGeometry()
        {
        }

        public RxGeometry(Action<T> componentRefAction)
            : base(componentRefAction)
        {
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxGeometryExtensions
    {
    }
}