using Microsoft.EntityFrameworkCore;
using MiniSchool.Repository.Entity;
using System;

namespace MiniSchool.Repository
{
    public class AppContext : DbContext
    {

        public DbSet<Parent> Parents { get; set; }
        public  DbSet<CourseWork> CourseWorks { get; set; }
        public  DbSet<Exam> Exams { get; set; }
        public  DbSet<Notification> Notications { get; set; }
        public  DbSet<School> Schools { get; set; }
        public  DbSet<Student> Students { get; set; }
        public  DbSet<StudentParent> StudentParents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=156.38.224.15;database=imonitor_mini_school;user=imonitor_mini_school;password=7b!og09C");
        }

    }
}
