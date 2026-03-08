using System.Reflection;
using HarmonyLib;
using SampleLibrary003;
namespace SampleApp003
{
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
            message= OriginalClass.DisplayMessage("Hello, Harmony!", 10, 10);
            Console.WriteLine(message);
        }
    }

    public static class PatchOriginal
    {
        [HarmonyPatch(typeof(OriginalClass), "Divide")]
        public static class DisplayMessagePatch
        {
            public static bool Prefix(double x, double y, ref double __result)
            {
                if (y== 0)
                {
                    Console.WriteLine("Prefix: Detected potential divide by zero. Modifying y to prevent exception.");
                    __result = 0;
                    return false; // Skip the original method
                }
                return true;
            }
           
        }
    }
}
