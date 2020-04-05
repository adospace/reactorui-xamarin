using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XamarinReactorUI
{
    public static class CopyObjectExtensions
    {
        //public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        //{
        //    var sourceProps = typeof(T).GetProperties()
        //        .Where(x => x.CanRead)
        //        .ToList();
        //    var destProps = typeof(TU).GetProperties()
        //        .Where(x => x.CanWrite)
        //        .ToList();

        //    foreach (var sourceProp in sourceProps)
        //    {
        //        var p = destProps.FirstOrDefault(x => x.Name == sourceProp.Name);
        //        if (p != null)
        //        { 
        //            p.SetValue(dest, sourceProp.GetValue(source, null), null);
        //        }
        //    }
        //}

        public static void CopyPropertiesTo<T>(this T source, object dest, PropertyInfo[] destProps)
        {
            var sourceProps = typeof(T).GetProperties()
                .Where(x => x.CanRead)
                .ToList();

            foreach (var sourceProp in sourceProps)
            {
                var p = destProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (p != null)
                { 
                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
                }
            }

        }

    }
}
