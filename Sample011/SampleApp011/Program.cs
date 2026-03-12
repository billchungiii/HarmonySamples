
using HarmonyLib;
using SampleLibrary011;
using System.Reflection;
using System.Reflection.Emit;

namespace SampleApp011
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sampleApp011");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Console.WriteLine($"Original Value: {OriginalClass.Value}"); 
        }
    }

    [HarmonyPatch(typeof(OriginalClass), MethodType.StaticConstructor)]
    public class PatchOriginal
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {               
                if (instruction.opcode == OpCodes.Ldc_I4_S && instruction.operand is sbyte val && val == 100)
                {
                    yield return new CodeInstruction(OpCodes.Ldc_I4_5);
                }
                else
                {
                    yield return instruction; 
                }
            }
        }
    }
}
