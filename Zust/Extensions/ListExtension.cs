﻿namespace Zust.Web.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            // Create a random number generator
            Random random = new Random();

            // Perform Fisher-Yates shuffle
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                // Swap list elements at positions i and j
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        public static List<T> GetRandomElements<T>(this List<T> source, int count)
        {
            Random random = new Random();
            List<T> result = new List<T>();
            HashSet<int> selectedIndices = new HashSet<int>();

            while (result.Count < count)
            {
                int index = random.Next(source.Count);

                if (!selectedIndices.Contains(index))
                {
                    result.Add(source[index]);
                    selectedIndices.Add(index);
                }
            }

            return result;
        }
    }
}
