namespace Samples.Test;
public class InterfaceSamples
{
    public static void ScenarioOne()
    {
        var student = new Student();
        student.Dance(); // Calls interface

        // Keep the console open in debug mode.
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    public static void ScenarioTwo()
    {
        var student = new Student();
        student.MoveOn();

        // Keep the console open in debug mode.
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}

