using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinReactorUI.Scaffold
{
    internal static class StringExtensions
    {
        public static string CamelCase(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;
            return Char.ToLowerInvariant(s[0]) + s.Substring(1, s.Length - 1);
        }

        public static string ToResevedWordTypeName(this string typename)
        {
            return typename switch
            {
                "SByte" => "sbyte",
                "Byte" => "byte",
                "Int16" => "short",
                "UInt16" => "ushort",
                "Int32" => "int",
                "UInt32" => "uint",
                "Int64" => "long",
                "UInt64" => "ulong",
                "Char" => "char",
                "Single" => "float",
                "Double" => "double",
                "Boolean" => "bool",
                "Decimal" => "decimal",
                "String" => "string",
                "Object" => "object",
                _ => typename,
            };
        }
    }
}
