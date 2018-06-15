using System;
using System.Collections.Generic;
using System.Text;
using static Samples.Delegates.Program;

namespace Samples.Delegates
{
    public class Calculator
    {
        public void Math(int x,int y, RuleDelegate del)
        {
            var result = del(x, y);
            System.Console.WriteLine(result);
        }

        public void Math(int x, int y, int z, Action<int,int,int> action)
        {
            action(x, y, z);
            System.Console.WriteLine("Do it");
        }

        public void Math(int x, int y, int z, int w,Func<int, int, int,int,int> func)
        {
            var result = func(x, y, z,w);
            System.Console.WriteLine(result);
        }
    }
}
