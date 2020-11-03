using Microsoft.EntityFrameworkCore;
using StudentsEFAssignment.Entities;

namespace StudentsEFAssignment.Contexts
{
    class UniversityDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=tsu;Integrated security=true;");
        }
    }
}
