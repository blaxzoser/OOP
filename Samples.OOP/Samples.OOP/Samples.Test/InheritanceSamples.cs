using Samples.OOP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Test
{
    public static class InheritanceSamples
    {
        //Classic sample of Inheritance 1
        public static void ScenarioOne()
        {
            // Create an instance of Teacher by using the constructor in the 
            // base class that takes three arguments.
            Teacher item = new Teacher("Fix Bugs",
                                         "Fix all bugs in my code branch",
                                         new TimeSpan(3, 4, 0, 0));

            // Create an instance of Student by using the constructor in
            // the derived class that takes four arguments.
            Student change = new Student("Change Base Class Design",
                                                     "Add members to the class",
                                                     new TimeSpan(4, 0, 0),
                                                     1);

            // Use the ToString method defined in Teacher
            Console.WriteLine(item.ToString());

            // Use the inherited Update method to change the title of the 
            // Student object.
            change.Update("Change the Design of the Base Class",
                new TimeSpan(4, 0, 0));

            // Student inherits Teacher's override of ToString.
            Console.WriteLine(change.ToString());

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


        //Classic sample of Inheritance 2
        public static void ScenarioTwo()
        {


            // Create an instance of Student by using the constructor in
            // the derived class that takes four arguments.
            Student change = new Student("Change Base Class Design",
                                                     "Add members to the class",
                                                     new TimeSpan(4, 0, 0),
                                                     1);

            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        //Classic sample of Static
        public static void ScenarioTree()
        {
            //Simply put, a static constructor is a special constructor that is an ideal place to initialize the
            //values of static data when the value is not known at compile time(e.g., you need to read in the value
            //from an external file, generate a random number, etc.).Here are a few points of interest regarding
            //static constructors:
            //• A given class (or structure) may define only a single static constructor.
            //• A static constructor does not take an access modifier and cannot take any parameters.
            //• A static constructor executes exactly one time, regardless of how many objects of the type are
            //created.
            //• The runtime invokes the static constructor when it creates an instance of the class or before
            //accessing the first static member invoked by the caller.
            //• The static constructor executes before any instance-level constructors.
            Console.WriteLine("***** Fun with Static Data *****\n");
            Teacher teacher1 = new Teacher();
            teacher1.SetMoney(0.001);
            Teacher teacher2 = new Teacher();
            teacher1.SetMoney(0.002);

            Console.WriteLine("teacher 1: {0}", teacher1.GetMoney());
            Console.WriteLine("teacher 2: {0}", teacher2.GetMoney());


            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        //Classic override with out override
        public static void ScenarioFour()
        {
            EnglishTeacher  teacher = new EnglishTeacher();
            teacher.Dance();
            teacher.ShowMeDance(); // Show child - public  void ShowMeDance()
            Console.WriteLine("------------------");

            Teacher teacher1 = new EnglishTeacher();
            teacher1.Dance();
            teacher1.ShowMeDance();  // Show father -public virtual void Dance()
            Console.WriteLine("------------------");

            Teacher newTeacher = (Teacher)teacher;
            newTeacher.Dance();
            newTeacher.ShowMeDance(); // Show father -public virtual void Dance()


            Console.WriteLine("------------------");
            ((Teacher)teacher).Dance();
            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


        //Classic override with "new" witout override
        public static void ScenarioFive()
        {
            NewStyleTeacher teacher = new NewStyleTeacher();
            teacher.Dance();  // Show Child 
            teacher.ShowMeDance(); // Show Father 
            Console.WriteLine("------------------");

    
            Teacher newTeacher = (Teacher)teacher;
            newTeacher.Dance();
            newTeacher.ShowMeDance(); // Show father -public virtual void Dance()


            Console.WriteLine("------------------");
            ((Teacher)teacher).Dance();
            // Keep the console open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


    }
}
