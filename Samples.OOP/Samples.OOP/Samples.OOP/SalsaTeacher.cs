using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class SalsaTeacher: Teacher
    {
        public override void Dance()
        {
            System.Console.WriteLine("Dance - SALSA");
        }
    }
}
