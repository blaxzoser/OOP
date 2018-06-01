using System;

namespace Samples.OOP
{
    public class Teacher
    {
        // Static field currentID stores the job ID of the last WorkItem that
        // has been created.
        // Static data is allocated once and shared among all instances of the class.

        private static int currentID;

        // A static point of data.
        public static double money = 0.03;


        // Static members to get/set interest rate.
        public  void SetMoney(double _money)
        { money = _money; }

        public  double GetMoney()
        { return money; }


        //Properties.
        protected int ID { get; set; }
        protected string Title { get; set; }
        protected string Description { get; set; }
        protected TimeSpan jobLength { get; set; }


        // Default constructor. If a derived class does not invoke a base-
        // class constructor explicitly, the default constructor is called
        // implicitly. 
        public Teacher()
        {
            ID = 0;
            Title = "Teacher Created";
            Description = "Teacher description.";
            jobLength = new TimeSpan();
            System.Console.WriteLine("Created Teacher-CALL CONSTRUCTOR BASE");
        }

        // Instance constructor that has three parameters.
        public Teacher(string title, string desc, TimeSpan joblen)
        {
            this.ID = GetNextID();
            this.Title = title;
            this.Description = desc;
            this.jobLength = joblen;
            System.Console.WriteLine("Created Teache by args");
        }

        // Static constructor to initialize the static member, currentID. This
        // constructor is called one time, automatically, before any instance
        // of WorkItem or ChangeRequest is created, or currentID is referenced.
        static Teacher()
        {
            currentID = 0;
            System.Console.WriteLine("Assigned CurrentID=0-CALL STATIC CONTRUCTOR");
        }

        protected int GetNextID()
        {
            System.Console.WriteLine("++currentID--CALL GETNextID BASE");
            // currentID is a static field. It is incremented each time a new
            // instance of WorkItem is created.
            return ++currentID;
        }

        // Method Update enables you to update the title and job length of an
        // existing WorkItem object.
        public void Update(string title, TimeSpan joblen)
        {
            System.Console.WriteLine("Update");
            this.Title = title;
            this.jobLength = joblen;
        }

        // Virtual method override of the ToString method that is inherited
        // from System.Object.
        public override string ToString()
        {
            System.Console.WriteLine("Override from Object");
            return String.Format("{0} - {1}", this.ID, this.Title);
        }


        public virtual void Dance()
        {
            System.Console.WriteLine("Dance()-BASE");
        }

        public  void ShowMeDance()
        {
            System.Console.WriteLine("Show me Dance-BASE");
            Dance();
        }

    }
}
