using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxTableSectionBase
    {
        string Title { get; set; }
        Color TextColor { get; set; }
    }

    public abstract class RxTableSectionBase<T, C> : VisualNode, IRxTableSectionBase where T : TableSectionBase<C>, new() where C : BindableObject
    {
        public RxTableSectionBase()
        {
        }

        public string Title { get; set; } = (string)TableSectionBase.TitleProperty.DefaultValue;
        public Color TextColor { get; set; } = (Color)TableSectionBase.TextColorProperty.DefaultValue;


        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield break;
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