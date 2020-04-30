using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTableSection
    {
        string Title { get; set; }
    }

    public class RxTableSection : RxTableSectionBase<TableSection, Cell>, IRxTableSection
    {
        private TableSection _nativeControl;
        private readonly Action<TableSection> _componentRefAction;

        public RxTableSection()
        {
        }

        public RxTableSection(Action<TableSection> componentRefAction)
        {
            _componentRefAction = componentRefAction;
        }

        protected override void OnMount()
        {
            _nativeControl = _nativeControl ?? new TableSection();
            //Parent.AddChild(this, _nativeControl);
            _componentRefAction?.Invoke(_nativeControl);

            base.OnMount();
        }

        protected override void OnUnmount()
        {
            base.OnUnmount();
        }

        protected override void OnUpdate()
        {
            _nativeControl.Title = Title;
            _nativeControl.TextColor = TextColor;

            base.OnUpdate();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
        }
    }

    public static class RxTableSectionExtensions
    {
    }
}