namespace SampleLibrary002
{
    public class OriginalClass
    {
        public static string DisplayMessage(string message)
        {
            Console.WriteLine("Executing Original Method...");
            return $"Original Message : {message}";
        }

        public static string DisplayMessage(string message, int number)
        {
            Console.WriteLine("Executing Original Method with Number...");
            return $"Original Message : {message}, Number: {number}";
        }
    }
}
