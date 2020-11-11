using EFCoreAssignment.Api;
using StudentsEFAssignment.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EFCoreAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            UniversityApi api = new UniversityApi();

            // Open a csv file TODO: might allow user to pecify path
            string filePath = @"C:\Users\meryc\Desktop\მაგისტრატურა\Programming Technologies\EFCoreAssignment\EFCoreAssignment\EFCoreAssignment\Resources\StudentPointsData.csv";
            string[] fileLines = null;
            try
            {
                fileLines = File.ReadAllLines(filePath, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading file located at " + filePath);
                Console.WriteLine(e.Message);
            }

            int successfulImportsCount = 0;
            List<String> failedImportsInfo = new List<string>();
            

            // Read, validate and save data
            if (fileLines != null && fileLines.Length > 1) // line 0 is headers
            {
                for (int i = 1; i < fileLines.Length; i++)
                {
                    string problemString = "Problem at line N." + i + ": ";


                    string line = fileLines[i];
                    string[] dataArray = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    if (dataArray == null || dataArray.Length < 3)
                    {
                        failedImportsInfo.Add(problemString + "Malformed data input");
                    }
                    else
                    {
                        long studentId = long.Parse(dataArray[0].Trim());
                        long subjectId = long.Parse(dataArray[1].Trim());
                        int score = Int32.Parse(dataArray[2].Trim());

                        bool isValidStudentId = api.StudentExists(studentId);
                        bool isValidSubjectId = api.SubjectExists(subjectId);
                        StudentSubject existingStudentSubject = api.GetStudentSubject(studentId, subjectId);


                        if (!isValidStudentId || !isValidSubjectId)
                        {
                            if (!isValidStudentId)
                            {
                                problemString += "No student with id " + studentId + ".";
                            }

                            if (!isValidSubjectId)
                            {
                                problemString += "No subject with id " + subjectId + ".";
                            }
                            failedImportsInfo.Add(problemString);
                        }
                        else
                        {
                            if (existingStudentSubject == null)
                            {
                                problemString += "Student with id " + studentId + " is not enrolled in class with id " + subjectId;
                                failedImportsInfo.Add(problemString);
                            }
                            else
                            {
                                api.SaveStudentSubjectScore(studentId, subjectId, score);
                                successfulImportsCount++;
                            }
                        }
                    }
                }
            }

            // Print 
            Console.WriteLine("Number of successfully saved entries: " + successfulImportsCount);
            Console.WriteLine("Number of failed attempts: " + failedImportsInfo.Count);
            if (failedImportsInfo.Count > 0)
            {
                Console.WriteLine("Problems encountered: ");
                foreach (string problem in failedImportsInfo)
                {
                    Console.WriteLine(problem);
                }
            }
        }
    }
}
