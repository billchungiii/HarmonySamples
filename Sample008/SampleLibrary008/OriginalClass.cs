using System.Numerics;
using System.Threading.Tasks;

namespace SampleLibrary008
{
    public class OriginalClass
    {
        public static async Task LongTimeMethodAsync()
        {
            Console.WriteLine("executing LongTimeMethod.....");
            await Task.Delay(200);
        }
    }
}
