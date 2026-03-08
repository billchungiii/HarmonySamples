using System;
using System.Numerics;

namespace SampleLibrary004
{   

    public class OriginalClass
    {
         public static void LongTimeMethod()
        {
            Console.WriteLine("executing LongTimeMethod.....");
            Enumerable.Range(0, 100000).Select(x => BigInteger.Pow(x, 10)).ToArray();
        }
    }
}
