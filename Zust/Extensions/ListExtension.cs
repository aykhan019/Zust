namespace Zust.Web.Extensions
{
    /// <summary>
    /// Contains extension methods for List<T> type.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Shuffles the elements of the List in random order using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the List.</typeparam>
        /// <param name="list">The List to be shuffled.</param>
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

        /// <summary>
        /// Gets a new List containing random elements from the source List, without repetition.
        /// </summary>
        /// <typeparam name="T">The type of elements in the List.</typeparam>
        /// <param name="source">The source List to get random elements from.</param>
        /// <param name="count">The number of random elements to retrieve.</param>
        /// <returns>A new List containing random elements from the source List.</returns>
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