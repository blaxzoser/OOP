using System;
namespace Samples.OOP.Encapsulation;
public class Player
{
    public Guid ID { get; private set; } // We need to private because it's only set itselft 
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
