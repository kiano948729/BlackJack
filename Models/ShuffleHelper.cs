namespace OOPBlackJack.Models
{
    /// Helper class voor kaart-gerelateerde utilities.
    /// Bevat centrale shuffle methode om code DRY te werken.
    public static class ShuffleHelper
    {
        private static readonly Random random = new();

        /// Shufflet een lijst van objecten met Fisher-Yates algoritme.
        public static void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}