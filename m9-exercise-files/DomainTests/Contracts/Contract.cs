using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DomainTests.Contracts
{
    public static class Contract
    {
        public static void Requires(Func<bool> predicate, string message)
        {
            Debug.Assert(predicate(), message);
        }

        public static void Ensures(Func<bool> predicate, string message)
        {
            Debug.Assert(predicate(), message);
        }

        public static void RequiresAll<T>(this IEnumerable<T> sequence, Func<T, bool> predicate, string message)
        {
            foreach (T obj in sequence)
                Requires(() => predicate(obj), message);
        }

        public static void EnsuresAll<T>(this IEnumerable<T> sequence, Func<T, bool> predicate, string message)
        {
            foreach (T obj in sequence)
                Requires(() => predicate(obj), message);
        }
    }
}
