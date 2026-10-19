using System;
namespace Samples.OOP.Encapsulation;
public class Player
{
    public Guid ID { get; private set; } // We need to private because it's only set itself 
    public string Name { get; init; } // C# 9

    public  readonly int Age;
    public Player()
    {
        ID = Guid.NewGuid(); // It allows it  
    }

    public Player(int age)
    {
        this.Age = age;    
    }

}
