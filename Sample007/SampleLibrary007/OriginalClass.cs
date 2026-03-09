namespace SampleLibrary007
{
    /// <summary>
    /// This class is migrated from Sample003, with the Divide method moved to the internal class Calculator.
    /// </summary>
    public class OriginalClass
    {
        public static string DisplayMessage(string message, double a, double b)
        {
            double x = a;

            /* 假設在程式碼內呼叫許多層的方法後計算後會讓 y=0 (if a==b) , 導致 Divide 方法拋出例外
             這裡的計算只是為了模擬這種情況，實際上可能會有更複雜的邏輯導致 y=0 */
            /* Assume that after calling multiple layers of methods in the code, the calculation results in y=0 (if a==b) ,  causing the Divide method to throw an exception. 
               This calculation is only to simulate such a situation; in reality, there might be more complex logic leading to y=0. */
            double y = a - b;
            double result = Calculator.Divide(x, y);
            return $"Original Message : {message}, Result: {result}";
        }
    }

    /// <summary>
    /// Provides static methods for performing basic arithmetic calculations.
    /// This class is internal, meaning it cannot be accessed directly by other assemblies.
    /// </summary>
    internal static class Calculator
    {
        internal static double Divide(double x, double y)
        {
            Console.WriteLine("Executing Original Method...");
            if (y == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return x / y;
        }
    }
}
