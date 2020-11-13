using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsEFAssignment.Entities
{
    public class Student
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<StudentSubject> Subjects { get; set; }

        public String toString()
        {
            string result = "Id: " + Id + "; " +
                "First Name: " + FirstName + "; " +
                "Last Name: " + LastName + ";" +
                "Birth Date: " + BirthDate + ";" +
                "Number of Subjects: " + (Subjects == null ? "0" : Subjects.Count.ToString());

            return result;
        }
    }
}
