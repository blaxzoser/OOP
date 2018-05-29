using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class ReguetonTeacher : AbstractTeacher
    {
        public override string Shudule => "21:00";

        public ReguetonTeacher(string name) 
            :base(name)
        {
            Console.WriteLine("Regueton Teacher Created");
        }

        public override void SetMoney()
        {
            Console.WriteLine("Set money Regueton Teacher");
        }
    }
}
