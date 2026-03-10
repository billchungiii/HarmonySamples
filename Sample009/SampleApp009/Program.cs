using HarmonyLib;
using SampleLibrary009;
using System.Reflection;

namespace SampleApp009
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var harmony = new Harmony("com.example.sample009");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            var originalObject = new OriginalClass();
            try
            {
                originalObject.DisplayMessage("Hello, Harmony!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Original class exception occurred: {ex.Message}");
            }
        }
    }

    [HarmonyPatch(typeof(OriginalClass), nameof(OriginalClass.DisplayMessage))]
    public static class PatchOriginal
    {
        public static void Prefix(string message, ref OtherClass __state)
        {
            Console.WriteLine($"Prefix: About to display message: {message}");
            __state = new OtherClass();
        }

        public static void Postfix(string message)
        {
            Console.WriteLine($"Postfix: Finished displaying message: {message}");
        }

        public static void Finalizer(ref OtherClass __state)
        {
            Console.WriteLine("Finalizer: Cleaning up resources.");
            __state?.Dispose();
        }
    }

    /// <summary>
    /// A class that simulates a resource that needs to be disposed.
    /// Implements the IDisposable pattern for proper resource cleanup.
    /// </summary>
    public class OtherClass : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: Dispose managed state (managed objects)
                }

                // TODO: Free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: Set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
            Console.WriteLine("OtherClass disposed.");
        }
    }
}
