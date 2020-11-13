using EFCoreAssignment.Models;
using StudentsEFAssignment.Entities;
using System.Collections.Generic;

namespace EFCoreAssignment.Api
{
    public interface IUniversityApi
    {
        // Methods used in this assignment
        public StudentSubject GetStudentSubject(long studentId, long subjectId);
        public StudentSubject SaveStudentSubjectScore(long studentId, long subjectId, double score);

        // Previously written methods
        public void AddStudents(List<Student> students);
        public Subject GetSubjectById(long id);
        public List<StudentSubject> GetAllStudentSubjects();
        public List<StudentSubjectDto> GetAllStudentSubjectModels();
        public Student GetStudentById(long studentId);
        public bool SubjectExists(long subjectId);
        public bool StudentExists(long studentId);       
        public void AddSubjects(List<Subject> subjects);
        public void EnrollStudents();

    }
}
