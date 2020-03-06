using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinReactorUI
{
    public static class ResourceDictionaryExtensions
    {
        public static T GetResourceOrDefault<T>(this ResourceDictionary resourceDictionary, string resourceKey, T defaultValue = default)
        {
            if (resourceDictionary is null)
            {
                throw new ArgumentNullException(nameof(resourceDictionary));
            }

            if (string.IsNullOrWhiteSpace(resourceKey))
            {
                throw new ArgumentException("can't be null or empty", nameof(resourceKey));
            }

            if (resourceDictionary.TryGetValue(resourceKey, out var value))
                return (T)value;

            return defaultValue;        
        }

    }
}
