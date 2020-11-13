using EFCoreAssignment.Api;
using EFCoreAssignment.Util;
using StudentsEFAssignment.Contexts;
using System.IO;

namespace EFCoreAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will get the current WORKING directory (i.e. ...\bin\Debug)
            string workingDirectory = Directory.GetCurrentDirectory();

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            UniversityDbContext dbContext = new UniversityDbContext();
            IUniversityApi api = new UniversityApi(dbContext);

            string filePath = projectDirectory + @"\Resources\StudentPointsData.csv";
            ScoresFileReader sfr = new ScoresFileReader(filePath, api);
            sfr.ReadAndStoreFileData();
        }
    }
}
