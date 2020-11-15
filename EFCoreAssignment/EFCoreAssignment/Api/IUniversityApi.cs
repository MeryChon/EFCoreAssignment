using EFCoreAssignment.Models;
using StudentsEFAssignment.Entities;
using System.Collections.Generic;

namespace EFCoreAssignment.Api
{
    public interface IUniversityApi
    {
        public StudentSubject GetStudentSubject(long studentId, long subjectId);
        public StudentSubject SaveStudentSubjectScore(long studentId, long subjectId, double score);

        public List<StudentSubjectDto> GetAllStudentSubjectModels();
        public bool SubjectExists(long subjectId);
        public bool StudentExists(long studentId);

        public Student GetStudentById(long studentId);

    }
}
