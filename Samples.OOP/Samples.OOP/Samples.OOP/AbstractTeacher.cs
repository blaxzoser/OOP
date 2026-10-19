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
            System.Console.WriteLine("BASE-Create Abstract class");
        }

        public AbstractTeacher(string name)
        {
            Name = name;
            System.Console.WriteLine("BASE-Create Abstract class with args");
        }
        public virtual void Dance()
        {
            Console.WriteLine("BASE-Dance Abstract");
        }
        public virtual void DoClass()
        {
            Console.WriteLine("BASE-DoClass Abstract");
        }
        public abstract void SetMoney();

        public abstract string Schedule
        {
            get;
        }

     

        public override string ToString()
        {
            return " Schedule = " + Schedule;
        }

    }
}
