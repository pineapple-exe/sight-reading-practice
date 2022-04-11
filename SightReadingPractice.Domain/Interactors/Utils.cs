using System;
using System.Collections.Generic;
using System.Linq;

namespace SightReadingPractice.Domain.Interactors
{
    internal static class Utils
    {
        public static List<T> Shuffle<T>(this IEnumerable<T> collection, Random random)
        {
            List<T> copy = collection.ToList();

            int n = copy.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = copy[k];
                copy[k] = copy[n];
                copy[n] = value;
            }

            return copy;
        }
    }
}
