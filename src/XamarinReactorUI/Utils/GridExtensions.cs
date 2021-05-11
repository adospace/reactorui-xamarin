using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI.Utils
{
    public static class GridExtensions
    {
        public static bool IsEqualTo(this RowDefinitionCollection rows, RowDefinitionCollection otherRows)
        {
            if (rows == null && otherRows == null)
                return true;
            if (rows == null)
                return false;
            if (otherRows == null)
                return false;
            if (rows.Count != otherRows.Count)
                return false;

            for (int i = 0; i < rows.Count; i++)
            {
                if (!rows[i].Height.Equals(otherRows[i].Height))
                    return false;
            }

            return true;
        }

        public static bool IsEqualTo(this ColumnDefinitionCollection columns, ColumnDefinitionCollection otherColumns)
        {
            if (columns == null && otherColumns == null)
                return true;
            if (columns == null)
                return false;
            if (otherColumns == null)
                return false;
            if (columns.Count != otherColumns.Count)
                return false;

            for (int i = 0; i < columns.Count; i++)
            {
                if (!columns[i].Width.Equals(otherColumns[i].Width))
                    return false;            
            }

            return true;
        }
    }
}
