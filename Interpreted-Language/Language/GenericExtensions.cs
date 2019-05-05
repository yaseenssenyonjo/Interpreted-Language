using System;
using System.Collections.Generic;

namespace InterpretedLanguage.Language
{
    internal static class GenericExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable) action(element);
        }
    }
}