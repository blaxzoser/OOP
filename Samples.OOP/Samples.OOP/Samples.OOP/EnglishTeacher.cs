using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class EnglishTeacher : Teacher
    {
      

        /// <summary>
        /// Call the child
        /// </summary>
        //public new void Dance()
        //{
        //    System.Console.WriteLine("I dont know dance again i'm new");
        //}


        /// <summary>
        /// Call the Father
        /// </summary>
        public override void Dance()
        {
            System.Console.WriteLine("I dont know dance again how my father");
        }

    }
}
