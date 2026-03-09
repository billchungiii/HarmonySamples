using HarmonyLib;
using SampleLibrary005;
using System.Reflection;

namespace SampleApp005
{
    /// <summary>
    /// Provides the entry point for the application.
    /// </summary>
    /// <remarks>This class is intended for internal use and contains the application's main method, 
    /// which initializes Harmony patches and demonstrates the behavior of the BankAccount class when patched. The class is
    /// not intended to be instantiated or used directly by consumers of the application.</remarks>
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sampleApp005");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            var account = new BankAccount { Owner = "Alice", IsFrozen = true };
            account.Deposit(1000m); // This will trigger the Harmony patch and print a message about the account being frozen.
            Console.WriteLine($"Account balance for {account.Owner}: {account.Balance}"); // Balance should remain unchanged due to the patch.
            Console.WriteLine("UnFrozen account ...");
            account.IsFrozen = false;
            account.Deposit(1000m); // This will allow the deposit to proceed since the account is no longer frozen.
            Console.WriteLine($"Account balance for {account.Owner}: {account.Balance}"); // Balance should reflect the deposit now.
        }
    }

    [HarmonyPatch(typeof(BankAccount), nameof(BankAccount.Deposit))]
    public static class PatchBankAccount
    {
        public static bool Prefix(BankAccount __instance, decimal amount)
        {
            if (__instance.IsFrozen)
            {
                Console.WriteLine($"Cannot deposit to frozen account of {__instance.Owner}.");
                return false; // Skip original method
            }
            return true; // Continue with original method
        }
    }
}
