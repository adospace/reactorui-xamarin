using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTableSectionBase
    {
        string Title { get; set; }
        Color TextColor { get; set; }
    }

    public abstract class RxTableSectionBase<T> : VisualNode<T>, IRxTableSectionBase, IEnumerable<VisualNode> where T : TableSectionBase, new()
    {
        private List<VisualNode> _children = new List<VisualNode>();

        public RxTableSectionBase()
        {
        }

        public RxTableSectionBase(Action<T> componentRefAction)
            : base(componentRefAction)
        {

        }

        public string Title { get; set; } = (string)TableSectionBase.TitleProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)TableSectionBase.TextColorProperty.DefaultValue;

        protected override void OnUpdate()
        {
            NativeControl.Title = Title;
            NativeControl.TextColor = TextColor;

            base.OnUpdate();
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _children;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public void Add(VisualNode child)
        {
            _children.Add(child);
        }
    }

    public static class RxTableSectionBaseExtensions
    {
        public static T Title<T>(this T tablesectionbase, string title) where T : IRxTableSectionBase
        {
            tablesectionbase.Title = title;
            return tablesectionbase;
        }

        public static T TextColor<T>(this T tablesectionbase, Color textColor) where T : IRxTableSectionBase
        {
            tablesectionbase.TextColor = textColor;
            return tablesectionbase;
        }
    }
}