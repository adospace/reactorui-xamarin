using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XamarinReactorUI.Scaffold
{
    public partial class TypeGenerator
    {
        private readonly Type _typeToScaffold;

        public TypeGenerator(Type typeToScaffold)
        {
            _typeToScaffold = typeToScaffold;

            var propertiesMap = _typeToScaffold.GetProperties()
                .Where(_ => !_.PropertyType.IsGenericType)
                .Distinct(new PropertyInfoEqualityComparer())
                .ToDictionary(_ => _.Name, _ => _);

            Properties = _typeToScaffold.GetFields()
                .Where(_ => _.FieldType == typeof(Xamarin.Forms.BindableProperty))
                .Where(_ => _.GetCustomAttribute<ObsoleteAttribute>() == null)
                .Select(_ => _.Name.Substring(0, _.Name.Length - "Property".Length))
                .Where(_ => propertiesMap.ContainsKey(_))
                .Select(_ => propertiesMap[_])
                .Where(_ => _.GetCustomAttribute<ObsoleteAttribute>() == null)
                .Where(_ => !_.PropertyType.IsGenericType)
                .Where(_ => (_.GetSetMethod()?.IsPublic).GetValueOrDefault())
                .ToArray();
        }

        public string TypeName() => _typeToScaffold.Name;

        public PropertyInfo[] Properties { get; }
    }
}
