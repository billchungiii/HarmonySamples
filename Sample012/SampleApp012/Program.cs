using HarmonyLib;
using SampleLibrary012;
using System.Reflection;
using System.Reflection.Emit;
namespace SampleApp012
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sample012");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Console.WriteLine($"Original Value: {OriginalClass.Value}");
        }
    }

    /// <summary>
    /// Provides a static class that serves as the Harmony patch target for the static constructor of OriginalClass.
    /// </summary>
    /// <remarks>
    /// This class is intended to be used with the Harmony library to modify the behavior of the
    /// static constructor of OriginalClass at runtime. It contains patching methods, such as transpilers, that Harmony
    /// will invoke when applying patches. This class should not be instantiated.
    /// <para>
    /// This patch uses a Harmony Transpiler with <see cref="CodeMatcher"/> to locate and modify specific IL instructions.
    /// The <see cref="CodeMatcher"/> provides a fluent API for navigating and manipulating IL code:
    /// <list type="bullet">
    ///   <item><description><see cref="CodeMatcher.MatchStartForward"/> - Searches forward for a matching instruction pattern.</description></item>
    ///   <item><description><see cref="CodeMatcher.SetInstruction"/> - Replaces the current matched instruction with a new instruction.</description></item>
    ///   <item><description><see cref="CodeMatcher.InstructionEnumeration"/> - Returns the modified instructions as an enumerable.</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    [HarmonyPatch(typeof(OriginalClass), MethodType.StaticConstructor)]
    public static class PatchTarget
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
                .MatchStartForward(new CodeMatch(OpCodes.Ldc_I4_S, (sbyte)100))
                .SetInstruction(new CodeInstruction(OpCodes.Ldc_I4_5))
                .InstructionEnumeration();
        }
    }
}
