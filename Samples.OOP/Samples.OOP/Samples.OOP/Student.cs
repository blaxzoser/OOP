using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.OOP
{
    public class Student : Teacher, IMoveShoulder, IAction
    {
        protected int originalItemID { get; set; }

        public Student() {
            System.Console.WriteLine("Created Student");

        }

        public Student(string title, string desc, TimeSpan jobLen,
                      int originalID)
        {
            // The following properties and the GetNexID method are inherited 
            // from WorkItem.
            this.ID = GetNextID();
            this.Title = title;
            this.Description = desc;
            this.jobLength = jobLen;

            // Property originalItemId is a member of ChangeRequest, but not 
            // of WorkItem.
            this.originalItemID = originalID;

            System.Console.WriteLine("Created Student by args-CHILD");
        }

        public void MoveOn()
        {
            System.Console.WriteLine("Move Shoulders Studen");
        }

        //Typical question in interview
        //Showwing apply again
        public  void Dance()
        {
            System.Console.WriteLine("Move Dance Student by interface");
        }
    }
}
