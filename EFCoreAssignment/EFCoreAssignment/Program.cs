using EFCoreAssignment.Api;
using EFCoreAssignment.Models;
using StudentsEFAssignment.Entities;
using System;
using System.Collections.Generic;

namespace EFCoreAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            UniversityApi api = new UniversityApi();
            List<StudentSubjectModel> allStudentSubjects = api.GetAllStudentSubjectModels();

            foreach (StudentSubjectModel ssm in allStudentSubjects)
            {
                Console.WriteLine("Enter score for student " + ssm.StudentFirstName + " " + ssm.StudentLastName +
                    " in subject " + ssm.SubjectName);
                double score = Convert.ToDouble(Console.ReadLine());
                api.SaveStudentSubjectScore(ssm.Id, score);
            }
        }



        private static List<Student> GetStudents(int count)
        {
            RandomDataGenerator dataGenerator = new RandomDataGenerator();

            List<Student> students = new List<Student>();

            for (int i = 0; i < count; i++)
            {
                Student s = new Student();
                s.FirstName = RandomDataGenerator.getRandomFirstName();
                s.LastName = RandomDataGenerator.getRandomSurname();

                students.Add(s);
            }

            return students;
        }

        private static List<Subject> GetSubjects()
        {
            List<Subject> subjects = new List<Subject>();

            Subject s1 = new Subject();
            s1.Name = "Differential Equations";
            s1.Code = "MATH-301";
            s1.Credits = 5;
            subjects.Add(s1);

            Subject s2 = new Subject();
            s2.Name = "Object Oriented Programming";
            s2.Code = "CS-215";
            s2.Credits = 6;
            subjects.Add(s2);

            Subject s3 = new Subject();
            s3.Name = "Computer Networks";
            s3.Code = "CS-930";
            s3.Credits = 7;
            subjects.Add(s3);

            Subject s4 = new Subject();
            s4.Name = "Game Development";
            s4.Code = "CS-592";
            s4.Credits = 5;
            subjects.Add(s4);

            Subject s5 = new Subject();
            s5.Name = "Physics";
            s5.Code = "PHY-101";
            s5.Credits = 6;
            subjects.Add(s5);

            return subjects;
        }
    }

    class RandomDataGenerator
    {
        private static List<string> FirstNames = new List<string>(new string[] { "Jane", "Joe", "Inigo", "Data", "Mzia" });
        private static List<string> LastNames = new List<string>(new string[] { "Doe", "Montoya", "Lee", "Kufaradze", "Tutashkhia" });
        private static List<string> SubjectCodePrefixes = new List<String>(new string[] { "CS", "MATH", "ART" });

        private static Random random;


        public RandomDataGenerator()
        {
            random = new Random();
        }

        //Students
        public static string getRandomFirstName()
        {
            int randomIndex = random.Next(0, FirstNames.Count);
            return FirstNames[randomIndex];
        }

        public static string getRandomSurname()
        {
            int randomIndex = random.Next(0, LastNames.Count);
            return LastNames[randomIndex];
        }


        //Subjects TODO delete
        public static string getRandomSubjectCode()
        {
            int randomIndex = random.Next(0, SubjectCodePrefixes.Count);
            string prefix = SubjectCodePrefixes[randomIndex];
            int randomCode = random.Next(100, 1000);
            return prefix + "-" + randomCode;
        }
    }
}
