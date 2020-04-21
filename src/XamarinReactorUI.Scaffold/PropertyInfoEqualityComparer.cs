using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace XamarinReactorUI.Scaffold
{
    internal class PropertyInfoEqualityComparer : IEqualityComparer<PropertyInfo>
    {
        public bool Equals(PropertyInfo x, PropertyInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(PropertyInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}