using HarmonyLib;
using SampleLibrary010;
using System.Reflection;
namespace SampleApp010
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sample009");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            var originalObject = new OriginalClass();
            originalObject.DisplayMessage("Hello, Harmony!");

            var result = Calculator.Divide(10, 0);
            Console.WriteLine($"Result of division: {result}");

            try
            {
                var number = Parser.ParseInt("NotANumber");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}");
            }
        }
    }


    [HarmonyPatch(typeof(OriginalClass), nameof(OriginalClass.DisplayMessage))]
    public static class PatchOriginal
    {
        /// <summary>
        /// Performs finalization logic for the specified exception and logs its message.
        /// </summary>
        /// <remarks>This method logs the message of the provided exception using the application's logging
        /// mechanism. The method does not throw exceptions but always returns null, which may indicate that the
        /// exception has been handled or suppressed.</remarks>
        /// <param name="__exception">The exception to be finalized. Cannot be null.</param>
        /// <returns>Always returns null.</returns>
        public static Exception Finalizer(Exception __exception)
        {
            if (__exception != null)
            {
                FileLog.Log($"{__exception.Message}");
            }
            return null;
        }
    }

    [HarmonyPatch(typeof(Calculator), nameof(Calculator.Divide))]
    public static class PatchCalculator
    {
        /// <summary>
        /// Handles finalization logic for an operation by updating the result and returning an exception if necessary.
        /// </summary>
        /// <remarks>If an exception is provided, the result is set to -1 to indicate failure. This method
        /// does not propagate the exception; it returns null regardless of input.</remarks>
        /// <param name="__exception">The exception that occurred during the operation, or null if no exception was thrown.</param>
        /// <param name="__result">A reference to the result value to be updated. If <paramref name="__exception"/> is not null, this value is
        /// set to -1.</param>
        /// <returns>Always returns null. The caller should check the updated result value to determine the outcome.</returns>
        public static Exception Finalizer(Exception __exception, ref double __result)
        {
            if (__exception != null)
            {
                __result = -1;
            }
            return null;
        }
    }

    public class ParserException : Exception
    {
        public ParserException(string message) : base(message) { }
    }

    [HarmonyPatch(typeof(Parser), nameof(Parser.ParseInt))]
    public static class PatchParser
    {
        /// <summary>
        /// Finalizes the parsing operation by checking for exceptions and returning a custom exception if necessary.
        /// </summary>
        /// <remarks>If an exception is detected, this method returns a new instance of <see cref="ParserException"/> with a specific message. If no exception is present, it returns null.</remarks>
        /// <param name="__exception">The exception that occurred during parsing, or null if no exception was thrown.</param>
        /// <returns>A new instance of <see cref="ParserException"/> if an exception was detected; otherwise, null.</returns>
        public static Exception Finalizer(Exception __exception)
        {
            if (__exception != null)
            {
                return new ParserException("An error occurred during parsing.");
            }
            return null;
        }
    }
}
