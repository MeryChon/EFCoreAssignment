using EFCoreAssignment.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentsEFAssignment.Contexts;
using StudentsEFAssignment.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreAssignement.Test
{
    [TestClass]
    public class UniversityApiTest
    {
        private static IUniversityApi api;

        [ClassInitialize]
        public static void BeforeAllTests(TestContext context)
        {
            //var mockStudentSet = new Mock<DbSet<Student>>();
            //var mockSubjectSet = new Mock<DbSet<Subject>>();
            IQueryable<StudentSubject> data = new List<StudentSubject>
            {
                new StudentSubject
                {
                    Id = 1,
                    StudentId = 10,
                    SubjectId = 3
                },
                new StudentSubject {
                    Id = 2,
                    StudentId = 5,
                    SubjectId = 2
                },
                new StudentSubject
                {
                    Id = 3,
                    StudentId = 5,
                    SubjectId = 3,
                    Score = 100
                }
            }.AsQueryable();

            var mockStudentSubjectSet = new Mock<DbSet<StudentSubject>>();
            mockStudentSubjectSet.As<IQueryable<StudentSubject>>().Setup(ss => ss.Provider).Returns(data.Provider);
            mockStudentSubjectSet.As<IQueryable<StudentSubject>>().Setup(ss => ss.Expression).Returns(data.Expression);
            mockStudentSubjectSet.As<IQueryable<StudentSubject>>().Setup(ss => ss.ElementType).Returns(data.ElementType);
            mockStudentSubjectSet.As<IQueryable<StudentSubject>>().Setup(ss => ss.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<UniversityDbContext>();
            //mockContext.Setup(m => m.Students).Returns(mockStudentSet.Object);
            //mockContext.Setup(m => m.Subjects).Returns(mockSubjectSet.Object);
            mockContext.Setup(m => m.StudentSubjects).Returns(mockStudentSubjectSet.Object);

            api = new UniversityApi(mockContext.Object);
        }

        [TestMethod]
        public void GetStudentSubjectTestSuccess()
        {
            StudentSubject ss = api.GetStudentSubject(10, 3);
            Assert.AreEqual(ss.StudentId, 10);
            Assert.AreEqual(ss.SubjectId, 3);
            Assert.AreEqual(ss.Id, 1);
        }

        [TestMethod]
        public void GetStudentSubjectTestFail()
        {
            Assert.IsNull(api.GetStudentSubject(10, 10));
        }

        [TestMethod]
        public void SaveStudentSubjectScoreTestSuccess()
        {
            StudentSubject ss = api.SaveStudentSubjectScore(5, 2, 91);
            Assert.IsNotNull(ss);
            Assert.AreEqual(ss.Id, 2);
            Assert.AreEqual(ss.StudentId, 5);
            Assert.AreEqual(ss.SubjectId, 2);
            Assert.AreEqual(ss.Score, 91);
        }

        [TestMethod]
        public void SaveStudentSubjectScoreTestFail()
        {
            StudentSubject ss = api.SaveStudentSubjectScore(100, 100, 50);
            Assert.IsNull(ss);
        }



    }
}
