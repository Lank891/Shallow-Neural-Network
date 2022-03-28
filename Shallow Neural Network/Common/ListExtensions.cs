using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ListExtensions
    {
        private static readonly Random rng = new();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }

        public static void Normalize(this IList<double> list)
        {
            if (list.Count == 0)
                return;

            var sum = list.Sum();
            if (sum == 0)
                return;

            for (int i = 0; i < list.Count; i++)
            {
                list[i] /= sum;
            }
        }
    }
}
