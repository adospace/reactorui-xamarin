using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace XamarinReactorUI.Scaffold
{
    internal class PropertyInfoEqualityComparer : IEqualityComparer<PropertyInfo>
    {
        public bool Equals([AllowNull] PropertyInfo x, [AllowNull] PropertyInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] PropertyInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}