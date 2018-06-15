using System;
using System.Collections.Generic;
using System.Text;
using static Samples.Delegates.Program;

namespace Samples.Delegates
{
    public delegate int RuleDelegate(int x, int y);


    public class DelegateSamples
    {
        /// <summary>
        /// We created a delegate to create a flexible behaviour
        /// </summary>
        public static void ScenarioOne()
        {
            RuleDelegate addDelegate = (x, y) => x + y;
            RuleDelegate multipleDelegate = (x, y) => x * y;

            var newOperation = new Calculator();
            newOperation.Math(10, 10, multipleDelegate);
            newOperation.Math(10, 10, addDelegate);
        }

        /// <summary>
        /// We used Action<T> to create a flexible behaviour
        /// </summary>
        public static void ScenarioTwo()
        {
            Action<int, int, int> myActionSum = (x, y, z) => Console.WriteLine(x + y + z);
            Action<int, int, int> myActionMulti = (x, y, z) => Console.WriteLine(x * y * z);


            var newOperation = new Calculator();
            newOperation.Math(10,10,10,myActionSum);
            newOperation.Math(10,10,10,myActionMulti);
        }

        /// <summary>
        /// We used Func<T,TResult> to create a flexible behaviour
        /// </summary>
        public static void ScenarioTree()
        {
            Func<int, int, int,int,int> myFuncSum = (x, y, z, w) => x + y + z +w;
            Func<int, int, int,int,int> myFuncMulti = (x, y, z, w) => x * y * z *w;

            var newOperation = new Calculator();
            newOperation.Math(10, 10, 10,10, myFuncSum);
            newOperation.Math(10, 10, 10,10, myFuncMulti);
        }
    }
}
