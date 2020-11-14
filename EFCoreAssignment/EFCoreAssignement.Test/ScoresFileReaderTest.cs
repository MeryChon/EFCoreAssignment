using EFCoreAssignment.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EFCoreAssignement.Test
{
    [TestClass]
    public class ScoresFileReaderTest
    {
        private readonly string ProjectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        [TestMethod]
        public void TestReadFileLinesOk()
        {
            string filePath = ProjectDirectory + @"\Resources\StudentPointsDataOk.csv";
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

        [TestMethod]
        public void TestReadFileLinesFailed()
        {
            ScoresFileReader sfr = new ScoresFileReader();
            string filePath = ProjectDirectory + @"Test\Resources\StudentPointsDataBla.csv";
            string[] lines = sfr.ReadLines(filePath);
            Assert.IsTrue(lines == null);
        }

        [TestMethod]
        public void TestParseSingleLineOk()
        {
            ScoresFileReader sfr = new ScoresFileReader();
            string line = "12,5,75";
            string[] data = sfr.ParseLine(line, 1);
            Assert.IsTrue(data.Length == 3);
            Assert.AreEqual(data[0], "12");
            Assert.AreEqual(data[1], "5");
            Assert.AreEqual(data[2], "75");
        }

        [TestMethod]
        public void TestParseSingleLineNull()
        {
            ScoresFileReader sfr = new ScoresFileReader();
            string line = "12,5,";
            string[] data = sfr.ParseLine(line, 1);
            Assert.IsNull(data);
        }
    }
}
