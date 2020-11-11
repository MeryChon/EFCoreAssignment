using StudentsEFAssignment.Entities;
using System;
using System.Collections.Generic;

namespace EFCoreAssignment.Util
{
    class RandomDataGenerator
    {
        private static readonly List<string> FirstNames = new List<string>(new string[] { "Jane", "Joe", "Inigo", "Data", "Mzia" });
        private static readonly List<string> LastNames = new List<string>(new string[] { "Doe", "Montoya", "Lee", "Kufaradze", "Tutashkhia" });

        private static Random random;


        public RandomDataGenerator()
        {
            random = new Random();
        }

        //Students
        private string getRandomFirstName()
        {
            int randomIndex = random.Next(0, FirstNames.Count);
            return FirstNames[randomIndex];
        }

        private string getRandomSurname()
        {
            int randomIndex = random.Next(0, LastNames.Count);
            return LastNames[randomIndex];
        }

        public List<Student> GetRandomStudents(int count)
        {
            List<Student> students = new List<Student>();

            for (int i = 0; i < count; i++)
            {
                Student s = new Student();
                s.Id = i + 1;
                s.FirstName = getRandomFirstName();
                s.LastName = getRandomSurname();

                students.Add(s);
            }

            return students;
        }

        public List<Subject> GetRandomSubjects()
        {
            List<Subject> subjects = new List<Subject>();

            Subject s1 = new Subject
            {
                Id = 1,
                Name = "Differential Equations",
                Code = "MATH-301",
                Credits = 5
            };
            subjects.Add(s1);

            Subject s2 = new Subject
            {
                Id = 2,
                Name = "Object Oriented Programming",
                Code = "CS-215",
                Credits = 6
            };
            subjects.Add(s2);

            Subject s3 = new Subject
            {
                Id = 3,
                Name = "Computer Networks",
                Code = "CS-930",
                Credits = 7
            };
            subjects.Add(s3);

            Subject s4 = new Subject
            {
                Id = 4,
                Name = "Game Development",
                Code = "CS-592",
                Credits = 5
            };
            subjects.Add(s4);

            Subject s5 = new Subject
            {
                Id = 5,
                Name = "Physics",
                Code = "PHY-101",
                Credits = 6
            };
            subjects.Add(s5);

            return subjects;
        }
    }
}
