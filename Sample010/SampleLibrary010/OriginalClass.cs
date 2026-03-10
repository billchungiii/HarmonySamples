namespace SampleLibrary010
{
    public class OriginalClass
    {
        /// <summary>
        /// This method intentionally throws an exception after displaying the provided message.
        /// </summary>
        /// <param name="message">The message to be displayed before the exception is thrown.</param>
        /// <exception cref="InvalidOperationException">Thrown to demonstrate exception handling.</exception>
        public void DisplayMessage(string message)
        {
            Console.WriteLine($"Original Message : {message}");
           // throw new InvalidOperationException("An error occurred in DisplayMessage.");
        }
    }

    
    public static class Calculator
    {
        /// <summary>
        /// Divides one double-precision floating-point number by another and returns the result.
        /// </summary>
        /// <param name="a">The dividend. Represents the number to be divided.</param>
        /// <param name="b">The divisor. Must not be zero.</param>
        /// <returns>The quotient resulting from dividing <paramref name="a"/> by <paramref name="b"/>.</returns>
        /// <exception cref="DivideByZeroException">Thrown if <paramref name="b"/> is zero.</exception>
        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return a / b;
        }
    }

   
    public static class Parser
    {
        /// <summary>
        /// Converts the specified string representation of a number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="input">A string containing the number to convert. The string should represent a valid integer in the format
        /// recognized by the .NET Framework.</param>
        /// <returns>A 32-bit signed integer equivalent to the number contained in the input string.</returns>
        public static int ParseInt(string input)
        {
            return int.Parse(input);
        }
    }
}
