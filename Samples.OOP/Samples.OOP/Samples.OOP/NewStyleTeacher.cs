﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class NewStyleTeacher : Teacher
    {
        public new void Dance()
        {
            System.Console.WriteLine("New Sytle-DANCE");
        }



    }
}
