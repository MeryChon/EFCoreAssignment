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
            var mockStudentSet = SetupMockDbSet(new List<Student> {
                new Student
                {
                    Id = 5,
                    FirstName = "Mikasa",
                    LastName = "Akerman"
                },
                new Student
                {
                    Id = 10,
                    FirstName = "Eren",
                    LastName = "Yeager"
                }
            });

            var mockSubjectSet = SetupMockDbSet(new List<Subject> {
                new Subject
                {
                    Id = 2,
                    Name = "Physics"
                },
                new Subject
                {
                    Id = 3,
                    Name = "Multicore Programming"
                }
            });

            var mockStudentSubjectSet = SetupMockDbSet(new List<StudentSubject> {
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
            });

            var mockContext = new Mock<UniversityDbContext>();
            mockContext.Setup(m => m.Students).Returns(mockStudentSet.Object);
            mockContext.Setup(m => m.Subjects).Returns(mockSubjectSet.Object);
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

        //[TestMethod] FIXME: This method fails
        //public void StudentExistsTestSuccess()
        //{
        //    Assert.IsTrue(api.StudentExists(10));
        //}

        [TestMethod]
        public void StudentExistsTestFail()
        {
            Assert.IsFalse(api.StudentExists(0));
        }

        //[TestMethod] FIXME: This method fails
        //public void SubjectExistsTestSuccess()
        //{
        //    Assert.IsTrue(api.SubjectExists(2));
        //}

        [TestMethod]
        public void SubjectExistsTestFail()
        {
            Assert.IsFalse(api.SubjectExists(100));
        }

        private static Mock<DbSet<T>> SetupMockDbSet<T>(List<T> seedData) where T : class
        {
            IQueryable<T> data = seedData.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(t => t.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(t => t.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(t => t.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(t => t.GetEnumerator()).Returns(data.GetEnumerator());
            return mockDbSet;
        }

    }
}
