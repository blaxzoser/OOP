using Samples.OOP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Test
{
    /// <summary>
    /// Some samples with override,new,implicit cast
    /// </summary>
    public static class PolymorphismSamples
    {

        //Classic sample of Polymorphism
        public static void ScenarioFour()
        {
            // Polymorphism at work #1: a Rectangle, Triangle and Circle
            // can all be used whereever a Shape is expected. No cast is
            // required because an implicit conversion exists from a derived 
            // class to its base class.
            // A SalsaTeacher "is-a" Teacher.
            var shapes = new List<Teacher>
            {
                new SalsaTeacher(),
                new HipHopTeacher(),
                new BachataTeacher()
            };

            // Polymorphism at work #2: the virtual method Draw is
            // invoked on each of the derived classes, not the base class.
            foreach (var shape in shapes)
            {
                shape.Dance();
            }

            Teacher teacher = new SalsaTeacher();
            teacher.Dance();


            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        //Classic sample of Polymorphism 2
        // Virtual methods and properties enable derived classes to extend a base class without 
        // needing to use the base class implementation of a method
        public static void ScenarioFive()
        {
            BachataTeacher B = new BachataTeacher();
            B.Dance();  // Calls the new method.

            Teacher A = (Teacher)B;
            A.Dance();  // Also calls the new method.

            Teacher D = new BachataTeacher();
            D.Dance(); // Also calls the new method.

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        //Classic sample of Polymorphism 3
        // If you want your derived member to have the same name as a member in a base class, but you do not want it to participate in virtual invocation,
        // you can use the new keyword.The new keyword is put before 
        // the return type of a class member that is being replaced.The following code provides an
        public static void ScenarioSix()
        {
            NewStyleTeacher B = new NewStyleTeacher();
            B.Dance();  // Calls the new method.

            Teacher A = (Teacher)B;
            A.Dance();  // Calls the old method.

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


        //Classic sample of Polymorphism 4
        // Accessing Base Class Virtual Members from Derived Classes
        // A derived class that has replaced or overridden a method or property 
        // can still access the method or property on the base class using the base keyword.
        public static void ScenarioSeven()
        {
            StupidTeacher B = new StupidTeacher();
            B.Dance();  // Calls the old method.



            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        //Classic sample of Polymorphism 4 Knowing When to Use Override and New Keywords 
        public static void ScenarioEight()
        {
            Teacher baseTeacher = new Teacher();
            baseTeacher.ShowMeDance();
            System.Console.WriteLine("----------");

            NewStyleTeacher newStyleTeacher = new NewStyleTeacher();
            newStyleTeacher.ShowMeDance(); //go to the new
            newStyleTeacher.Dance(); ///go to the new
            System.Console.WriteLine("----------");

            BachataTeacher bachataTeacher = new BachataTeacher();
            bachataTeacher.ShowMeDance(); //go to  base
            System.Console.WriteLine("----------");

            StupidTeacher stupidTeacher = new StupidTeacher();
            stupidTeacher.ShowMeDance(); //go to the old

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        //Calls with cast implicit Knowing When to Use Override and New Keywords 
        // "new" keyword is specified, the compiler will issue a warning and the method 
        //in the derived class will hide the method in the base class.
        public static void ScenarioNine()
        {
            var derived = new NewStyleTeacher();
            derived.Dance();
            ((Teacher)derived).Dance(); //Call old method-----new
            System.Console.WriteLine("----------");

            var derived2 = new BachataTeacher();
            derived2.Dance(); 
            ((Teacher)derived2).Dance(); //Call new method-----------override

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
