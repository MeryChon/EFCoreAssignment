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
            //List<StudentSubjectModel> allStudentSubjects = api.GetAllStudentSubjectModels();

            //foreach (StudentSubjectModel ssm in allStudentSubjects)
            //{
            //    Console.WriteLine("Enter score for student " + ssm.StudentFirstName + " " + ssm.StudentLastName +
            //        " in subject " + ssm.SubjectName);
            //    double score = Convert.ToDouble(Console.ReadLine());
            //    api.SaveStudentSubjectScore(ssm.Id, score);
            //}
        }
    }
}
