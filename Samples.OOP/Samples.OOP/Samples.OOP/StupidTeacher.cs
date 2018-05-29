using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class StupidTeacher :Teacher
    {
        public StupidTeacher()
        {
            System.Console.WriteLine("Constructor StupidTeacher -I don't wanna to teach them");
        }


        public override void Dance()
        {
            base.Dance();
        }
    }
}
