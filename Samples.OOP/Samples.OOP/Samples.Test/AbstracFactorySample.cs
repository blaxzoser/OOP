
using Samples.SOLID.AbstracFactory;
using System;

namespace Samples.Test
{
    public class AbstracFactorySample
    {
        public static void Does()
        {
            Console.WriteLine("Client: Testing client code with the first factory type...");
            ClientMethod(new ConcreateFactoryGermanyWomman());
            Console.WriteLine();

            Console.WriteLine("Client: Testing the same client code with the second factory type...");
            ClientMethod(new ConcreateFactoryMexicoWomman());
            Console.ReadLine();

        }

        public static  void ClientMethod(IWommanFactory factory)
        {
            var dancers = factory.CreateDancer();
            var teachers = factory.CreateTeacher();

            Console.WriteLine(dancers.DoSomething());
            Console.WriteLine(teachers.DoSomething());
        }
    }
}
