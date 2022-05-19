namespace Samples.Test;
public static class AbstractSamples
{
    public static void Scenario1()
    {
        AfroTeacher afroTeacher1 = new AfroTeacher();
        afroTeacher1.Name = "Erick";
        afroTeacher1.Dance();
        afroTeacher1.SetMoney();

        AfroTeacher afroTeacher2 = new AfroTeacher();
        afroTeacher2.Name = "Danny";
        afroTeacher2.Dance();
        afroTeacher1.SetMoney();


        Console.WriteLine("my name teacher 1 is:" + afroTeacher1.Name);
        Console.WriteLine("my name teacher 2 is:" + afroTeacher2.Name);

        // Keep the console open in debug mode.
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    //Check properites Abstract
    public static void Scenario2()
    {
        AfroTeacher afroTeacher1 = new AfroTeacher();
        afroTeacher1.Dance();
        afroTeacher1.SetMoney();
        Console.WriteLine(afroTeacher1.Shudule);
        System.Console.WriteLine("----------");

        BaletTeacher balet = new BaletTeacher();
        balet.Dance();
        balet.SetMoney();
        Console.WriteLine(balet.Shudule);
        System.Console.WriteLine("----------");


        // Keep the console open in debug mode.
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    //Call  base  params
    public static void Scenario3()
    {
        ReguetonTeacher regueton = new ReguetonTeacher("Luis Pintado");
        regueton.Dance();
        regueton.SetMoney();
        Console.WriteLine(regueton.Shudule);
        Console.WriteLine("the name is:" + regueton.Name);
        System.Console.WriteLine("----------");

        BaletTeacher balet = new BaletTeacher();
        balet.Name = "Diana";
        balet.Dance();
        balet.SetMoney();
        Console.WriteLine(balet.Shudule);
        Console.WriteLine("the name is:" + balet.Name);
        System.Console.WriteLine("----------");


        // Keep the console open in debug mode.
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    //Call  of shadowing
    //In the real world, the possibility of this occurring
    //is the greatest when you are subclassing from a class you (or your team) did not create
    //yourselves(for example, if you purchase a third-party.NET software package).

    //To address this issue, you have two options.You could simply update the parent’s version of
    //Dance() using the override keyword(as suggested by the compiler). With this approach, the
    //SuperAfroTeacher type is able to extend the parent’s default behavior as required.However, if you don’t
    //have access to the code defining the base class (again, as would be the case in many third-party
    //libraries), you would be unable to modify the Dance() method as a virtual member, as you don’t have
    //access to the code file!
    public static void Scenario4()
    {
        SuperAfroTeacher super = new SuperAfroTeacher();
        super.Dance(); // Calls Child
        super.SetMoney();
        Console.WriteLine(super.Shudule);
        Console.WriteLine("the name is:" + super.Name);
        System.Console.WriteLine("----------");
        ((AfroTeacher)super).Dance(); // Call parent


        // Keep the console open in debug mode.
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

}

