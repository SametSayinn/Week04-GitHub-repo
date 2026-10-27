using System;

namespace StudentMarkcalCulator
{
    /// <summary> Delegate for mark events. </summary>
    public delegate void MarkHandler(string msg);

    /// <summary> Stores course data with struct . </summary>
    public struct CourseCode
    {
        /// <summary> Course code. </summary>
        public string Code;

        /// <summary> Course credits. </summary>
        public int Credits;
    }

    /// <summary> Class for a person. </summary>
    public abstract class Person
    {
        /// <summary> Person's name. </summary>
        public string Name;

        protected string protectedId;

        /// <summary> binding for Person. </summary>
        public Person(string name, string id)
        {
            Name = name;
            protectedId = id;
        }

        /// <summary> Prints details. </summary>
        public abstract void Describe();
    }

    /// <summary> Student class. </summary>
    public class Student : Person
    {
        /// <summary> Fired when a valid mark is added. </summary>
        public event MarkHandler OnMarkAdded;

        /// <summary> Enrolled course. </summary>
        public CourseCode Course;

        private int mark;

        /// <summary> (0 >= Student's mark <= 100). </summary>
        public int Mark
        {
            get { return mark; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    mark = value;

                    if (OnMarkAdded != null)
                    {
                        OnMarkAdded("Valid mark added for " + Name + ": " + value);
                    }
                }
                else
                {
                    Console.WriteLine("[ERROR] Invalid mark for " + Name + "! Mark must be between 0 and 100.");
                }
            }
        }

        /// <summary> Constructor for Student. </summary>
        public Student(string name, string id) : base(name, id)
        {
        }

        /// <summary> Prints student info. </summary>
        public override void Describe()
        {
            Console.WriteLine("Student: " + Name + " | Mark: " + mark);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student s1 = new Student("Samet", "101");
            Student s2 = new Student("Sayın", "102");

            CourseCode course1 = new CourseCode();
            course1.Code = "CPE317";
            course1.Credits = 4;
            s1.Course = course1;

            CourseCode course2 = new CourseCode();
            course2.Code = "CPE317";
            course2.Credits = 4;
            s2.Course = course2;

            s1.OnMarkAdded += NotificationMethod;
            s2.OnMarkAdded += NotificationMethod;

            Console.WriteLine("--- ADDING MARKS ---");
            s1.Mark = 85;
            s1.Mark = 105;

            s2.Mark = -10;
            s2.Mark = 92;

            Console.WriteLine("\n--- STUDENT DETAILS ---");
            s1.Describe();
            s2.Describe();

            Console.ReadLine();
        }

        static void NotificationMethod(string msg)
        {
            Console.WriteLine("[NOTIFICATION] " + msg);
        }
    }
}