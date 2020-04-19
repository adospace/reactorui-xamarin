using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XamarinReactorUI.Internals
{
    internal static class CopyObjectExtensions
    {
        public static void CopyPropertiesTo<T>(this T source, object dest, PropertyInfo[] destProps)
        {
            var sourceProps = typeof(T).GetProperties()
                .Where(x => x.CanRead)
                .ToList();

            foreach (var sourceProp in sourceProps)
            {
                var targetProperty = destProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (targetProperty != null)
                {
                    var sourceValue = sourceProp.GetValue(source, null);
                    if (sourceValue != null && sourceValue.GetType().IsEnum)
                    {
                        sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(sourceProp.PropertyType));
                    }

                    try
                    {
                        targetProperty.SetValue(dest, sourceValue, null);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Unable to copy property '{targetProperty.Name}' of state ({source.GetType()}) to new state after hot reload (Exception: '{ex.Message}')");
                    }
                }
            }

        }

    }
}
