using HarmonyLib;
using SampleLibrary006;
using System.Reflection;
using System.Runtime.CompilerServices;
namespace SampleApp006
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sampleApp006");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            string message = "default message";
            message = OriginalClass.DisplayMessage("Hello, Harmony!", 10, 5);
            Console.WriteLine(message);
            Console.WriteLine("-----------------");
            message = OriginalClass.DisplayMessage("Hello, Harmony!", 10, 10);
            Console.WriteLine(message);
        }
    }

    [HarmonyPatch]
    public static class PatchOriginal
    {
        public static MethodBase TargetMethod()
        {
            /* 這裡提供兩種方式來獲取目標方法的 MethodBase：
             1. 使用 AccessTools，這是 HarmonyLib 提供的工具類，可以更方便地獲取方法信息，尤其是對於內部方法。               
             2. 使用傳統的 Reflection 方法來獲取 MethodBase。
             你可以根據需要選擇其中一種方式。AccessTools 通常更簡潔且更適合 Harmony 的使用場景。*/

            /*  Here are two ways to obtain the target method's MethodBase:
                 1. Use AccessTools, a utility provided by HarmonyLib, which makes it easier to retrieve method information, especially for internal methods.
                 2. Use traditional Reflection methods to obtain the MethodBase.
             You can choose one of these methods based on your needs. AccessTools is usually more concise and better suited for Harmony's use cases.)*/

            return GetMethodByAccessTools();
            //return GetMethodByReflection();
        }

        /// <summary>
        /// Retrieves the MethodBase representing the 'Divide' method of the 'SampleLibrary006.Calculator' type using Reflection.
        /// </summary>  
        /// <returns>A MethodBase object for the 'Divide' method of the 'SampleLibrary006.Calculator' type, or null if the method
        /// does not exist.</returns>
        private static MethodBase GetMethodByReflection()
        {
            Assembly sampleLibraryAssembly = Assembly.Load("SampleLibrary006");
            Type calculatorType = sampleLibraryAssembly.GetType("SampleLibrary006.Calculator", throwOnError: true);
            return calculatorType.GetMethod("Divide", BindingFlags.NonPublic | BindingFlags.Static);
        }

        /// <summary>
        /// Retrieves the MethodBase representing the 'Divide' method of the 'SampleLibrary006.Calculator' type using AccessTools.
        /// </summary>
        /// <returns>A MethodBase object for the 'Divide' method of the 'SampleLibrary006.Calculator' type. Returns null if the
        /// method is not found.</returns>
        private static MethodBase GetMethodByAccessTools()
        {
            Type calculatorType = AccessTools.TypeByName("SampleLibrary006.Calculator");
            if (calculatorType == null)
            {
                throw new Exception("無法找到類型 'SampleLibrary006.Calculator'");
            }
            return AccessTools.Method(calculatorType, "Divide");
        }

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
