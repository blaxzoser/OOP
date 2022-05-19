using Samples.OOP.Encapsulation;
using System;

namespace Samples.Test.Encapsulation;

internal class PlayerConsole
{
    // Test 'readonly' focus on fields
    // Usually it works inside on parameters on constructor on your class
    public static void ScenarioReadonly()
    {
        var player = new Player();
        // player._age = 23;  ERROR

        var player2 = new Player(23); // GOOD to set on constructor variables
        Console.WriteLine($"New Player1 age:{player.Age}"); // GOOD readonly
        Console.WriteLine($"New Player2 age:{player2.Age}"); // GOOD readonly
    }

    // Test 'private set' propery
    //Usually it works when you want to change internal and readonly for external classes
    public static void ScenarioPrivateSet()
    {
        var player = new Player(); // It will assing new ID on that constructor 
        //player.ID = Guid.NewGuid(); ERROR

        Console.WriteLine($"New Player ID:{ player.ID}"); // GOOD readonly
    }

    // Test 'init' focus on property c# 9
    // Only set the property when you create a new instance 
    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/init
    public static void ScenarioInit()
    {
        var player = new Player() { Name = "Beto"}; //Good Reason
        // player.Name = "betl"; ERROR
       
        Console.WriteLine($"New Player Name:{player.Name}");  
    }
}

