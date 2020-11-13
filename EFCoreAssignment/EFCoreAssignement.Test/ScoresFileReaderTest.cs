using EFCoreAssignment.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EFCoreAssignement.Test
{
    [TestClass]
    class ScoresFileReaderTest
    {
        private readonly string ProjectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        [TestMethod]
        public void TestReadFileLinesOk()
        {
            string filePath = ProjectDirectory + @"Test\Resources\StudentPointsDataOk.csv";
            //UniversityDbContext dbContext = new UniversityDbContext();
            //IUniversityApi api = new UniversityApi(dbContext);
            ScoresFileReader sfr = new ScoresFileReader();

            string[] lines = sfr.ReadLines(filePath);
            Assert.IsTrue(lines.Length == 4);
            Assert.AreEqual(lines[0], "StudentId,SubjectId,Point");
            Assert.AreEqual(lines[1], "10,3,90");
            Assert.AreEqual(lines[2], "2,4,67");
            Assert.AreEqual(lines[3], "15,2,51");
        }
    }
}
