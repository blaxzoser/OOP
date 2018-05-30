using Samples.Generics;
using System;

namespace Samples.Generic
{
    class Program
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ScenarioTwo();
        }

        /// <summary>
        /// Create a generic method
        /// </summary>
        public static void ScenarioOne()
        {
            var a = 10;
            var b = 20;
            Console.WriteLine("Value of a should be A: " + a);
            Console.WriteLine("Value of b should be B:  " + b);
            SampleGeneric.Swap<int>(ref a, ref b);
            System.Console.WriteLine("----------");
            Console.WriteLine("Value of a should be A: " + a);
            Console.WriteLine("Value of b should be B:  " + b);

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// Constraints on type parameters
        /// </summary>
        public static void ScenarioTwo()
        {
            // GenericList<Programer> list = new GenericList<Programer>(); // it's not valid
            GenericList<Dancer> list = new GenericList<Dancer>();
            list.AddHead(new Dancer("miguel", 1));
            list.AddHead(new Dancer("carlos", 2));
            list.AddHead(new Dancer("kakaroto", 3));

            Console.WriteLine(list.FindFirstOccurrence("kakaroto").ID);

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

}
