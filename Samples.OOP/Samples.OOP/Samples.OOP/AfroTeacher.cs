using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class AfroTeacher : AbstractTeacher
    {
        public AfroTeacher()
        {
            System.Console.WriteLine("Create AfroTeacher");
        }

        public override string Shudule 
        {
            get
            {
                return "11:00";
            }
        }

        public override void Dance()
        {
            Console.WriteLine("Dace AFRO");
        }

        public override void SetMoney()
        {
            Console.WriteLine("Set money Afro Teacher");
        }
    }
}
