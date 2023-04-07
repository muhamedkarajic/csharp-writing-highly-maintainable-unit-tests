using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Domain.Contracts
{
    public static class Contract
    {
        private class PreconditionViolationException : Exception
        {
            public PreconditionViolationException(string message) : base(message) { }
        }

        public static void Requires(Func<bool> predicate, string message)
        {
            if (predicate()) return;

            Debug.Assert(false, message);
            throw new PreconditionViolationException(message);
        }

        [Conditional("DEBUG")]
        public static void Ensures(Func<bool> predicate, string message)
        {
            Debug.Assert(predicate(), message);
        }

        [Conditional("DEBUG")]
        public static void RequiresAll<T>(this IEnumerable<T> sequence,
                                          Func<T, bool> predicate, string message)
        {
            foreach (T obj in sequence)
                Requires(() => predicate(obj), message);
        }

        [Conditional("DEBUG")]
        public static void EnsuresAll<T>(this IEnumerable<T> sequence, 
                                         Func<T, bool> predicate, string message)
        {
            foreach (T obj in sequence)
                Requires(() => predicate(obj), message);
        }

        [Conditional("DEBUG")]
        public static void RequiresAny<T>(this IEnumerable<T> sequence,
                                          Func<T, bool> predicate, string message)
        {
            Requires(() => sequence.Any(predicate), message);
        }

        [Conditional("DEBUG")]
        public static void EnsuresAny<T>(this IEnumerable<T> sequence,
                                         Func<T, bool> predicate, string message)
        {
            Ensures(() => sequence.Any(predicate), message);
        }
    }
}
