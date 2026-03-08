using SampleLibrary005;

namespace SampleApp005
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var account = new BankAccount { Owner = "Alice", Balance = 1000m, IsFrozen = false };
        }
    }
}
