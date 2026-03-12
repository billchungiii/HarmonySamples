
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
    /// <remarks>
    /// This class demonstrates the use of a <b>Harmony Transpiler</b> to modify IL code at runtime.
    /// The Transpiler method receives and yields <see cref="CodeInstruction"/> objects, which represent
    /// individual IL instructions. By iterating through these <see cref="CodeInstruction"/> instances,
    /// you can inspect, modify, add, or remove IL instructions before they are executed.
    /// 
    /// In this example, the Transpiler replaces any occurrence of the integer constant 100 
    /// (loaded via <see cref="OpCodes.Ldc_I4_S"/>) with the constant 5 (using <see cref="OpCodes.Ldc_I4_5"/>).
    /// This approach allows altering the behavior of the original static constructor without modifying
    /// the source code directly.
    /// </remarks>
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
