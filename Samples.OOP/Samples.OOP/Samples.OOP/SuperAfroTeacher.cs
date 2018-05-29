using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class SuperAfroTeacher : AfroTeacher
    {
        public SuperAfroTeacher()
        {
            System.Console.WriteLine("Created SuperAfroTeacher");
        }

        public new  void Dance()
        {
            System.Console.WriteLine("Dance SuperAfroTeacher");
        }
    }
}
