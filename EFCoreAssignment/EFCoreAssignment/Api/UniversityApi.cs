using EFCoreAssignment.Models;
using StudentsEFAssignment.Contexts;
using StudentsEFAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreAssignment.Api
{
    class UniversityApi
    {

        private UniversityDbContext context;

        public UniversityApi()
        {
            context = new UniversityDbContext();
        }


        public void AddStudents(List<Student> students)
        {
            foreach (Student s in students)
            {
                context.Add<Student>(s);
            }

            context.SaveChanges();
        }

        public Subject GetSubjectById(long subjectId)
        {
            return context.Subjects.Find(subjectId);
        }

        public List<StudentSubject> GetAllStudentSubjects()
        {
            return context.StudentSubjects.ToList();
        }

        public List<StudentSubjectModel> GetAllStudentSubjectModels()
        {
            var result = (from ss in context.StudentSubjects
                          join subj in context.Subjects on ss.SubjectId equals subj.Id
                          join student in context.Students on ss.StudentId equals student.Id
                          select new StudentSubjectModel
                          {
                              Id = ss.Id,
                              StudentFirstName = student.FirstName,
                              StudentLastName = student.LastName,
                              SubjectName = subj.Name
                          }).ToList();

            return result;
        }

        public Student GetStudentById(long studentId)
        {
            return context.Students.Find(studentId);
        }

        public bool SubjectExists(long subjectId)
        {
            return context.Subjects.Find(subjectId) != null;
        }

        public bool StudentExists(long studentId)
        {
            return context.Students.Find(studentId) != null;
        }

        internal StudentSubject GetStudentSubject(long studentId, long subjectId)
        {
            return context.StudentSubjects.SingleOrDefault(s => s.StudentId == studentId && s.SubjectId == subjectId);
        }

        public void AddSubjects(List<Subject> subjects)
        {
            foreach (Subject s in subjects)
            {
                context.Add<Subject>(s);
            }

            context.SaveChanges();
        }

        internal void SaveStudentSubjectScore(long studentId, long subjectId, double score)
        {
            StudentSubject ss = context.StudentSubjects.SingleOrDefault(s => s.StudentId == studentId && s.SubjectId == subjectId);
            if (ss != null)
            {
                ss.Score = score;
            }

            context.SaveChanges();
        }

        public void EnrollStudents()
        {
            var allStudents = context.Students.ToList();
            var allSubjects = context.Subjects.Select(s => s.Id).ToList();

            Random r = new Random();

            foreach (Student s in allStudents)
            {

                int randomIndex1 = r.Next(allSubjects.Count);
                long randomId1 = allSubjects[randomIndex1];
                context.Add(new StudentSubject
                {
                    StudentId = s.Id,
                    SubjectId = randomId1
                });

                int randomIndex2 = r.Next(allStudents.Count);
                while (randomIndex1 == randomIndex2)
                {
                    randomIndex2 = r.Next(allStudents.Count);
                }

                long randomId2 = allSubjects[randomIndex2];
                context.Add(new StudentSubject
                {
                    StudentId = s.Id,
                    SubjectId = randomId2
                });
            }

            context.SaveChanges();
        }

    }
}
