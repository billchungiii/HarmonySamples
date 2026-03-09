using HarmonyLib;
using SampleLibrary007;
using System.Reflection;
namespace SampleApp007
{
    /// <summary>
    /// Provides the entry point and patching logic for the application, demonstrating the use of Harmony to modify method behavior at runtime.
    /// </summary>
    /// <remarks>This class is intended for internal use as the main program driver. It applies a Harmony
    /// patch to the 'Divide' method of the 'SampleLibrary007.Calculator' type before executing sample calls to
    /// demonstrate the effect of the patch. This example showcases the use of manual patching.</remarks>
    internal class Program
    {
        static void Main(string[] args)
        {
            Patch();
            string message = "default message";
            message = OriginalClass.DisplayMessage("Hello, Harmony!", 10, 5);
            Console.WriteLine(message);
            Console.WriteLine("-----------------");
            message = OriginalClass.DisplayMessage("Hello, Harmony!", 10, 10);
            Console.WriteLine(message);
        }

        static void Patch()
        {
            var harmony = new Harmony("com.example.sampleApp007");
            var original = GetOriginalMethod();
            var prefix = GetPrefixMethod();
            harmony.Patch(original, prefix: new HarmonyMethod(prefix));
        }

        private static MethodInfo GetOriginalMethod()
        {
            var harmony = new Harmony("com.example.sampleApp006");
            var originalType = AccessTools.TypeByName("SampleLibrary007.Calculator");
            if (originalType == null)
            {
                throw new Exception("無法找到類型 'SampleLibrary007.Calculator'");
            }
            return AccessTools.Method(originalType, "Divide");
        }

        private static MethodInfo GetPrefixMethod()
        {
            return AccessTools.Method(typeof(PatchOriginal) , nameof (PatchOriginal.Divide));
        }
    }

    public static class PatchOriginal
    {
        public static bool Divide(double x, double y, ref double __result)
        {
            if (y == 0)
            {
                Console.WriteLine("Prefix Divide: Detected potential divide by zero. Modifying y to prevent exception.");
                __result = 0;
                return false; // Skip the original method
            }
            return true;
        }
    }
}
