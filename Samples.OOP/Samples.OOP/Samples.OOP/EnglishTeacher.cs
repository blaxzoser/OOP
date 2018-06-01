using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class EnglishTeacher : Teacher
    {
        public  void ShowMeDance()
        {
            System.Console.WriteLine("I dont know dance");
           
        }

        public void Dance()
        {
            System.Console.WriteLine("I dont know dance again");
        }
    }
}
