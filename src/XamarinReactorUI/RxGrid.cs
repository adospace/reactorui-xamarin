using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public interface IRxGrid
    {
        double RowSpacing { get; set; }
        double ColumnSpacing { get; set; }
        ColumnDefinitionCollection ColumnDefinitions { get; set; }
        RowDefinitionCollection RowDefinitions { get; set; }
    }

    public class RxGrid : RxLayout<Grid>, IRxGrid
    {
        public RxGrid()
        {
        }

        public RxGrid(Action<Grid> componentRefAction)
            : base(componentRefAction)
        {

        }

        public RxGrid(string rows, string columns)
        {
            var converter = new GridLengthTypeConverter();
            foreach (var rowDefinition in rows.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => (GridLength)converter.ConvertFromInvariantString(_))
                .Select(_ => new RowDefinition() { Height = _ }))
            {
                RowDefinitions.Add(rowDefinition);
            }
            foreach (var columnDefinition in columns.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => (GridLength)converter.ConvertFromInvariantString(_))
                .Select(_ => new ColumnDefinition() { Width = _ }))
            {
                ColumnDefinitions.Add(columnDefinition);
            }
        }

        public RxGrid(RowDefinitionCollection rows, ColumnDefinitionCollection columns)
        {
            RowDefinitions = rows;
            ColumnDefinitions = columns;
        }

        public RxGrid(IEnumerable<RowDefinition> rows, IEnumerable<ColumnDefinition> columns)
        {
            foreach (var row in rows)
                RowDefinitions.Add(row);
            foreach (var column in columns)
                ColumnDefinitions.Add(column);
        }

        protected override void OnAddChild(VisualNode widget, Element childControl)
        {
            if (childControl is View view)
            {
                NativeControl.Children.Insert(widget.ChildIndex, view);
            }
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, Element childControl)
        {
            NativeControl.Children.Remove((View)childControl);

            base.OnRemoveChild(widget, childControl);
        }

        public double RowSpacing { get; set; } = (double)Grid.RowSpacingProperty.DefaultValue;
        public double ColumnSpacing { get; set; } = (double)Grid.ColumnSpacingProperty.DefaultValue;
        public ColumnDefinitionCollection ColumnDefinitions { get; set; } = new ColumnDefinitionCollection();
        public RowDefinitionCollection RowDefinitions { get; set; } = new RowDefinitionCollection();

        protected override void OnUpdate()
        {
            NativeControl.RowSpacing = RowSpacing;
            NativeControl.ColumnSpacing = ColumnSpacing;
            NativeControl.ColumnDefinitions = ColumnDefinitions;
            NativeControl.RowDefinitions = RowDefinitions;

            base.OnUpdate();
        }
    }

    public static class RxGridExtensions
    {
        public static T RowSpacing<T>(this T grid, double rowSpacing) where T : IRxGrid
        {
            grid.RowSpacing = rowSpacing;
            return grid;
        }

        public static T ColumnSpacing<T>(this T grid, double columnSpacing) where T : IRxGrid
        {
            grid.ColumnSpacing = columnSpacing;
            return grid;
        }

        public static T ColumnDefinition<T>(this T grid) where T : IRxGrid
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            return grid;
        }

        public static T ColumnDefinition<T>(this T grid, double width) where T : IRxGrid
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = width });
            return grid;
        }

        public static T ColumnDefinitionAuto<T>(this T grid) where T : IRxGrid
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            return grid;
        }

        public static T ColumnDefinitionStar<T>(this T grid, double starValue) where T : IRxGrid
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(starValue, GridUnitType.Star) });
            return grid;
        }

        public static T ColumnDefinition<T>(this T grid, GridLength width) where T : IRxGrid
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = width });
            return grid;
        }

        public static T RowDefinition<T>(this T grid) where T : IRxGrid
        {
            grid.RowDefinitions.Add(new RowDefinition());
            return grid;
        }

        public static T RowDefinition<T>(this T grid, double height) where T : IRxGrid
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = height });
            return grid;
        }

        public static T RowDefinitionAuto<T>(this T grid) where T : IRxGrid
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            return grid;
        }

        public static T RowDefinitionStar<T>(this T grid, double starValue) where T : IRxGrid
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(starValue, GridUnitType.Star) });
            return grid;
        }

        public static T RowDefinition<T>(this T grid, GridLength width) where T : IRxGrid
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = width });
            return grid;
        }

        public static T GridRow<T>(this T view, int rowIndex) where T : VisualNode
        {
            if (view is IRxElement element)
                element.SetAttachedProperty(Grid.RowProperty, rowIndex);
            else
            {
                throw new NotSupportedException($"Type '{typeof(T)}' doesn't support attached properties");
            }

            return view;
        }

        public static T GridRowSpan<T>(this T view, int rowSpan) where T : VisualNode
        {
            if (view is IRxElement element)
                element.SetAttachedProperty(Grid.RowSpanProperty, rowSpan);
            else
            {
                throw new NotSupportedException($"Type '{typeof(T)}' doesn't support attached properties");
            }
            return view;
        }

        public static T GridColumn<T>(this T view, int columnIndex) where T : VisualNode
        {
            if (view is IRxElement element)
                element.SetAttachedProperty(Grid.ColumnProperty, columnIndex);
            else
            {
                throw new NotSupportedException($"Type '{typeof(T)}' doesn't support attached properties");
            }
            return view;
        }

        public static T GridColumnSpan<T>(this T view, int columnSpan) where T : VisualNode
        {
            if (view is IRxElement element)
                element.SetAttachedProperty(Grid.ColumnSpanProperty, columnSpan);
            else
            {
                throw new NotSupportedException($"Type '{typeof(T)}' doesn't support attached properties");
            }
            return view;
        }
    }
}