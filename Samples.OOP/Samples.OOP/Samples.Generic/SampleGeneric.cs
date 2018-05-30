using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Generics
{
    public static class SampleGeneric
    {
        public static void Swap<T>(ref T a,ref T b)
        {
            T temp;
            temp = a;
            a = b;
            b = temp;
        }

        public static void DisplayBaseClass<T>()
        {
            Console.WriteLine("Base class of {0} is: {1}.",
            typeof(T), typeof(T).BaseType);
        }
    }
}
