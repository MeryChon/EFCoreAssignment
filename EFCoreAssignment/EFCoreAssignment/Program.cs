using EFCoreAssignment.Util;
using System.IO;

namespace EFCoreAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Directory.GetCurrentDirectory();

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            string filePath = projectDirectory + @"\Resources\StudentPointsData.csv";
            ScoresFileReader sfr = new ScoresFileReader(filePath);
            sfr.ParseAndStoreData();
        }
    }
}
