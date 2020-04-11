using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.TestApp.ComponentWithChildren
{
    public class WrapGrid : RxComponent, IEnumerable<VisualNode>
    {
        private readonly List<VisualNode> _children = new List<VisualNode>();
        private int _columnCount = 4;

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IEnumerable<VisualNode> children)
        {
            _children.AddRange(children);
        }

        public WrapGrid ColumnCount(int columnCount)
        {
            _columnCount = columnCount;
            return this;
        }

        public override VisualNode Render()
        {
            int rowIndex = 0, colIndex = 0;

            int rowCount = Math.DivRem(_children.Count, _columnCount, out var divRes);
            if (divRes > 0)
                rowCount++;

            return new RxGrid(
                Enumerable.Range(1, rowCount).Select(_ => new RowDefinition() { Height = GridLength.Auto }),
                Enumerable.Range(1, _columnCount).Select(_ => new ColumnDefinition()))
            {
                _children.Select(child =>
                {
                    child.GridRow(rowIndex);
                    child.GridColumn(colIndex);
                    
                    colIndex++;
                    if (colIndex == _columnCount)
                    {
                        colIndex = 0;
                        rowIndex++;
                    }

                    return child;
                }).ToArray()
            };
        }
    }
}
