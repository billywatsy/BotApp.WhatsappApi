using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApp2
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<tCourseWork> tCourseWorks { get; set; }
        public virtual DbSet<tExam> tExams { get; set; }
        public virtual DbSet<tNotication> tNotications { get; set; }
        public virtual DbSet<tSchool> tSchools { get; set; }
        public virtual DbSet<tStudent> tStudents { get; set; }
        public virtual DbSet<tStudentParent> tStudentParents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parent>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Parent>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Parent>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Parent>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.TestSchoolId)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.TestName)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.TermNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.SubjectCode)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.SubjectName)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.ClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.ClassName)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.LevelCode)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.LevelName)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.TestGrade)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.Effort)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tCourseWork>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.TermNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.SubjectCode)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.SubjectName)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.ClassCode)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.ClassName)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.LevelCode)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.LevelName)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.ExamGrade)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.Effort)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tExam>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tNotication>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<tNotication>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<tNotication>()
                .Property(e => e.FileFullUrl)
                .IsUnicode(false);

            modelBuilder.Entity<tNotication>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tNotication>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tSchool>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<tSchool>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tSchool>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tSchool>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tStudent>()
                .Property(e => e.SchoolRegNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tStudent>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tStudent>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tStudentParent>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<tStudentParent>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);
        }
    }
}
