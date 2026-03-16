using HarmonyLib;
using SampleLibrary013;
using System.Reflection;
using System.Reflection.Emit;
namespace SampleApp013
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sample013");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Console.WriteLine($"Transpiler Result: {OriginalClass.DisplayValue(100)}");

            ReversePatching.ApplyReversePatches();

            var reverseOriginalResult = ReversePatching.ReverseOriginalCalculate(100);
            Console.WriteLine($"Reverse Original Calculate Result: {reverseOriginalResult}");

            var reverseSnapshotResult = ReversePatching.ReverseSnapshotCalculate(100);
            Console.WriteLine($"Reverse Snapshot Calculate Result: {reverseSnapshotResult}");

        }
    }
    /// <summary>
    /// Provides a Harmony patch target for modifying the static constructor of the OriginalClass type.
    /// </summary>
    /// <remarks>This class is intended to be used with the Harmony library to apply runtime modifications
    /// (patches) to the static constructor of OriginalClass. It is not intended to be instantiated or used directly by
    /// consumers; instead, Harmony uses the presence of this class and its methods to identify and apply the
    /// patch.</remarks>

    [HarmonyPatch(typeof(OriginalClass), "Calculate")]
    public static class PatchTarget
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
                .MatchStartForward(new CodeMatch(OpCodes.Ldc_I4_2))
                .SetInstruction(new CodeInstruction(OpCodes.Ldc_I4_5))
                .InstructionEnumeration();
        }
    }
   
    /// <summary>
    /// Provides static methods for applying and managing reverse patches to the Calculate method of OriginalClass using
    /// the Harmony library.
    /// </summary>
    /// <remarks>This class is intended for advanced scenarios where reverse patching is required, such as
    /// intercepting or redirecting method calls at runtime. The methods in this class are not meant to be called
    /// directly; instead, they are used by the Harmony patching infrastructure to facilitate reverse patching. See the
    /// Harmony documentation for more information on reverse patching and its use cases.</remarks>
    public class ReversePatching
    {      
        public static int ReverseOriginalCalculate(int baseValue) => throw new NotImplementedException("This method is a reverse patch and should not be called directly.");

        public static int ReverseSnapshotCalculate(int baseValue) => throw new NotImplementedException("This method is a reverse patch and should not be called directly.");

        public static void ApplyReversePatches()
        {
            var harmony = new Harmony("com.example.sample013.reverse");
            var originalMethod = AccessTools.Method(typeof(OriginalClass), "Calculate");
            var reverseOriginalMethod = AccessTools.Method(typeof(ReversePatching), nameof(ReverseOriginalCalculate));
            var reverseSnapshotMethod = AccessTools.Method(typeof(ReversePatching), nameof(ReverseSnapshotCalculate));
            harmony.CreateReversePatcher(originalMethod, reverseOriginalMethod).Patch(HarmonyReversePatchType.Original);
            harmony.CreateReversePatcher(originalMethod, reverseSnapshotMethod).Patch(HarmonyReversePatchType.Snapshot);
        }
    }   
}
