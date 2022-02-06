namespace ConsoleApp2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tExam")]
    public partial class tExam
    {
        [Key]
        public int ExamId { get; set; }

        public int StudentId { get; set; }

        public int Year { get; set; }

        [StringLength(5)]
        public string TermNumber { get; set; }

        [StringLength(50)]
        public string SubjectCode { get; set; }

        [StringLength(200)]
        public string SubjectName { get; set; }

        [StringLength(50)]
        public string ClassCode { get; set; }

        [StringLength(50)]
        public string ClassName { get; set; }

        [StringLength(50)]
        public string LevelCode { get; set; }

        [StringLength(200)]
        public string LevelName { get; set; }

        public int? PercentageMark { get; set; }

        [StringLength(50)]
        public string ExamGrade { get; set; }

        [StringLength(50)]
        public string Effort { get; set; }

        [StringLength(200)]
        public string Comment { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
