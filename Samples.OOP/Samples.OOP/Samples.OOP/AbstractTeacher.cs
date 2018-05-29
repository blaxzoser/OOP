using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public abstract class AbstractTeacher
    {
        public string Name { get; set; }

        public AbstractTeacher()
        {
            System.Console.WriteLine("BASE-Create Abstact class");
        }

        public AbstractTeacher(string name)
        {
            Name = name;
            System.Console.WriteLine("BASE-Create Abstact class with args");
        }
        public virtual void Dance()
        {
            Console.WriteLine("BASE-Dace Abstract");
        }
        public virtual void DoClass()
        {
            Console.WriteLine("BASE-DoClass  Abstrac");
        }
        public abstract void SetMoney();

        public abstract string Shudule
        {
            get;
        }

     

        public override string ToString()
        {
            return " Shudle = " + Shudule;
        }

    }
}
