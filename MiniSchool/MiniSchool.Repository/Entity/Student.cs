namespace MiniSchool.Repository.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema; 

    [Table("tStudent")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [StringLength(50)]
        public string SchoolRegNumber { get; set; }

        public string Fullname { get; set; }
        public int SchoolId { get; set; }


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
