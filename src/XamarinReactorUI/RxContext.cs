﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI
{
    public class RxContext : Dictionary<string, object>
    {
    }

    public static class RxContextExtensions
    {
        public static T Get<T>(this RxContext context, string key, T defaultValue = default)
        {
            if (context.TryGetValue(key, out var value))
                return (T)value;

            return defaultValue;
        }
    }
}
