using System.Diagnostics;
using System.Reflection;
using HarmonyLib;
using SampleLibrary004;
namespace SampleApp004
{
    /// <summary>
    /// This program demonstrates the use of Harmony patches to intercept and modify the behavior of methods.
    /// Specifically, the PatchOriginal class is used to measure the execution time of the LongTimeMethod
    /// by passing a Stopwatch instance between the Prefix and Postfix methods.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sampleApp004");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            OriginalClass.LongTimeMethod();
        }
    }
    [HarmonyPatch(typeof(OriginalClass), nameof(OriginalClass.LongTimeMethod))]
    public static class PatchOriginal
    {
        public static void Prefix(ref Stopwatch __state)
        {
            Console.WriteLine("Stopwatch start.....");
            __state = new Stopwatch();
            __state.Start();
        }

        public static void Postfix(ref Stopwatch __state)
        {
            __state.Stop();
            Console.WriteLine($"Stopwatch stop, elapsed {__state.ElapsedMilliseconds} ms");
        }
    }   
}
