using EFCoreAssignment.Api;
using StudentsEFAssignment.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EFCoreAssignment.Util
{
    public class ScoresFileReader
    {
        private readonly string ProblemTextPrefix = "Problem at line N.{0}: ";

        private IUniversityApi Api { get; set; }

        public string FilePath { get; set; }
        public List<string> FailedImportsInfo { get; private set; }
        public int SuccessfulImportsCount { get; private set; }

        public ScoresFileReader(string path, IUniversityApi api)
        {
            FilePath = path;
            FailedImportsInfo = new List<string>();
            SuccessfulImportsCount = 0;
            this.Api = api;
        }

        public ScoresFileReader()
        {
            FailedImportsInfo = new List<string>();
            SuccessfulImportsCount = 0;
        }

        public void ReadAndStoreFileData()
        {
            string[] fileLines = ReadLines(FilePath);

            ParseLines(fileLines);

            if (fileLines != null && fileLines.Length > 1) // line 0 is headers
            {
                PrintResults();
            }
        }


        public string[] ReadLines(string path)
        {
            string[] fileLines = null;
            try
            {
                fileLines = File.ReadAllLines(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading file located at " + path);
                Console.WriteLine(e.Message);
            }

            return fileLines;
        }

        public void ParseLines(string[] lines)
        {
            if (lines != null && lines.Length > 1) // line 0 is headers
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    ParseLine(line, i);
                }
            }
        }

        public void ParseLine(string line, int lineIndex)
        {
            string[] dataArray = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            if (dataArray == null || dataArray.Length < 3)
            {
                string problemString = String.Format(ProblemTextPrefix, lineIndex + 1);
                FailedImportsInfo.Add(problemString + "Malformed data input");
            }
            else
            {
                ParseLineData(dataArray, lineIndex);
            }
        }

        public StudentSubject ParseLineData(string[] dataArray, int lineIndex)
        {
            if (Api == null)
            {
                throw new Exception("Api field must be set");
            }

            long studentId = long.Parse(dataArray[0].Trim());
            long subjectId = long.Parse(dataArray[1].Trim());
            int score = Int32.Parse(dataArray[2].Trim());

            bool isValidStudentId = Api.StudentExists(studentId);
            bool isValidSubjectId = Api.SubjectExists(subjectId);
            StudentSubject existingStudentSubject = Api.GetStudentSubject(studentId, subjectId);

            string problemString = String.Format(ProblemTextPrefix, lineIndex + 1);

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
                    existingStudentSubject = Api.SaveStudentSubjectScore(studentId, subjectId, score);
                    SuccessfulImportsCount++;
                }
            }

            return existingStudentSubject;
        }

        public void PrintResults()
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
