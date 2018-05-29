using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class BaletTeacher : AbstractTeacher
    {
        public override string Shudule => "12:00";

        public BaletTeacher()
        {
            System.Console.WriteLine("Create BaletTeacher");
        }
        public override void SetMoney()
        {
            Console.WriteLine("Set money Balet Teacher");
        }
    }
}
