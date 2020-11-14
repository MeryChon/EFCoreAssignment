using EFCoreAssignment.Util;
using Microsoft.EntityFrameworkCore;
using StudentsEFAssignment.Entities;
using System;
using System.Collections.Generic;

namespace StudentsEFAssignment.Contexts
{
    public class UniversityDbContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=tsu;Integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RandomDataGenerator rdg = new RandomDataGenerator();

            List<Student> initialRandomStudents = rdg.GetRandomStudents(50);
            modelBuilder.Entity<Student>().HasData(initialRandomStudents);

            List<Subject> initialRandomSubjects = rdg.GetRandomSubjects();
            modelBuilder.Entity<Subject>().HasData(initialRandomSubjects);

            List<StudentSubject> randomStudentSubjects = GetRandomStudentSubjects(initialRandomStudents, initialRandomSubjects);
            modelBuilder.Entity<StudentSubject>().HasData(randomStudentSubjects);
        }

        private List<StudentSubject> GetRandomStudentSubjects(List<Student> students, List<Subject> subjects)
        {

            Random r = new Random();
            List<StudentSubject> studentSubjects = new List<StudentSubject>();
            long nextId = 1;

            foreach (Student s in students)
            {
                int randomIndex1 = r.Next(subjects.Count);
                long randomId1 = subjects[randomIndex1].Id;
                studentSubjects.Add(new StudentSubject
                {
                    Id = nextId++,
                    StudentId = s.Id,
                    SubjectId = randomId1
                });

                int randomIndex2 = r.Next(subjects.Count);
                while (randomIndex1 == randomIndex2)
                {
                    randomIndex2 = r.Next(subjects.Count);
                }

                long randomId2 = subjects[randomIndex2].Id;
                studentSubjects.Add(new StudentSubject
                {
                    Id = nextId++,
                    StudentId = s.Id,
                    SubjectId = randomId2
                });
            }

            return studentSubjects;
        }

    }
}
