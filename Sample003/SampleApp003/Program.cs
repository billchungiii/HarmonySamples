using System.Reflection;
using HarmonyLib;
using SampleLibrary003;
namespace SampleApp003
{
    /// <summary>
    /// Provides the entry point for the application and demonstrates the usage of the Harmony patching mechanism for handling parameters and return value.
    /// This application demonstrates the use of the Harmony library to patch methods at runtime.
    /// Specifically, it shows how to intercept method calls, handle parameters (e.g., prevent divide-by-zero errors),
    /// and modify return values using a Prefix patch.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sampleApp003");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            string message = "default message";
            message = OriginalClass.DisplayMessage("Hello, Harmony!", 10, 5);
            Console.WriteLine(message);
            Console.WriteLine("-----------------");
            message = OriginalClass.DisplayMessage("Hello, Harmony!", 10, 10);
            Console.WriteLine(message);
        }
    }
    [HarmonyPatch(typeof(OriginalClass), "Divide")]
    public static class PatchOriginal
    {
        public static bool Prefix(double x, double y, ref double __result)
        {
            if (y == 0)
            {
                Console.WriteLine("Prefix: Detected potential divide by zero. Modifying y to prevent exception.");
                __result = 0;
                return false; // Skip the original method
            }
            return true;
        }
    }
}
