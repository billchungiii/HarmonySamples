
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

    /// <summary>
    /// Provides a Harmony patch for the static constructor of the OriginalClass type.
    /// </summary>
    /// <remarks>This class is used to modify the static constructor of OriginalClass at runtime using
    /// Harmony. It replaces any occurrence of the integer constant 100 with 5 in the constructor's IL code. This patch
    /// is typically used to alter the behavior of the original static constructor without modifying the source code
    /// directly.</remarks>
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
