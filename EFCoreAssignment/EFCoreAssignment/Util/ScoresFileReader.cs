﻿using EFCoreAssignment.Api;
using StudentsEFAssignment.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EFCoreAssignment.Util
{
    class ScoresFileReader
    {
        private readonly string ProblemTextPrefix = "";

        private string FilePath = "";
        private List<string> FailedImportsInfo;
        private string[] FileLines;
        private readonly UniversityApi api; //TODO fix injection
        private int SuccessfulImportsCount;

        public ScoresFileReader(string path)
        {
            FilePath = path;
            FailedImportsInfo = new List<string>();
            SuccessfulImportsCount = 0;
            api = new UniversityApi();
        }


        private void ReadLines()
        {
            // Open a csv file TODO: generify path like .\Resource\StudentPointsData.csv
            //string filePath = @"C:\Users\meryc\Desktop\მაგისტრატურა\Programming Technologies\EFCoreAssignment\EFCoreAssignment\EFCoreAssignment\Resources\StudentPointsData.csv";
            string[] fileLines = null;
            try
            {
                fileLines = File.ReadAllLines(FilePath, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading file located at " + FilePath);
                Console.WriteLine(e.Message);
            }

            FileLines = fileLines;
        }


        public void ParseAndStoreData()
        {
            ReadLines();

            if (FileLines != null && FileLines.Length > 1) // line 0 is headers
            {
                for (int i = 1; i < FileLines.Length; i++)
                {
                    string problemString = "Problem at line N." + i + ": ";


                    string line = FileLines[i];
                    string[] dataArray = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    if (dataArray == null || dataArray.Length < 3)
                    {
                        FailedImportsInfo.Add(problemString + "Malformed data input");
                    }
                    else
                    {
                        ParseLineData(dataArray);
                    }
                }

                PrintResults();
            }
        }

        private void ParseLineData(string[] dataArray)
        {
            long studentId = long.Parse(dataArray[0].Trim());
            long subjectId = long.Parse(dataArray[1].Trim());
            int score = Int32.Parse(dataArray[2].Trim());

            bool isValidStudentId = api.StudentExists(studentId);
            bool isValidSubjectId = api.SubjectExists(subjectId);
            StudentSubject existingStudentSubject = api.GetStudentSubject(studentId, subjectId);

            string problemString = ProblemTextPrefix;

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
                FailedImportsInfo.Add(problemString);
            }
            else
            {
                if (existingStudentSubject == null)
                {
                    problemString += "Student with id " + studentId + " is not enrolled in class with id " + subjectId;
                    FailedImportsInfo.Add(problemString);
                }
                else
                {
                    api.SaveStudentSubjectScore(studentId, subjectId, score);
                    SuccessfulImportsCount++;
                }
            }
        }

        private void PrintResults()
        {
            Console.WriteLine("Number of successfully saved entries: " + SuccessfulImportsCount);
            Console.WriteLine("Number of failed attempts: " + FailedImportsInfo.Count);
            if (FailedImportsInfo.Count > 0)
            {
                Console.WriteLine("Problems encountered: ");
                foreach (string problem in FailedImportsInfo)
                {
                    Console.WriteLine(problem);
                }
            }
        }
    }
}