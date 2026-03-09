using HarmonyLib;
using SampleLibrary008;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SampleApp008
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            var harmony = new Harmony("com.example.SampleApp008");
            harmony.PatchAll();
            await OriginalClass.LongTimeMethodAsync();
        }
    }

    public class StateMachineInfo
    {
        public FieldInfo Field { get; set; }
        public Stopwatch Stopwatch { get; set; }
    }


    [HarmonyPatch]
    public static class PatchOriginal
    {
        private static readonly ConditionalWeakTable<object, StateMachineInfo> _stopwatches = new();
        static MethodBase TargetMethod()
        {
            var method = typeof(OriginalClass).GetMethod(nameof(OriginalClass.LongTimeMethodAsync));
            var attr = method.GetCustomAttribute<AsyncStateMachineAttribute>();
            return attr.StateMachineType.GetMethod("MoveNext", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        static void Prefix(object __instance)
        {
            if (!_stopwatches.TryGetValue(__instance, out var info))
            {
                // The state machine has an internal field called "<>1__state"
                // -1 indicates the state machine has just started execution
                var stateField = __instance.GetType().GetField("<>1__state", BindingFlags.Public | BindingFlags.Instance);
                var sw = new Stopwatch();
                info = new StateMachineInfo { Field = stateField, Stopwatch = sw};
                _stopwatches.Add(__instance, info);
                sw.Start();
            }
        }

        static void Postfix(object __instance)
        {
            if (_stopwatches.TryGetValue(__instance, out var info))
            {
                int internalState = (int)info.Field.GetValue(__instance);
                if (internalState == -2) // State -2 indicates the async state machine has completed execution
                {
                    info.Stopwatch.Stop();
                    Console.WriteLine($"[Harmony] LongTimeMethod 執行完畢，耗時: {info.Stopwatch.ElapsedMilliseconds} ms");
                    _stopwatches.Remove(__instance);
                }
            }
        }
    }
}
