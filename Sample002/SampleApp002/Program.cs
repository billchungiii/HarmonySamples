using HarmonyLib;
using SampleLibrary002;
using System.Reflection;
namespace SampleApp002
{
    /// <summary>
    /// Provides the entry point for the application and demonstrates the usage of the Harmony patching mechanism for handling method overloads.
    /// </summary>    
    internal class Program
    {

        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sampleApp002");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            string result = OriginalClass.DisplayMessage("Hello, Harmony!", 100);
            Console.WriteLine(result);
            string result2 = OriginalClass.DisplayMessage("Hello, Harmony!");
            Console.WriteLine(result2);
        }
    }

    /// <summary>
    /// Provides Harmony patch methods for injecting code before and after the execution of the OriginalClass.DisplayMessage method.
    /// </summary>
    /// <remarks>This static class is intended for use with the Harmony library to modify the behavior of the
    /// DisplayMessage method in OriginalClass. This example demonstrates how to patch overloaded methods by specifying
    /// the exact method signature using the new Type[] { } argument. The array explicitly defines the parameter types
    /// of the target method, ensuring that the correct overload is patched. The Prefix and Postfix methods are called
    /// automatically by Harmony when the target method is invoked, allowing custom logic to be executed before and
    /// after the original method. This class should not be instantiated.</remarks>
    [HarmonyPatch(typeof(OriginalClass), nameof(OriginalClass.DisplayMessage), new Type[] { typeof(string), typeof(int) })]
    public static class PatchOriginal
    {
        public static void Prefix()
        {
            Console.WriteLine("Prefix: Before the original method.");
        }
        public static void Postfix()
        {
            Console.WriteLine("Postfix: After the original method.");
        }
    }
}
