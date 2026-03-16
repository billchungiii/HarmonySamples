namespace SampleLibrary013
{
    public class OriginalClass
    {
        public static string DisplayValue(int baseValue)
        {
            return $"The calculated value is {Calculate(baseValue)}";
        }
        private static int Calculate(int baseValue)
        {
            return baseValue * 2;
        }
    }
}
