using HarmonyLib;
using SampleLibrary001;
using System.Reflection;

namespace SampleApp001
{
    /// <summary>
    /// Provides the entry point for the application and demonstrates the basic usage of the Harmony patching mechanism.
    /// </summary>
    /// <remarks>This class initializes Harmony with a unique identifier and applies all patches defined in
    /// the executing assembly. It then calls a method to display a message, illustrating the execution order of Harmony
    /// patches: Prefix, Original, and Postfix. This example is intended to show how to set up and use Harmony for
    /// method patching in a .NE;T application.</remarks>
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sampleApp001");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            string result = OriginalClass.DisplayMessage("Hello, Harmony!");
            Console.WriteLine(result);
        }
    }

    /// <summary>
    /// Provides Harmony patch methods for the DisplayMessage method of the OriginalClass type.
    /// </summary>
    /// <remarks>This static class contains prefix and postfix methods used by Harmony to inject custom
    /// behavior before and after the execution of OriginalClass.DisplayMessage. These methods are intended for use with
    /// the Harmony patching framework and are not meant to be called directly.</remarks>
    [HarmonyPatch(typeof(OriginalClass), nameof(OriginalClass.DisplayMessage))]    
    public static class PatchOriginal
    {
        /// <summary>
        /// Writes a message to the console indicating that the prefix action has occurred.
        /// </summary>
        public static void Prefix()
        {
            Console.WriteLine("Prefix: Before the original method.");
        }
        /// <summary>
        /// Performs additional actions after the original method has executed.
        /// </summary>
        public static void Postfix()
        {
            Console.WriteLine("Postfix: After the original method.");
        }
    }
}
