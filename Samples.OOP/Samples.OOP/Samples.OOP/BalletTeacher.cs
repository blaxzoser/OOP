using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class BalletTeacher : AbstractTeacher
    {
        public override string Schedule => "12:00";

        public BalletTeacher()
        {
            System.Console.WriteLine("Create BalletTeacher");
        }
        public override void SetMoney()
        {
            Console.WriteLine("Set money Ballet Teacher");
        }
    }
}
